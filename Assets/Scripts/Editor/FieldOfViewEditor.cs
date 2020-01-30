using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.viewRadius);
        Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.followRadius);
        if (fov.attackTarget == true)
        {
            Handles.DrawLine(fov.transform.position, fov.visibleTarget.position);
        }
    }

}
