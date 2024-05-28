using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPaner : MonoBehaviour
{
    Rigidbody clearRb;

    private void Start()
    {
        clearRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PanelManager.Instance.F_Interaction(other, clearRb);
        UIManager.Instance.F_OnClearUI();
    }
}
