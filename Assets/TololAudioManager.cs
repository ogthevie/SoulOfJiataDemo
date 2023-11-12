using System.Collections.Generic;
using UnityEngine;

public class TololAudioManager : MonoBehaviour
{
    public AudioSource enemyAudioSource;
    public List<AudioClip> enemySfx = new ();

    void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
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
