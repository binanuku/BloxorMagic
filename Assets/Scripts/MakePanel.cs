using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePanel : MonoBehaviour
{
    Rigidbody makeRb;

    private void Start()
    {
        makeRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PanelManager.Instance.F_CreatePanel(collision, makeRb);
    }
}
