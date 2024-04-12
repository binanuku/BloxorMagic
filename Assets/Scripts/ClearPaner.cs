using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearPaner : PanelManager
{
    public override void F_Interaction()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        F_Interaction();
    }
}
