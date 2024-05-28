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

    private void OnTriggerEnter(Collider other)
    {
        F_Interaction(other, glassRb);
        UIManager.Instance.F_OnClearUI();
    }
}