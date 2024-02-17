using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDelete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyExplosion", 4.0f);
    }

    void DestroyExplosion()
    {
        Destroy(this.gameObject);
    }
}
