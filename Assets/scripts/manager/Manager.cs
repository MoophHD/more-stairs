using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Manager : MonoBehaviour
{
    public Player player;
    public GameObject block;
    public Transform blockContainer;
    public GameObject pauseMenu;
    public GameObject idleMenu;
    public ScoreText scoreText;
    private CameraController myCamera;
    private Score score;
    private BlockColor blockColor;
    private Vector3 lastBlockPos;
    public GameObject pointUp;

    private float playerSide;
    private float blockSide;
    private float pointUpSide;
    private float playerJumpHeight;

    const int START_BLOCKS = 10;

    void Awake() {
        PopUpController.init();
        myCamera = Camera.main.GetComponent<CameraController>();
        blockColor = Camera.main.GetComponent<BlockColor>();
        score = Camera.main.GetComponent<Score>();
        Mesh mesh = block.GetComponent<MeshFilter>().sharedMesh;
        Bounds bounds = mesh.bounds;
        blockSide = bounds.size.x;

        mesh = player.GetComponent<MeshFilter>().mesh;
        bounds = mesh.bounds;
        playerSide = bounds.size.x * player.GetComponent<Transform>().localScale.x;

        mesh = pointUp.GetComponent<MeshFilter>().sharedMesh;
        bounds = mesh.bounds;


        pointUpSide = bounds.size.x;
        playerJumpHeight = player.jumpHeight;
    }

    void Start() {
        gameStart();
    }

    public void gameStart() {
        //idle mode
        blockColor.reset(); 
        score.score = 0;
        id = 0;
        Time.timeScale = 0;
        scoreText.gameObject.SetActive(true);

        player.reset();
        myCamera.reset();
        
        idleMenu.SetActive(true);
        pauseMenu.SetActive(false);

        foreach (Transform child in blockContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        //create 3x3 platform
        lastBlockPos = new Vector3(player.startPos.x, player.startPos.y - playerSide * 1.5f, player.startPos.z);

        createBlock(lastBlockPos);
        createBlock(new Vector3(lastBlockPos.x - blockSide, lastBlockPos.y, lastBlockPos.z));
        createBlock(new Vector3(lastBlockPos.x, lastBlockPos.y, lastBlockPos.z - blockSide));
        createBlock(new Vector3(lastBlockPos.x + blockSide, lastBlockPos.y, lastBlockPos.z + blockSide));
        createBlock(new Vector3(lastBlockPos.x - blockSide, lastBlockPos.y, lastBlockPos.z - blockSide));
        createBlock(new Vector3(lastBlockPos.x + blockSide, lastBlockPos.y, lastBlockPos.z - blockSide));
        createBlock(new Vector3(lastBlockPos.x - blockSide, lastBlockPos.y, lastBlockPos.z + blockSide));
        createBlock(new Vector3(lastBlockPos.x, lastBlockPos.y, lastBlockPos.z + blockSide));

        for (int i = 0; i < START_BLOCKS; i++) {
            spawn();
        }
    }

    public void gamePlay() {
        //real game start
        scoreText.isUpdating = true;
        scoreText.setHgScoreMode(false);

        Time.timeScale = 1;
        player.start();
        idleMenu.SetActive(false);
    }

    public void onLose() {
        scoreText.gameObject.SetActive(false);
        scoreText.isUpdating = false;
        scoreText.setHgScoreMode(true);

        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public int id = 0;
    private int lastStepId = 0;
    private int maxNoStepBlocks = 5;
    private float pointUpRateMiddle = 1f;
    private float pointUpRateStepUp = 0.45f;
    private int lastRowLength;
    private Vector3 lastMiddleVector;
    private Vector3 lastStepUpVector;
    
    
    public void spawn() {
        Vector3 nextPos;
        nextPos = new Vector3(lastBlockPos.x + blockSide, lastBlockPos.y, lastBlockPos.z);

        // // 0.05 + 0.01 per 25
        float oneBlockChance = Mathf.Min(0.05f + (id / 25) / 100, 0.13f);
        float twoBlockChance = 1f;
        // float twoBlockChance = Mathf.Min(0.15f + (id / 25) / 100, 0.25f);

        int order = id - lastStepId;
        float chance = 0;
        if (id < START_BLOCKS)
        {
            chance = 0f;
        }
        else if (order == 1)
        {
            chance = oneBlockChance;
        }
        else if (order == 2)
        {
            chance = twoBlockChance;
        }
        else
        {
            // 1 = coef (max - 2)^2 + min_chance
            // coef = (1 - min_chance) / (max - 2)^2
            chance = 0.09444f * Mathf.Pow((order - 2), 2) + oneBlockChance + twoBlockChance;
        }
        
 

        //success, step up
        if (chance > Random.value) {

            
            //block under new layer, not triggirable
            createBlock(nextPos, false);

            Vector3 upPos = new Vector3(nextPos.x, lastBlockPos.y + blockSide, nextPos.z);
            createBlock(upPos);

            lastBlockPos = upPos;

            float randi = Random.value;

         
            int rowLength = id - lastStepId;
            //don't spawn a candy on 1st blocks
            if (lastStepId == 0) { lastStepId = id; return;};

            lastStepId = id;

            if (order > 1) {
                if (pointUpRateMiddle > randi) {                    Vector3 nextPointUp = new Vector3(
                        // 0.35f + 0.2f * Random.value,
                        nextPos.x - (rowLength) * ( 0.425f + 0.15f *Random.value ) - blockSide,
                        nextPos.y,
                        nextPos.z
                    );
                    createPointUp(nextPointUp);
                }
            } else {
                if (pointUpRateStepUp > randi)
                {
                    lastStepUpVector = new Vector3(nextPos.x - blockSide, nextPos.y, nextPos.z);
                    createPointUp(lastStepUpVector);
                }
            }

        } else {
            createBlock(nextPos);
            lastBlockPos = nextPos;
        }

        id++;
    }

    void createBlock(Vector3 pos) {
        GameObject nextBlock = Instantiate(block, pos, Quaternion.identity);
        nextBlock.GetComponent<Transform>().SetParent(blockContainer);
    }

    void createBlock(Vector3 pos, bool trigger)
    {
        GameObject nextBlock = Instantiate(block, pos, Quaternion.identity);
        nextBlock.GetComponent<Transform>().SetParent(blockContainer);

        nextBlock.GetComponent<Block>().isSpawnTrigger = trigger;
    }

    void createPointUp(Vector3 blockPos) {
        Vector3 pos = new Vector3(blockPos.x, blockPos.y + playerJumpHeight + (blockSide + pointUpSide) * .5f, blockPos.z);
        GameObject nextPointUp = Instantiate(pointUp, pos, pointUp.GetComponent<Transform>().rotation);
        nextPointUp.GetComponent<Transform>().SetParent(blockContainer);
    }
}
