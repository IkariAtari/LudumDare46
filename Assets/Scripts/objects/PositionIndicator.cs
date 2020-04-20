using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionIndicator : PlaceableObject
{
    public bool Valid;

    protected override void Start()
    {
        base.Start();

        Valid = false;
    }

    private void OnTriggerStay(Collider _col)
    {
        if(_col.transform.tag == "GridNode")
        {
            Valid = true;
        }
    }
    private void OnTriggerExit(Collider _col)
    {
        Valid = false;
    }
}
