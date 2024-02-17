using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHit : MonoBehaviour
{
    public Material greenTick;
    public AudioSource boxHitSound;
    public BoxScore boxScore;
    public GameObject boxScoreObj;
    public bool hasHit;
    //void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag=="Rocket")
    //    {
    //        boxHitSound.Play();
    //        this.gameObject.GetComponent<MeshRenderer>().material = greenTick;
    //    }
    //}
    public void Start()
    {
        boxScoreObj = GameObject.FindGameObjectWithTag("BoxScore");
        boxScore = boxScoreObj.gameObject.GetComponent<BoxScore>();
    }
    public void boxIsHit()
    {
        
        if(this.gameObject.GetComponent<MeshRenderer>().material == greenTick || hasHit == true) return;
        hasHit = true;
        boxHitSound.Play();
        this.gameObject.GetComponent<MeshRenderer>().material = greenTick;
        
        boxScore.boxes= boxScore.boxes + 1;
        //StartCoroutine(Reset());
    }
    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
    }
}
