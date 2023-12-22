using System.Collections.Generic;
using UnityEngine;
using SJ;

public class KazeAudioManager : MonoBehaviour
{
    public AudioSource enemyAudioSource;
    public List<AudioClip> enemySfx = new ();

    void Start()
    {
        enemyAudioSource = GetComponentInParent<AudioSource>();
    }   
    
    public void ReadDead()
    {
        enemyAudioSource.PlayOneShot(enemySfx[0]);
    }

    public void ReadAttack()
    {
        enemyAudioSource.PlayOneShot(enemySfx[1]);
    }
}
