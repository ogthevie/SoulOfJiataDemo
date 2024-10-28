using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloAudioManager : MonoBehaviour
{
    public List<AudioClip> buffaloSfx = new ();
    [SerializeField] AudioSource buffaloAudioSource;

    public void ReadFootStep()
    {
        buffaloAudioSource.PlayOneShot(buffaloSfx[0]);
    }

    public void ReadBreath()
    {
        buffaloAudioSource.PlayOneShot(buffaloSfx[1]);
    }

    public void ReadRoar()
    {
        buffaloAudioSource.PlayOneShot(buffaloSfx[2]);
    }

    public void ReadDead()
    {
        buffaloAudioSource.PlayOneShot(buffaloSfx[3]);
    }

    public void TikarliburKao()
    {
        buffaloAudioSource.PlayOneShot(buffaloSfx[4]);
    }
}
