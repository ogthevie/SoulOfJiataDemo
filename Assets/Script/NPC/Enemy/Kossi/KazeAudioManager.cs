using UnityEngine;
using SJ;

public class KazeAudioManager : EnemyAudioManager
{
    AudioManager audioManager;
    private void Awake() 
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public override void ReadAttackFx()
    {
        audioManager.jiataAudioSource.PlayOneShot(enemySfx[1]);
    }
}
