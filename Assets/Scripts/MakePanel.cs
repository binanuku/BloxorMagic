using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePanel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PanelManager.Instance.F_MakePanel(other);
    }
}
