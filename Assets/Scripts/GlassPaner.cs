using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlassPaner : MonoBehaviour
{
    Rigidbody glassRb;

    private void Start()
    {
        glassRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PanelManager.Instance.F_Interaction(other, glassRb);
    }
}