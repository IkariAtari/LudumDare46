using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : PlaceableObject
{
    public int BaseDamage;
    public float BaseRange;

    public GameObject Gun;
    public GameObject LaserPoint;

    public bool GunAiming;

    public List<GameObject> Enemies = new List<GameObject>();

    public LineRenderer Line;

    private Vector3 VecNull = new Vector3(100f, 100f, 100f);

    private GameObject RadiusSphere;

    protected override void Start()
    {
        base.Start();

        RadiusSphere = transform.GetChild(0).gameObject;

        GetComponent<SphereCollider>().radius = BaseRange;

        LaserPoint = model.transform.GetChild(0).GetChild(0).gameObject;

        Line.positionCount = 2;

        HideRadiusSphere();
    }

    protected override void Update()
    {
        base.Update();

        if(GunAiming)
        {
            model.transform.GetChild(0).Find("Gun").transform.LookAt(Enemies[0].transform.position);
        }
        else
        {
            if(Enemies.Count > 0)
            {
                Line.SetPositions(new Vector3[] {LaserPoint.transform.position, Enemies[0].transform.position});
            }
            else
            {
                Line.SetPositions(new Vector3[] {VecNull, VecNull});
            }
        }        
    }

    protected override void Activate()
    {
        if(Enemies.Count > 0)
        {
            if(Enemies[0].GetComponent<Enemy>().Damage(BaseDamage))
            {
                Enemies[0].GetComponent<Enemy>().DestroySelf();
                Enemies.Remove(Enemies[0]);
            }
        }
    }
    
    public override void Upgrade()
    {
        base.Upgrade();
        BaseDamage *= 2;
        BaseRange *= 1.3f;
    }

    private void OnTriggerEnter(Collider _col)
    {
        if(_col.transform.tag == "Enemy")
        {
            Enemies.Add(_col.gameObject);
        }
    }

    private void OnTriggerExit(Collider _col)
    {
        if(_col.transform.tag == "Enemy")
        {
            Enemies.Remove(_col.gameObject);
        }
    }

    protected override void ChangeModels()
    {
        base.ChangeModels();

        LaserPoint = model.transform.GetChild(0).GetChild(0).gameObject;
    }

    public override void Clicked()
    {
        ShowInformation();
        ShowRadiusSphere();
    }

    public override void UnClicked()
    {
        HideRadiusSphere();
    }

    private void ShowInformation()
    {
        GameManager.Instance.ui.SetInformationPanelTowers(new string[]
        {
            transform.name,
            ModelNumber.ToString(),
            BaseDamage.ToString(),
            "N/a",
            Speed.ToString(),
            UpgradeCost.ToString(),
            Name
        });
    }

    public void ShowRadiusSphere()
    {
        RadiusSphere.transform.localScale = new Vector3(BaseRange * 2, 0.1f, BaseRange * 2);
    }

    public void HideRadiusSphere()
    {
        RadiusSphere.transform.localScale = new Vector3(0, 0, 0);
    }

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, BaseRange);

        if(Enemies != null)
        {
            foreach(GameObject _enemy in Enemies)
            {
                Gizmos.DrawLine(transform.position, _enemy.transform.position);
            }
        }
    }
    
}
