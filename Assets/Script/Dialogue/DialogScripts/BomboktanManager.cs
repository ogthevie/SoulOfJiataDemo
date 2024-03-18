using System.Collections;
using System.Collections.Generic;
using SJ;
using UnityEngine;

public class BomboktanManager : CharacterManager
{
    GameObject thunderBomboktan;
    //Faire pop up le bomboktan à des zones précises, à des moments précis.
            /// evenement surcharge
            /// evenement sommeil
            /// evenement stele
            //il va se tranformer en bigkossi
    //lires les bons dialogues

    PlayerManager playerManager;

    protected override void Awake()
    {
        characterAnim = GetComponent<Animator>();
        playerManager = FindObjectOfType<PlayerManager>();
        characterAudioSource = GetComponent<AudioSource>();
        thunderBomboktan = transform.GetChild(2).gameObject;   
    }

    protected override void Start(){}


    public void Spawn(int idStoryB)
    {
        StartCoroutine(SpawnBomboktan(idStoryB));
    }

    IEnumerator SpawnBomboktan(int id)
    {
        yield return new WaitForSeconds(6f);
        thunderBomboktan.SetActive(true);
        characterAudioSource.Play();
        yield return new WaitForSeconds(0.2f);
        DayJob(characterpositions[id], characterRotation[id]);
        yield return new WaitForSeconds(1.5f);
        thunderBomboktan.SetActive(false);

    }


}
