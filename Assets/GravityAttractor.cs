using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{

    [SerializeField] float gravity = -10f;
    
    public void Attract(Transform body, Rigidbody rb)
    {
        Vector3 targetDir = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        rb.AddForce(targetDir * gravity);
    }
}
