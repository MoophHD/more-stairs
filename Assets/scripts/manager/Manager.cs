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
     

        const int START_BLOCKS = 10;

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
    public void spawn() {
        Vector3 nextPos;
        nextPos = new Vector3(lastBlockPos.x + blockSide, lastBlockPos.y, lastBlockPos.z);

        createBlock(nextPos);

        if (id%4 == 3) {
            createBlock(new Vector3(nextPos.x, nextPos.y + blockSide, nextPos.z), false);
        }

        id++;

        lastBlockPos = nextPos;
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
}
