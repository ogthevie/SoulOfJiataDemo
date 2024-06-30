using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class NotifDoorManager : MonoBehaviour
{
    GameManager gameManager;
    PlayerManager playerManager;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float maxDoorDistance;
    [SerializeField] string firstZone;
    [SerializeField] string secondZone;
    [SerializeField] string generalZone;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerManager =FindObjectOfType<PlayerManager>();
    }

    /*private void Update() 
    {
        float doorDistance;
        doorDistance = Vector3.Distance(spawnPoint.transform.position, playerManager.transform.position);
        Debug.Log(doorDistance);        
    }*/

    void OnTriggerEnter(Collider collider)
    {
      float doorDistance;

      doorDistance = Vector3.Distance(spawnPoint.transform.position, playerManager.transform.position);
      if(doorDistance > maxDoorDistance) StartCoroutine(gameManager.ZoneEntry(firstZone, generalZone));
      else StartCoroutine(gameManager.ZoneEntry(secondZone, generalZone));
    }
}
