using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject leaderboard;
    public void Play()
    {
         SceneManager.LoadScene("GameplayScene");
    }
    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
    public void BackButton()
    {
        controlsMenu.SetActive(false);
        leaderboard.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void Leaderboard()
    {
        mainMenu.SetActive(false);
        leaderboard.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
