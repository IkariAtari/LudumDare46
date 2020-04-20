using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capacitor : PlaceableObject
{
    private int Energy = 1;

    public override void Upgrade()
    {
        base.Upgrade();

        Energy *= 2;
    }

    protected override void Activate()
    {
        GameManager.Instance.Earn(Energy);
    }

    public override void Clicked()
    {
        ShowInformation();
    }

    private void ShowInformation()
    {
        GameManager.Instance.ui.SetInformationPanelTowers(new string[]
        {
            transform.name,
            ModelNumber.ToString(),
            "N/a",
            Energy.ToString(),
            Speed.ToString(),
            UpgradeCost.ToString(),
            Name
        });
    }
}
