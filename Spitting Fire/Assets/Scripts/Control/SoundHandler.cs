using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Play firing and explosion sounds upon request
/// </summary>
public class SoundHandler : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip fireSoundEffect;
    [SerializeField] AudioClip ExplosionSoundEffect;
    // Start is called before the first frame update
   
    public void Fire()
    {

        if (source.clip == fireSoundEffect) return;
        source.clip = fireSoundEffect;
        source.Play();
    }

    public void Stop()
    {
        source.clip = null;
        source.Stop();
    }

    public void Explode()
    {
        AudioSource.PlayClipAtPoint(ExplosionSoundEffect, transform.position);
    }
}
