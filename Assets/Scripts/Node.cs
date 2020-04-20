using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject[] NextNodes;
    public bool EndNode;
    
    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        for(int i = 0; i < NextNodes.Length; i++)
        {
            Gizmos.DrawLine(transform.position, NextNodes[i].transform.position);
        }
    }
}
