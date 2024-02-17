using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BoxScore : MonoBehaviour
{
    public TMP_Text boxesHit;
    public int boxes;
    public int maxBoxes;
    public GameObject[] allBoxes;
    public GameObject finishLine;
    void Start()
    {
        allBoxes = GameObject.FindGameObjectsWithTag("Box");
        for(int i=0; i<allBoxes.Length; i++)
        {
            maxBoxes++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        boxesHit.text = "Boxes Hit: " + boxes + "/" + maxBoxes;
        if(boxes==maxBoxes)
        {
            finishLine.SetActive(true);
            boxesHit.color = Color.green;
        }
        
    }
}
