using UnityEngine;

public class KossiAudioManager : EnemyAudioManager
{
    [SerializeField] AudioSource kossiAudioSource;
    private void Awake() 
    {
        kossiAudioSource = GetComponent<AudioSource>();
    }

    public void InvokeKossiKaze()
    {
        kossiAudioSource.Play();
    }
}
