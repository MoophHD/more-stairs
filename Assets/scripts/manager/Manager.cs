using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private bool isRight;
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


        Vector3 playerPos = player.GetComponent<Transform>().position;
        lastBlockPos = new Vector3(playerPos.x, playerPos.y - (blockSide + playerSide) * 0.5f, playerPos.z);

        gameStart();
    }

    void Start()
    {
        player.setVelocity(isRight);
    }

    public void gameStart() {
        scoreText.SetActive(true);
        pauseMenu.SetActive(false);

        foreach (Transform child in blockContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        isRight = Random.value > 0.5f;

        createBlock(lastBlockPos);
        spawn();
        spawn();
        spawn();
    }

    public void spawn() {
        Vector3 nextPos;
        if (isRight) {
            nextPos = new Vector3(lastBlockPos.x + blockSide, lastBlockPos.y, lastBlockPos.z);
        } else {
            nextPos = new Vector3(lastBlockPos.x, lastBlockPos.y, lastBlockPos.z + blockSide);
        }

        createBlock(nextPos);

        lastBlockPos = nextPos;
    }

    void createBlock(Vector3 pos) {
        GameObject nextBlock = Instantiate(block, pos, Quaternion.identity);
        nextBlock.GetComponent<Transform>().SetParent(blockContainer);
    }

    public void onLose() {
        scoreText.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
