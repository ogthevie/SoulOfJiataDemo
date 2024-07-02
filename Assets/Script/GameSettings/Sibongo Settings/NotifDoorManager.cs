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
    [SerializeField] string generalZone;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerManager =FindObjectOfType<PlayerManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer != 3) return;

        float doorDistance;
        doorDistance = Vector3.Distance(spawnPoint.transform.position, playerManager.transform.position);
        //Debug.Log(doorDistance);    
        if(doorDistance > maxDoorDistance) StartCoroutine(gameManager.ZoneEntry(firstZone, generalZone));
    }
}
