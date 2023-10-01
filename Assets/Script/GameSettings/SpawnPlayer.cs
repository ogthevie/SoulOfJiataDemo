using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }
}
