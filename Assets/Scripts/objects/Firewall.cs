using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : PlaceableObject
{
    public int Damage;
    public int Uses;

    private void OnTriggerEnter(Collider _col)
    {
        if(Uses > 0)
        {
            if(_col.transform.tag == "Enemy")
            {
                if(_col.gameObject.GetComponent<Enemy>().Damage(Damage))
                {
                    _col.gameObject.GetComponent<Enemy>().DestroySelf();
                }
                
                Uses--;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
