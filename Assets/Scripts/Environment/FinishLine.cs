using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class FinishLine : MonoBehaviour
{
    public GameObject VictoryUI;
    public bool isFinished = false;
    public Timer finishTime;
    public HighScoreEntry highScore;
    public GameObject player;
    public GameObject inputField;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject==player)
        {
            Victory();
        }
    }

    void Victory()
    {
        VictoryUI.SetActive(true);
        isFinished = true;
        player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void SaveScore(string input)
    {
        input = inputField.GetComponent<TMP_InputField>().text;
        highScore.AddNewScore(input, finishTime.time);
    }
    public void returnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
