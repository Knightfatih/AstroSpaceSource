using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip npcHit;
    [SerializeField] AudioClip npcDestroy;
    [SerializeField] AudioClip npcFireShot;
    [SerializeField] AudioClip bossFireShot;
    [SerializeField] AudioClip bossDestroy;
    [SerializeField] AudioClip playerFireShot;
    [SerializeField] AudioClip playerDestroy;
    [SerializeField] AudioClip collectableObjects;

    AudioSource AS;

    float defaultLevel = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void AudioNPCHit()
    {
        AS.PlayOneShot(npcHit, defaultLevel);
    }
    
    public void AudioNPCDestroy()
    {
        AS.PlayOneShot(npcDestroy, 0.5f);
    }

    public void AudioNPCFireShot()
    {
        AS.PlayOneShot(npcFireShot, defaultLevel);
    }

    public void AudioBossFireShot()
    {
        AS.PlayOneShot(bossFireShot, defaultLevel);
    }

    public void AudioBossDestroy()
    {
        AS.PlayOneShot(bossDestroy, 0.4f);
    }

    public void AudioPlayerFireShot()
    {
        AS.PlayOneShot(playerFireShot, defaultLevel/5f);
    }
   
    public void AudioPlayerDestroy()
    {
        AS.PlayOneShot(playerDestroy, 0.4f);
    }
    
    public void AudioCollectableObjects()
    {
        AS.PlayOneShot(collectableObjects, 0.5f);
    }
}
