using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        /*
         * Draw two white discs around the NPC
         * one for attackRadius
         * one for followRadius
         * 
         * Also, if the target is within the attackRange,
         * Draw a line between the NPC and target.
         // */
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.attackRadius);
        Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.followRadius);
        if (fov.attackTarget == true)
        {
            Handles.DrawLine(fov.transform.position, fov.visibleTarget.position);
        }
    }

}
