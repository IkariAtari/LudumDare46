﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        GameManager _manager = (GameManager)target;
    }
}
