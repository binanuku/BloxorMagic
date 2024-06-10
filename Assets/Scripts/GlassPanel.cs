using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlassPanel : MonoBehaviour
{
    Rigidbody glassRb;

    private void Start()
    {
        glassRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PanelManager.Instance.F_GlassActive(other, glassRb);
    }
}