using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Player player;
    public GameObject block;
    public Transform blockContainer;
    private float playerSide;
    private float blockSide;
    private Vector3 lastBlockPos;

    public GameObject pauseMenu;
    public GameObject scoreText;

    void Awake() {
        Mesh mesh = block.GetComponent<MeshFilter>().sharedMesh;
        Bounds bounds = mesh.bounds;
        blockSide = bounds.size.x;

        mesh = player.GetComponent<MeshFilter>().mesh;
        bounds = mesh.bounds;
        playerSide = bounds.size.x * player.GetComponent<Transform>().localScale.x;

        
        Vector3 playerPos = player.startPos;
        lastBlockPos = new Vector3(playerPos.x, playerPos.y - (blockSide + playerSide) * 0.5f, playerPos.z);

        gameStart();
    }

    void Start()
    {
        player.setVelocity();
    }

    public void gameStart() {
        player.reset();
        Time.timeScale = 1;
        
        scoreText.SetActive(true);
        pauseMenu.SetActive(false);

        foreach (Transform child in blockContainer)
        {
            GameObject.Destroy(child.gameObject);
        }


        //create 3x3 platform
        lastBlockPos = player.startPos;
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

    public void onLose() {
        Time.timeScale = 0;
        scoreText.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
