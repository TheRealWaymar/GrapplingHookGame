using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public float time;
    public FinishLine finished;
    void Update()
    {
        if(finished.isFinished == true) return;
        time = time + Time.deltaTime;
        timerText.text="Time: "+time;
    }
}
