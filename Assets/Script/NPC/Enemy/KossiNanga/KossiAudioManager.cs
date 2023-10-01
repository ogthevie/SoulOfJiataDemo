using UnityEngine;

public class KossiAudioManager : EnemyAudioManager
{
    private void Awake() 
    {
        enemyAudioSource = GetComponent<AudioSource>();
    }
}
