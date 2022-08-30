using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject _menu;
    public GameObject _mode;
    public GameObject _difficulty;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickPlay() {
        //SceneManager.LoadScene(01);
        _menu.SetActive(false);
        _mode.SetActive(true);
    }

    public void clickQuit() {
        Application.Quit();
    }

    public void clickBackOne()
    {
        _mode.SetActive(false);
        _menu.SetActive(true);
    }
    public void setModeHotseat()
    {
        Settings._instance.mode = Mode.Hotseat;
        SceneManager.LoadScene(01);
    }
    public void setModeAI()
    {
        Settings._instance.mode = Mode.AI_Type1;
        _mode.SetActive(false);
        _difficulty.SetActive(true);
    }
    public void setDifficultyEasy()
    {
        Settings._instance.difficulty = Difficulty.Easy;
        SceneManager.LoadScene(02);
    }
    public void setDifficultyMedium()
    {
        Settings._instance.difficulty = Difficulty.Medium;
        SceneManager.LoadScene(02);
    }
    public void setDifficultyHard()
    {
        Settings._instance.difficulty = Difficulty.Hard;
        SceneManager.LoadScene(02);
    }
    public void setModeMCST()
    {
        Settings._instance.mode = Mode.AI_Type2;
        SceneManager.LoadScene(03);
    }

    public void clickBackTwo()
    {
        _difficulty.SetActive(false);
        _mode.SetActive(true);
    }
}
