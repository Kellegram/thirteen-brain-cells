using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //An array of Sound's using our custom Sound class. See the sound class for more information
    public Sound[] sounds;

    public static AudioManager instance;
    
    /*
    The Awake function gets called when the AudioManager is awoken.
    See Unity's documentation for more information.
    // */ 
    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        //Loops through every Sound object in the sounds array and makes a definition for its AudioSource
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        //Play the theme when game starts
        Play("theme");
    }

    /*
    public void Play() is a custom functiom which takes in a string and finds it in the sounds array, then
    then Plays it if it's not null.

    If it is null, you will receive an error in the console.

    You can call this function from any script by doing 
    // */
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Bruh moment. You tried to play " + name + " but " + name + " doesn't exist...");
            return;
        }
            
        s.source.Play();
    }
}
