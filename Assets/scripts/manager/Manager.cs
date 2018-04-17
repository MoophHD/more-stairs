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
    public GameObject scoreText;
    private Score score;
    private BlockColor blockColor;
    private Vector3 lastBlockPos;

    private float playerSide;
    private float blockSide;

    const int START_BLOCKS = 10;

    void Awake() {
        blockColor = Camera.main.GetComponent<BlockColor>();
        score = Camera.main.GetComponent<Score>();
        Mesh mesh = block.GetComponent<MeshFilter>().sharedMesh;
        Bounds bounds = mesh.bounds;
        blockSide = bounds.size.x;

        mesh = player.GetComponent<MeshFilter>().mesh;
        bounds = mesh.bounds;
        playerSide = bounds.size.x * player.GetComponent<Transform>().localScale.x;
    }

    void Start() {
        gameStart();
    }

    public void gameStart() {
        blockColor.reset(); 
        score.score = 0;
        id = 0;
        Time.timeScale = 0;
        scoreText.SetActive(false);
        player.reset();
        
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
        Time.timeScale = 1;
        player.start();
        scoreText.SetActive(true);
        idleMenu.SetActive(false);
    }

    public void onLose() {
        Time.timeScale = 0;
        scoreText.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public int id = 0;
    private int lastStepId = 0;
    // private int maximum = 5;
    
    public void spawn() {
        print("spawn, id " + id);
        Vector3 nextPos;
        nextPos = new Vector3(lastBlockPos.x + blockSide, lastBlockPos.y, lastBlockPos.z);

        int order = id - lastStepId;
        float chance = 0;
        if (id < START_BLOCKS) {
            chance = 0f;
        } else if (order == 1) {
            chance = 0.1f;
        } else if (order == 2) {
            chance = 0.18f;
        } else {
            // 1 = coef (max - 2)^2 + min_chance
            // coef = (1 - min_chance) / (max - 2)^2
            chance = 0.09444f * Mathf.Pow((order - 2), 2) + 0.1f;
        }
        
        //success, step up
        if (chance > Random.value) {
            lastStepId = id;
            //block under new layer, not triggirable
            createBlock(nextPos, false);

            Vector3 upPos = new Vector3(lastBlockPos.x + blockSide, lastBlockPos.y + blockSide, lastBlockPos.z);
            createBlock(upPos);

            lastBlockPos = upPos;
        } else {
            createBlock(nextPos);
            lastBlockPos = nextPos;
        }

        id++;
    }

    void createBlock(Vector3 pos) {
        print("create block");
        GameObject nextBlock = Instantiate(block, pos, Quaternion.identity);
        nextBlock.GetComponent<Transform>().SetParent(blockContainer);
    }

    void createBlock(Vector3 pos, bool trigger)
    {
        print("create block");

        GameObject nextBlock = Instantiate(block, pos, Quaternion.identity);
        nextBlock.GetComponent<Transform>().SetParent(blockContainer);

        nextBlock.GetComponent<Block>().isSpawnTrigger = trigger;
    }
}
