using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject Node;
    public float Height;

    public float MaxHealth;
    private float Health;
    [SerializeField] private Image HealthBarFill;

    public int Usage;

    public float Speed;

    private Vector3 pos;

    private Vector3 next_Node_pos;

    [SerializeField] private GameObject DestroyParticles;

    private void Start()
    {
       next_Node_pos = new Vector3(Node.GetComponent<Node>().NextNodes[0].transform.position.x, Height, Node.GetComponent<Node>().NextNodes[0].transform.position.z);
       MaxHealth *= (float)EnemyManager.Wave / 2;
       Health = MaxHealth;
    }

    private void Update()
    {
        if(!Node.GetComponent<Node>().NextNodes[0].GetComponent<Node>().EndNode)
        {
            pos = new Vector3(transform.position.x, Height, transform.position.z);
        
            transform.position = Vector3.MoveTowards(pos, next_Node_pos, Speed * Time.deltaTime);

            if(Vector3.Distance(pos, next_Node_pos) < 0.1f)
            {
                Node = Node.GetComponent<Node>().NextNodes[0];
                next_Node_pos = new Vector3(Node.GetComponent<Node>().NextNodes[0].transform.position.x, Height, Node.GetComponent<Node>().NextNodes[0].transform.position.z);   
            }
        }
        else
        {
            GameManager.Instance.SetUsage(Usage);
            TakeOver();
        }
    }

    public bool Damage(int _damage)
    {
        Health -= _damage;
        Debug.Log(Health / MaxHealth);
        HealthBarFill.fillAmount = Health / MaxHealth;

        if(Health < 0)
        {
            return true;
        }

        return false;
    }

    public void DestroySelf()
    {
        GameManager.Instance.AddScore((int)Mathf.CeilToInt(MaxHealth * 4));
        Instantiate(DestroyParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void TakeOver()
    {
        Instantiate(DestroyParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
