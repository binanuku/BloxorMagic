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

    private void OnTriggerEnter(Collider other)
    {
        F_Interaction(other, clearRb);
        UIManager.Instance.F_OnClearUI();
    }
}
