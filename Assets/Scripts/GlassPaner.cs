using UnityEngine;

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