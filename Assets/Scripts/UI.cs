using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{ 
    [SerializeField]
    GameObject finishScreen;

    [SerializeField]
    Text level;

    [SerializeField]
    GameObject retryScreen;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject levelSelect;

    [SerializeField]
    GameObject unlockButtons;

    [SerializeField]
    GameObject controlsScreen;

    [SerializeField]
    Text score;
    int scoreTracker = -1;

    [SerializeField]
    GameObject soundScreen;

    // Start is called before the first frame update
    void Start()
    {
        showFinishScreen(false);
        showLevel(true);
        showRetryScreen(false);
        showMainMenu(true);
        showLevelSelect(false);
        showScore(false);
        showControlsScreen(false);
        showSoundScreen(false);

        setButtons();
        updateButtonUnlocks(0);
        updateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showFinishScreen(bool set)
    {
        finishScreen.SetActive(set);
    }

    public void showLevel(bool set)
    {
        level.gameObject.SetActive(set);
    }

    public void showRetryScreen(bool set)
    {
        retryScreen.SetActive(set);
    }

    public bool getInRetryScreen()
    {
        return retryScreen.activeSelf;
    }
    public void showMainMenu(bool set)
    {
        mainMenu.SetActive(set);
    }

    public void showLevelSelect(bool set)
    {
        levelSelect.SetActive(set);
    }
    public void setLevel(int i)
    {
        i += 1;
        level.text = " " + i.ToString();
    }

    void setButtons()
    {
        for(int i = 0; i < unlockButtons.transform.childCount; i++)
        {
            unlockButtons.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }
    public void updateButtonUnlocks(int i)
    {
        unlockButtons.transform.GetChild(i).GetComponent<Button>().interactable = true;
    }

    public void showControlsScreen(bool set)
    {
        controlsScreen.SetActive(set);
    }

    public void showSoundScreen(bool set)
    {
        soundScreen.SetActive(set);
    }
    public void updateScore()
    {
        scoreTracker += 1;
        score.text = scoreTracker.ToString();
    }
    public void showScore(bool set)
    {
        score.gameObject.SetActive(set);
    }
    public void resetScore()
    {
        scoreTracker = 0;
        score.text = scoreTracker.ToString();
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
