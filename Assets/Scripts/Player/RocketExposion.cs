using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExposion : MonoBehaviour
{
    public float blastRadius;
    public float explosionForce;
    private Collider[] hitColliders;
    public GameObject explosion;
    public LayerMask rocketHittableLayer;
    void OnCollisionEnter(Collision collision)
    {
        Explode(collision.contacts[0].point);
    }
    //Rocket Explosion on Impact Function
    void Explode(Vector3 explosionPoint)
    {
        hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius);
        Instantiate(explosion, explosionPoint, Quaternion.identity);
        foreach(Collider hitcol in hitColliders)
        {
            if(hitcol.GetComponent<Rigidbody>() != null)
            {
                hitcol.GetComponent<Rigidbody>().isKinematic = false;
                hitcol.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPoint, blastRadius, 1f, ForceMode.Impulse);
                if(hitcol.gameObject.tag=="Box")
                {
                    //If box is hit activates boxhit function
                    BoxHit boxHit = hitcol.gameObject.GetComponent<BoxHit>();
                    boxHit.boxIsHit();
                    boxHit.hasHit = true;
                }
            }
        }
        Destroy(this.gameObject);
    }
    
    
}
