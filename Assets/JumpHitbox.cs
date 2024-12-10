using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHitbox : MonoBehaviour
{
    public bool CanJump = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter()
    {
        CanJump = true;
    }

    private void OnTriggerExit()
    {
        CanJump = false;
    }
}
