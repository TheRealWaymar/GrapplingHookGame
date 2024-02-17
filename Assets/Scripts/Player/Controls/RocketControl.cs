using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RocketControl : MonoBehaviour
{
    public GameObject missile;
    public float propulsionForce;
    private Transform launcherTransform;
    public bool _canShoot = true;
    private float nextFire;
    public float fireRate;
    public Image cooldownImage;
    public AudioSource rocketLaunchSound;
    void Start()
    {
        launcherTransform = transform;
    }
    //Controls cooldown image and firing of rocket when f pressed
    void Update()
    {
        Mathf.Clamp(cooldownImage.fillAmount, 0, 1);
		if(cooldownImage.fillAmount !=1)
		{
		cooldownImage.fillAmount = 1.0f - ((nextFire - (Time.time))/fireRate);
		}
        
        if(Input.GetKeyDown(KeyCode.F) && Time.time > nextFire)
        {   
                nextFire = Time.time + fireRate;
                cooldownImage.fillAmount = 0;
                LaunchMissile(); 
        }
    }
    //Launch missile object and propel forwards function 
    void LaunchMissile()
    {
       rocketLaunchSound.Play();
       GameObject missileObj = (GameObject) Instantiate(missile, launcherTransform.transform.TransformPoint(0,0,0), launcherTransform.rotation);
       missileObj.GetComponent<Rigidbody>().AddForce(launcherTransform.forward * propulsionForce, ForceMode.Impulse);
       Destroy(missileObj, 3);
    }
    
    
}
