using System.Collections.Generic;
using UnityEngine;

public abstract class  EnemyAudioManager : MonoBehaviour
{
    //audiosource
    //sons de detection
    //sons d'attaque
    //son de mort
    //lire le son de detection
    //lire le son d'attaque
    //lire le son de mort

    public AudioSource enemyAudioSource;
    public List<AudioClip> enemySfx = new (); //0 -- detect' 1 -- att 2 -- mort

    public virtual void ReadDetectionFx()
    {
        enemyAudioSource.PlayOneShot(enemySfx[0]);
    }

    public virtual void ReadAttackFx()
    {
        enemyAudioSource.PlayOneShot(enemySfx[1]);
    }
    
}
