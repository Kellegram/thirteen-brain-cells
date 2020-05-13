using System;
using System.Collections.Generic;
using UnityEngine;
public class CameraSwitcher : MonoBehaviour
{
    [Serializable]
    public class CameraShortcut
    {
        public Camera camera;
        public KeyCode key;
    }
    public List<CameraShortcut> cameraShortcuts = new List<CameraShortcut>();

    /*
     * Update() is called every frame
     * Checks if someone has pressed the assigned camera switch key, and switches cameras.
     */
    private void Update()
    {
        foreach (CameraShortcut cs in cameraShortcuts)
        {
            if (Input.GetKey(cs.key))
            {
                foreach (CameraShortcut cs2 in cameraShortcuts)
                    cs2.camera.enabled = cs2.camera == cs.camera;
                break;
            }
        }
    }
}
