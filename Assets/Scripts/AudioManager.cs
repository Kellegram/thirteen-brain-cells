using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //An array of Sound's using our custom Sound class. See the sound class for more information
    public Sound[] sounds;
    public static AudioManager instance;

    /*
     * The Awake function gets called when the AudioManager is awoken.
     * This function will loop through every Sound object in the sounds array and makes a definition for its AudioSource
     */
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLoop;
        }

        //Play the theme when game starts
        Play("theme");
    }

    /*
     * public void Play() is a custom functiom which takes in a string and finds it in the sounds array, then
     * then Plays it if it's not null.
     *
     * If it is null, you will receive an error in the console.
     *
     * You can call this function from any script by doing FindObjectOfType<AudioManager>().Play("SoundName");
     */
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
