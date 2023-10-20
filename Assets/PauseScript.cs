using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseScript : MonoBehaviour
{
    public static bool HelpUp = false;
    public static bool GamePaused = false;
    public GameObject PauseMenuUi;
    public GameObject HelpMenuUi;
    public GameObject Player;


    private void Start()
    {
        GamePaused = false;
        HelpUp = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("F");
            if(GamePaused)
            {
                Debug.Log("E");
                if (HelpUp)
                {
                    Debug.Log("fuck");
                    PauseMenuUi.SetActive(true);
                    HelpMenuUi.SetActive(false);
                    HelpUp = false;

                }
                else
                {
                    Resume();
                }
            } 

            

            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;


        PauseMenuUi.SetActive(false);
        Player.SetActive(true);    
        Time.timeScale = 1.0f;
        GamePaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;

        PauseMenuUi.SetActive(true);
        Player.SetActive(false); 
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        Player.SetActive(true);
        PauseMenuUi.SetActive(false);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void HelpMenu()
    {
        PauseMenuUi.SetActive(false);
        HelpMenuUi.SetActive(true);
        HelpUp = true;

    }
}
