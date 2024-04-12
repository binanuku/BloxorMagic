using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPaner : PanelManager
{
    public override void F_Interaction()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        F_Interaction();
    }
}