using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public string Name;
    public int Cost;

    public int UpgradeCost;

    protected GameObject model;
    public int ModelNumber;

    public int Speed;

    public bool Upgradeable = true;

    [SerializeField] protected int timer;

    [SerializeField] private GameObject[] models;

    protected virtual void Start()
    {
        if(Upgradeable)
        {
            ModelNumber = 0;
            model = Instantiate(models[ModelNumber]);
            model.transform.SetParent(this.transform);
            model.transform.localPosition = new Vector3(0, 0.3f, 0);
        }
    }

    protected virtual void Update()
    { 
        if(timer <= Speed)
        {
            timer++;
        }
        else
        {
            Activate();

            timer = 0;
        }
    }

    public virtual void Upgrade()
    {
        if(ModelNumber <= 4)
        {
            ModelNumber += 1;
            UpgradeCost *= 4;
        
            double _Speed = (double)Speed;
            _Speed *= 0.95;
            Speed = (int)System.Math.Ceiling(_Speed);
        }

        if(Upgradeable)
        {
            ChangeModels();
        }
    }

    protected virtual void Activate()
    {
        
    }

    public void Move(Vector3 _new_pos)
    {
        StartCoroutine(MoveE(_new_pos));
    }

    private IEnumerator MoveE(Vector3 _new_pos)
    {
        float i = 0;

        while(i <= 1)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _new_pos, Mathf.SmoothStep(0, 1, i));
            i += 0.01f;

            yield return null;
        }
    }

    protected virtual void ChangeModels()
    {
        Destroy(model);

        if(models[ModelNumber] != null)
        {
            model = Instantiate(models[ModelNumber]);
            model.transform.SetParent(this.transform);
            model.transform.localPosition = new Vector3(0, 0.3f, 0);
        }
        else
        {
            
        }
    }

    public virtual void Clicked()
    {

    }

    public virtual void UnClicked()
    {

    }
}
