using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject PausePanel;
    public  void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

     public void MainMenu()
    {
        SceneManager.LoadScene(0);
           Time.timeScale = 1;
    }

     public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

     public  void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

     private void Update()
     {
         if (Input.GetKeyDown(KeyCode.Escape))
         {
             PauseGame();
         }
     }
}
