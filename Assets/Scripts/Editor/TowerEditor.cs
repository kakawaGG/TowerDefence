using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(LevelScriptEditor))]
public class TowerEditor : Editor
{
    
    public class LevelScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Tower myTarget = (Tower)target;

        }
    }
}
