using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class audioManager : MonoBehaviour
{
    public Sounds[] sounds;
    public AudioClip[] aSounds;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private AudioSource _source;

    public static audioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sounds s in sounds)
        {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            audioSources.Add(a);
            s.source = a;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Theme");
        _source = GetComponent<AudioSource>();

    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.ClipName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void RandomSounds()
    {
        _source.clip = aSounds[UnityEngine.Random.Range(0, sounds.Length)];
        _source.Play();
    }

    
    public void ResumeMusic()
	{
        foreach (AudioSource a in audioSources)
        {
            a.UnPause();
        }
    }

    public void PauseMusic()

    { 
        foreach(AudioSource a in audioSources)
		{
            a.Pause();
		}
    }

    public void Stop()
	{
        foreach (AudioSource a in audioSources)
        {
            a.Stop();
        }
    }
}