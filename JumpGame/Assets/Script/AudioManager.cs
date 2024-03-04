using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public GameObject prefab;

    public AudioClip background;
    public AudioSource backgroundsource;
    public AudioClip jump;
    public AudioSource jumpsource;
    private void Awake()
    {
        instance = this;
    }
    
    public void PlaySound(AudioClip clip, float volume, bool isLoopback)
    {
        if(clip == this.background)
        {
            Play(clip, ref backgroundsource, volume, isLoopback);
        }
    }
    public void PlaySound(AudioClip clip, float volume)
    {
        if(clip == this.jump)
        {
            Play(clip, ref jumpsource, volume);
            return;
        }
    }

    private void Play(AudioClip clip, ref AudioSource audioSource, float volume, bool isLoopback = false)
    {
        if(audioSource != null && audioSource.isPlaying)
        {
            return;
        }
        audioSource = Instantiate(instance.prefab).GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.loop = isLoopback;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void StopSound(AudioClip clip)
    {
        if(clip == this.jump)
        {
            jumpsource.Stop();
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
