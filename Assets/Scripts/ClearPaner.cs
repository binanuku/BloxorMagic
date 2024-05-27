using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPaner : PanelManager
{
    Rigidbody clearRb;

    private void Start()
    {
        clearRb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        F_Interaction(collision, clearRb);
    }
}
