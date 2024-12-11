using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysObject : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Launch()
    {
        //rb.AddExplosionForce(10, transform.position, 10, 3.0F);
    }
}
