using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPaner : PanelManager
{
    Rigidbody glassRb;

    private void Start()
    {
        glassRb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        F_Interaction(collision, glassRb);
    }
}