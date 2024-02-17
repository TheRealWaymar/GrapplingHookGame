using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Controls the animation of the grapple hook, code for animation taken from this video
//https://www.youtube.com/watch?v=8nENcDnxeVE
//Modified to account for the difference between grappling and swinging in this game
public class GrapplingRope : MonoBehaviour
{
    private Spring swingSpring;
    private Spring grappleSpring;
    public LineRenderer lrSwing;
    public LineRenderer lrGrapple;
    private Vector3 currentGrapplePosition;
    private Vector3 currentSwingPosition;
    public grappleController grappleGun;
    //Quality of Animation
    public int quality;
    //How long it takes for rope to go from wiggly to taut
    public float damper;
    //How Taught the rope becomes, low leaves it wiggly, high makes it straight
    public float strength;
    //The force of the rope ejecting from gun, more wiggly at launch basically
    public float velocity;
    //How many wiggly waves appear from launch
    public float waveCount;
    //How wide the wiggly waves radiate outwards as they fly
    public float waveHeight;
    public AnimationCurve affectCurve;
    void Awake()
    {
        swingSpring = new Spring();
        swingSpring.SetTarget(0);
        grappleSpring = new Spring();
        grappleSpring.SetTarget(0);
    }

    //Constantly runs animation functions in late update to fire after raycasts in grappleController
    void LateUpdate()
    {
            drawRopeSwing();
            drawRopeGrapple();
    }


//Animation Code when swinging
    void drawRopeSwing()
    {
        if(!grappleGun.IsSwinging())
        {
            currentSwingPosition = grappleGun.gunTip.position;
            swingSpring.Reset();
            if(lrSwing.positionCount > 0)
            {
                lrSwing.positionCount = 0;
            }
            return;
        }

        if(lrSwing.positionCount == 0)
        {
            swingSpring.SetVelocity(velocity);
            lrSwing.positionCount = quality + 1;
        }
        swingSpring.SetDamper(damper);
        swingSpring.SetStrength(strength);
        swingSpring.Update(Time.deltaTime);

        var swingPoint = grappleGun.GetSwingPoint();
        var swingGunTipPosition = grappleGun.gunTip.position;
        var swingUp = Quaternion.LookRotation(swingPoint - swingGunTipPosition).normalized * Vector3.up;

        currentSwingPosition = Vector3.Lerp(currentSwingPosition, swingPoint, Time.deltaTime * 12f);

        for(var i = 0; i < quality +1; i++)
        {
            var swingDelta = i / (float) quality;
            var swingOffset = swingUp * waveHeight * Mathf.Sin(swingDelta * waveCount * Mathf.PI) 
            * swingSpring.Value * affectCurve.Evaluate(swingDelta);

            lrSwing.SetPosition(i, Vector3.Lerp(swingGunTipPosition, currentSwingPosition, swingDelta) + swingOffset);
        }

    }

//Animation Code when grappling
    void drawRopeGrapple()
    {
        if(!grappleGun.IsGrappling())
        {
            currentGrapplePosition = grappleGun.gunTip.position;
            grappleSpring.Reset();
            if(lrGrapple.positionCount > 0)
            {
                lrGrapple.positionCount = 0;
            }
            return;
        }

        if(lrGrapple.positionCount == 0)
        {
            grappleSpring.SetVelocity(velocity);
            lrGrapple.positionCount = quality + 1;
        }
        grappleSpring.SetDamper(damper);
        grappleSpring.SetStrength(strength);
        grappleSpring.Update(Time.deltaTime);

        var grapplePoint = grappleGun.GetGrapplePoint();
        var gunTipPosition = grappleGun.gunTip.position;
        var up = Quaternion.LookRotation(grapplePoint - gunTipPosition).normalized * Vector3.up;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);

        for(var i = 0; i < quality +1; i++)
        {
            var delta = i / (float) quality;
            var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) 
            * grappleSpring.Value * affectCurve.Evaluate(delta);

            lrGrapple.SetPosition(i, Vector3.Lerp(gunTipPosition, currentGrapplePosition, delta) + offset);
        }

    }
}
