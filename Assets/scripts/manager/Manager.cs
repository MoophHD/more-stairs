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
    private GameObject popUpTextCanvas;

    private float playerSide;
    private float blockSide;
    private float blockSideY;
    private float pointUpSide;
    private float playerJumpHeight;

    const int START_BLOCKS = 10;
    public GameObject blockCover;
    private float coverLength;

    void Awake() {
        PopUpController.init();
        popUpTextCanvas = GameObject.Find("popUpTextCanvas");
        myCamera = Camera.main.GetComponent<CameraController>();
        blockColor = Camera.main.GetComponent<BlockColor>();
        score = Camera.main.GetComponent<Score>();
        Mesh mesh = block.GetComponent<MeshFilter>().sharedMesh;
        Bounds bounds = mesh.bounds;
        blockSide = bounds.size.x;
        blockSideY = bounds.size.y * block.transform.localScale.y;
        
        mesh = player.GetComponent<MeshFilter>().mesh;
        bounds = mesh.bounds;
        playerSide = bounds.size.x * player.GetComponent<Transform>().localScale.x;

        mesh = pointUp.GetComponent<MeshFilter>().sharedMesh;
        bounds = mesh.bounds;
        pointUpSide = bounds.size.x;

        mesh = blockCover.GetComponent<MeshFilter>().sharedMesh;
        bounds = mesh.bounds;
        coverLength = bounds.size.x;
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
        lastStepId = 0;
        blockOrderIdle = "";
        scoreText.gameObject.SetActive(true);
        

        player.reset();
        myCamera.reset();
        
        idleMenu.SetActive(true);
        pauseMenu.SetActive(false);

        foreach (Transform child in blockContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in popUpTextCanvas.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        

        //create 3x3 platform
        lastBlockPos = new Vector3(player.startPos.x, player.startPos.y - playerSide * 1.5f - 0.08f, player.startPos.z);

        createBlock(lastBlockPos);
        //spawn around
        createBlock(new Vector3(lastBlockPos.x - blockSide, lastBlockPos.y, lastBlockPos.z));
        createBlock(new Vector3(lastBlockPos.x, lastBlockPos.y, lastBlockPos.z - blockSide));
        createBlock(new Vector3(lastBlockPos.x + blockSide, lastBlockPos.y, lastBlockPos.z + blockSide));
        createBlock(new Vector3(lastBlockPos.x - blockSide, lastBlockPos.y, lastBlockPos.z - blockSide));
        createBlock(new Vector3(lastBlockPos.x + blockSide, lastBlockPos.y, lastBlockPos.z - blockSide));
        createBlock(new Vector3(lastBlockPos.x - blockSide, lastBlockPos.y, lastBlockPos.z + blockSide));
        createBlock(new Vector3(lastBlockPos.x, lastBlockPos.y, lastBlockPos.z + blockSide));

        // lastBlockPos = new Vector3(lastBlockPos.x - blockSide, lastBlockPos.y,lastBlockPos.z);

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
    private int maxOrder = 4;
    private float pointUpRateMiddle = 0.2f - perMissingMiddle;
    private float candyTargetMedium = 0.375f;
    private int middleNoPoints = 0;
    private static float perMissingMiddle = 0.055f;
    private float pointUpRateStepUp = 0.45f;
    private int lastRowLength;
    private Vector3 lastMiddleVector;
    private Vector3 lastStepUpVector;
    private string blockOrderIdle = "";
    
    public void spawn() {
        Vector3 nextPos;
        nextPos = new Vector3(lastBlockPos.x + blockSide, lastBlockPos.y, lastBlockPos.z);

        // + chance per 35 points
        int moreChancePer = 32;
        float oneBlockChance = Mathf.Min(0.09f + (id / moreChancePer) / 100, 0.18f);
        float twoBlockChance = Mathf.Min(0.22f + (id / moreChancePer) / 100, 0.35f);

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
            float coef = (1 - (oneBlockChance + twoBlockChance)) / Mathf.Pow((maxOrder - 2), 2);
            chance = coef * Mathf.Pow((order - 2), 2) + oneBlockChance + twoBlockChance;
        }
        if (blockOrderIdle.Length > 1) {
            if (blockOrderIdle[0] == blockOrderIdle[1]) {
                string idleNum = blockOrderIdle[0].ToString();
                int idleOrder = int.Parse(idleNum);
                if (idleOrder == order) {

                        chance = chance * .25f;
                        // blockOrderIdle = blockOrderIdle[1].ToString();
                } else if (order != idleOrder) {
                    // print("idle order " + idleNum);
                    // print("order " + order);
                    // print("affected chance from " + chance + " to " + (Mathf.Abs(idleOrder - order) * 0.85f + 1) * chance);

                    chance = (Mathf.Abs(idleOrder - order) * 0.85f + 1) * chance;
                    // blockOrderIdle = blockOrderIdle[1].ToString();
                }
            } else {
                blockOrderIdle = blockOrderIdle[1].ToString();
            }
        }
            

        //success, step up
        if (chance > Random.value) {
            if (order != 0) {         
                blockOrderIdle += order.ToString();

                if (blockOrderIdle.Length > 2)
                {
                    blockOrderIdle = blockOrderIdle[1].ToString() + order.ToString();
                }

            }

            //block under new layer, not triggirable
            createBlock(nextPos, false);

            Vector3 upPos = new Vector3(nextPos.x, lastBlockPos.y + blockSideY, nextPos.z);
            createBlock(upPos);

            lastBlockPos = upPos;

            float randi = Random.value;

            int rowLength = id - lastStepId;

            //CREATE_COVER (fixes jump problems & stuff)
            if (rowLength == 0) rowLength = 1;
            Vector3 nextCoverPos = new Vector3(
                nextPos.x - rowLength * blockSide * 0.5f - blockSide *.5f,
                nextPos.y + blockSideY * .5f + 0.01f,
                nextPos.z
            );
            GameObject newCover = Instantiate(blockCover, nextCoverPos, Quaternion.identity);
            newCover.transform.SetParent(blockContainer);
            Vector3 coverScale = newCover.transform.localScale;
            newCover.transform.localScale = new Vector3( (blockSide * rowLength) / coverLength, coverScale.y, coverScale.z );

            //SPAWN_CANDY
            //don't spawn a candy on 1st blocks
            if (lastStepId == 0) { lastStepId = id; return;};

            lastStepId = id;

            if (order > 1) {
                middleNoPoints++;
                float candyChance = pointUpRateMiddle + middleNoPoints * perMissingMiddle;
 
                if ( candyChance  > randi) {
                    Vector3 nextPointUp = new Vector3(
                        // 0.35f + 0.2f * Random.value,
                        nextPos.x - (rowLength) * ( 0.425f + 0.15f *Random.value ) - blockSide,
                        nextPos.y,
                        nextPos.z
                    );
                    createPointUp(nextPointUp);
                    middleNoPoints = 0;

                    }
                middleNoPoints++;
            } else {
                if (pointUpRateStepUp  > randi)
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
        // return;
        GameObject nextBlock = Instantiate(block, pos, Quaternion.identity);
        nextBlock.GetComponent<Transform>().SetParent(blockContainer);
    }

    void createBlock(Vector3 pos, bool trigger)
    {
        // return;
        
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
