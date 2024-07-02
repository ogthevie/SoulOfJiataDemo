using System.Collections;
using SJ;
using UnityEngine;

public class BomboktanManager : CharacterManager
{
    GameObject thunderBomboktan;
    SkinnedMeshRenderer bombSkinnedMeshRenderer;
    GameObject bExplosionFx, auraGround;
    //Faire pop up le bomboktan à des zones précises, à des moments précis.
            /// evenement surcharge
            /// evenement sommeil
            /// evenement stele
            //il va se tranformer en bigkossi
    //lires les bons dialogues

    PlayerManager playerManager;

    protected override void Awake()
    {
        bombSkinnedMeshRenderer = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        characterAnim = GetComponent<Animator>();
        playerManager = FindObjectOfType<PlayerManager>();
        storyManager = FindObjectOfType<StoryManager>();
        characterAudioSource = GetComponent<AudioSource>();
        thunderBomboktan = transform.GetChild(2).gameObject;
        bExplosionFx = transform.GetChild(4).gameObject;
        auraGround = transform.GetChild(3).gameObject;
    }

    protected override void Start()
    {
        if(storyManager.storyStep < 3) return;
        int id = GetComponent<BomboktanTriggerManager>().idDialog;
        DayJob(characterpositions[id], characterRotation[id]);
    }


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

    public void DisappearBomboktan()
    {
        StartCoroutine(DisableBomboktan());
    }

    IEnumerator DisableBomboktan()
    {
        bExplosionFx.SetActive(true);
        yield return new WaitForSeconds (0.1f);
        bombSkinnedMeshRenderer.enabled = false;
        auraGround.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds (4.8f);
        this.gameObject.SetActive(false);
    }


}
