using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class grappleController : MonoBehaviour
{
    [Header("References")]
    private PlayerController pm;
    public Transform cam, gunTip, player;
    public LayerMask whatIsGrappleable;
    public AudioSource grappleSound;
    public AudioClip grappleHit;
    public AudioClip grappleMiss;
    public AudioClip SwingHit;
    public AudioClip SwingMiss;
    //public LineRenderer grappleLineRender;
    //public LineRenderer swingLineRenderer;
    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    private Vector3 grapplePoint;
    private bool grappling;
    public float overshootYAxis;
    
    [Header("Swinging")]
    public float maxSwingDistance;
    private Vector3 swingPoint;
    public bool swinging = false;
    private SpringJoint swingJoint;
    [Header("Cooldown")]
    //public float grapplingCd;
    public float grapplingCdTimer;
    public Image cooldownImage;
    private float nextGrapple;
    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    
    private void Start()
    {
        pm = GetComponent<PlayerController>();
    }
    private void Update()
    {
        Mathf.Clamp(cooldownImage.fillAmount, 0, 1);
		if(cooldownImage.fillAmount !=1)
		{
		cooldownImage.fillAmount = 1.0f - ((nextGrapple - (Time.time))/grapplingCdTimer);
		}

        //When switching this remember to add code in grapplingrope for animations
        if(Input.GetKeyDown(grappleKey) && Time.time > nextGrapple) 
        {
            nextGrapple = Time.time + grapplingCdTimer;
            cooldownImage.fillAmount = 0;
            StartGrapple();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(Input.GetMouseButtonDown(0) && swinging == false)
        {
           StartSwing(); 
        }
        else if(Input.GetMouseButtonDown(0) && swinging == true)
        {
            StopSwing();
        }
        //else if (Input.GetKeyDown(KeyCode.Q)) {
        //    swingJoint.maxDistance = 0f;
        //}
        //else if (Input.GetKeyUp(KeyCode.Q)) {
        //    swingJoint.maxDistance = 20f; //put whatever you want the "normal" distance to be here -- the distance when not reeled in
        //}
    }

    private void StartGrapple()
    {
        
        grappling = true;
        
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance))
        {
            grappleSound.clip = grappleHit;
            grappleSound.Play();
            grapplePoint = hit.point;
            //Freeze moved here so momentum isnt stopped if players grapple hits nothing
            pm.freeze = true;
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grappleSound.clip = grappleMiss;
            grappleSound.Play();
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }
        //grappleLineRender.enabled = true;
        //grappleLineRender.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos +overshootYAxis;

        if(grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(grapplePoint, highestPointOnArc);
        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        pm.freeze = false;
        grappling = false;

        //grapplingCdTimer = grapplingCd;

        //grappleLineRender.enabled = false;
    }

    public void StartSwing()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, whatIsGrappleable))
        {
            grappleSound.clip = SwingHit;
            grappleSound.Play();
            swinging = true;
            swingPoint = hit.point;
            swingJoint = player.gameObject.AddComponent<SpringJoint>();
            swingJoint.autoConfigureConnectedAnchor = false;
            swingJoint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            
            //Controls max swing joint distance
            swingJoint.maxDistance = distanceFromPoint * 0.8f;
            swingJoint.minDistance = 0f;//distanceFromPoint * 0.35f;

            swingJoint.spring = 4.5f;
            swingJoint.damper = 70f;
            swingJoint.massScale = 4.5f;
            //swingLineRenderer.positionCount = 2;
        }
    }
    //void drawRope()
    //{
    //    if(!swingJoint) return;

    //    swingLineRenderer.SetPosition(0, gunTip.position);
    //    swingLineRenderer.SetPosition(1, grapplePoint);
    //}
    public void StopSwing()
    {
        swinging = false;
        //swingLineRenderer.positionCount = 0;
        Destroy(swingJoint);
    }

    public bool IsSwinging()
    {
        return swingJoint != null;
    }

    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetSwingPoint()
    {
        return swingPoint;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

}
