﻿using UnityEngine;
using UnityEngine.SceneManagement;
public enum UIOptions { MainMenu, PauseMenu, Fade }
public class UIScript : MonoBehaviour
{
    #region VARIABLES
    [Header("UI Settings")]
    public UIOptions UI;
    bool pauseOn;
    [Header("Animation Settings")]
    public Animator animator;
    GameObject player;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        if (UI == UIOptions.PauseMenu)
            GetComponent<Canvas>().enabled = false;
        if(UI == UIOptions.Fade)
            player = GameObject.Find("Player");
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseOn == false && UI == UIOptions.PauseMenu)
        {
            GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
            pauseOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseOn == true && UI == UIOptions.PauseMenu)
        {
            GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1;
            pauseOn = false;
        }
    }
    #endregion
    //UI FUNCTIONS
    #region START GAME FUNCTION
    public void StartGame()
    {
        PlayerPrefs.SetInt("lives", 3);
        PlayerPrefs.SetInt("level", 1);
        animator.SetTrigger("FadeOut");
    }
    #endregion
    #region EXIT GAME FUNCTION
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
    #region RESUME FUNCTION
    public void Resume()
    {
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
    #endregion
    #region RESTART FUNCTION
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
    #endregion
    #region MAIN MENU FUNCTION
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
    #endregion
    #region ON FADE COMPLETE FUNCTION
    public void OnFadeComplete() 
    { 
        if(UI == UIOptions.MainMenu)
            SceneManager.LoadScene("Town");
        else if (UI == UIOptions.Fade && player.GetComponent<PlayerHealth>().loadTown == false)
            SceneManager.LoadScene("Level " + player.GetComponent<PlayerHealth>().level);
        else if (UI == UIOptions.Fade && player.GetComponent<PlayerHealth>().loadTown == true)
            SceneManager.LoadScene("Town");
    }
    #endregion
}
