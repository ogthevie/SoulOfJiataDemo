using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class TrainingDoorManager : MonoBehaviour
{
    GameManager gameManager;
    PlayerManager playerManager;
    [SerializeField] GameObject spawnPoint;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerManager =FindObjectOfType<PlayerManager>();
    }

    /*private void Update() 
    {
        float doorDistance, maxdoorDistance;
        maxdoorDistance = 134f;

        doorDistance = Vector3.Distance(spawnPoint.transform.position, playerManager.transform.position);
        Debug.Log(doorDistance);        
    }*/

    void OnTriggerEnter(Collider collider)
    {
      float doorDistance, maxdoorDistance;
      maxdoorDistance = 208;

      doorDistance = Vector3.Distance(spawnPoint.transform.position, playerManager.transform.position);
      if(doorDistance > maxdoorDistance) StartCoroutine(gameManager.ZoneEntry("...RIVE SACREE..", " "));
      else StartCoroutine(gameManager.ZoneEntry("...SIBONGO..", " "));
    }
}
