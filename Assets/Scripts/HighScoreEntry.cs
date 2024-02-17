using System.Collections.Generic;
using UnityEngine;
public class HighScoreEntry : MonoBehaviour
{
    public HighScoreDisplay[] highScoreDisplayArray;
    List<HighScoreEntry> scores = new List<HighScoreEntry>();
    public float score;
    void Start()
    {
        // Adds some test data
        
        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.score.CompareTo(x.score));
        for (int i = 0; i < highScoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplayArray[i].DisplayHighScore(scores[i].name, scores[i].score);
            }
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }
    }
    public void AddNewScore(string entryName, float entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, score = entryScore });
    }
}
