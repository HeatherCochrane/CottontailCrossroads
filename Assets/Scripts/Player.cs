using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject cam;

    [SerializeField]
    GameObject mask;

    [SerializeField]
    GameObject platform;

    [SerializeField]
    WorldGeneration world;

    float move = 2.4f;

    [SerializeField]
    UI screens;

    [SerializeField]
    Audio audio;

    float side = 2.4f;

    bool finished = false;

    bool inMenu = false;

    AudioSource source;
    AudioClip horn;

    [SerializeField]
    PlayerAnim anim;

    bool playerAlive = true;
    bool playerCanMove = true;
    // Start is called before the first frame update
    void Start()
    {
        leaveGame();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !inMenu && playerCanMove)
        {
            this.transform.position += new Vector3(0, move, 0);
            cam.transform.position += new Vector3(0, move, 0);
            mask.transform.position += new Vector3(0, move, 0);
            world.spawnNewPlatform();
            anim.hop();
            playerCanMove = false;
            Invoke("letPlayerMove", 0.1f);
        }

        if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && this.transform.position.x != 2.4f && !inMenu)
        {
            this.transform.position += new Vector3(side, 0, 0);
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && this.transform.position.x != -2.4f && !inMenu)
        {
            this.transform.position += new Vector3(-side, 0, 0);
        }

        if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.R)) && screens.getInRetryScreen())
        {
            newGame();
        }
    }

    void letPlayerMove()
    {
        playerCanMove = true;
    }

    void resetPlayer()
    {
        world.restartLevel(finished);
        this.transform.position = new Vector3(0, -0.8f, 0);
        cam.transform.position = new Vector3(0, 0, -10);
        mask.transform.position = new Vector3(0, 0, 0);
        playerAlive = true;

        if (finished)
        {
            finished = false;
        }
    }

    public void playNextLevel()
    {
        world.nextLevel();
        resetPlayer();
    }

    void playerDied()
    {
        world.spawnBlood(this.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Car")
        {
            playerDied();
            this.transform.GetChild(0).gameObject.SetActive(false);
            inMenu = true;
            screens.showRetryScreen(true);
            finished = false;
            audio.carHit();
            playerAlive = false;
        }

        if (collision.tag == "Finish")
        {
            screens.showFinishScreen(true);
            inMenu = true;
            finished = true;
            audio.reachedEnd();
        }
    }

    public void leaveGame()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        inMenu = true;
    }

    public void startGame()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        inMenu = false;
        playerAlive = true;
    }

    public void newGame()
    {
        screens.showRetryScreen(false);
        resetPlayer();
        this.transform.GetChild(0).gameObject.SetActive(true);
        inMenu = false;
        screens.resetScore();
        playerAlive = true;
    }

    public bool getPlayerAlive()
    {
        return playerAlive;
    }

}
