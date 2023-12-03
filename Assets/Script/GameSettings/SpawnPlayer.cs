using UnityEngine.SceneManagement;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    
    void Start()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Quaternion targetRotation;
        if(i == 1) targetRotation = Quaternion.Euler(0, 82f, 0f);
        else targetRotation = Quaternion.identity;
        player.transform.position = transform.position;
        player.transform.rotation *= targetRotation;

    }
}
