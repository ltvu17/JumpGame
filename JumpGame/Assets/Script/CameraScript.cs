using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame(){
        Application.Quit();
    }
}
