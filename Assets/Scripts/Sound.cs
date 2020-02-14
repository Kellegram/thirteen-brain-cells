using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    //name of sound, can be set in editor
    public string name;

    //Sound file, can be set in editor
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    
    [Range(0.1f, 3f)]
    public float pitch;
    
    //AudioSource is a Unity class for handling Audio
    [HideInInspector]
    public AudioSource source;
}
