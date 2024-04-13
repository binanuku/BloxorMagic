using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void F_Interaction(Collision col, Rigidbody rb) {
        if (col.gameObject.CompareTag("Player"))
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }
}
