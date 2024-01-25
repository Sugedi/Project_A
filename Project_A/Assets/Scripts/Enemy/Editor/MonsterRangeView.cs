using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class MonsterRangeView : Editor
{
    private void OnSceneGUI()
    {
        Enemy mon = (Enemy)target;
        Handles.color = Color.red;

        Handles.DrawWireArc(mon.homePosition, Vector3.up, Vector3.forward, 360f, mon.chaseRange);
    }
}