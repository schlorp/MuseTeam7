using UnityEngine.Audio;
using System;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static audioManager instance;
    // Start is called before the first frame update
    void Awake ()
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
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
		}
    }

	private void Start()
	{
        Play("Theme");
	}

	public void Play (string name)
	{
        Sounds s = Array.Find(sounds, sounds => sounds.ClipName == name);
        if ( s == null)
		{
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
		}

        s.source.Play();
	}
}
