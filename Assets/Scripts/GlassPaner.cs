using System.Collections;
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
        F_GlassActive(other);
    }

    public void F_GlassActive(Collider other)
    {
        PanelManager.Instance.F_Interaction(other, glassRb);
        StartCoroutine(UIManager.Instance.F_OnDeadUI());
    }
}