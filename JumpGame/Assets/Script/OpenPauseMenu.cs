using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OpenPauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Gán panel pause menu trong Inspector
    public GameObject canvas;
    public GameObject canvas1;
    private bool isPaused = false;
    private bool isRestart = false;
    void Update()
    {
        Debug.Log("ho"+isRestart);
        // if(isRestart) canvas.SetActive(false);
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f; // Dừng thời gian khi tạm dừng
    }

    public  void Restart(){
        isRestart = true; 
        // canvas.SetActive(false);
        // canvas1.SetActive(false);
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        //
        pauseMenu.SetActive(false);
        
        Time.timeScale = 1f;
        
    }
    public void QuitGame(){
        Application.Quit();
    }
}
