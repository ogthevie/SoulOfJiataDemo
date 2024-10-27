using System.Collections;
using SJ;
using UnityEngine;

public class BomboktanManager : CharacterManager
{
    [SerializeField] GameObject spawnFx, auraGround;
    SkinnedMeshRenderer bombSkinnedMeshRenderer;
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
        playerManager = FindFirstObjectByType<PlayerManager>();
        storyManager = FindFirstObjectByType<StoryManager>();
        characterAudioSource = GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        if(storyManager.storyStep < 3) return;
        int id = GetComponent<BomboktanTriggerManager>().idDialog;
        DayJob(characterpositions[id], characterRotation[id]);
    }

    public IEnumerator SpawnBomboktan(int id)
    {
        GetComponent<BomboktanTriggerManager>().idDialog = id;
        yield return new WaitForSeconds(3f);
        spawnFx.SetActive(true);
        characterAudioSource.Play();
        yield return new WaitForSeconds(0.2f);
        DayJob(characterpositions[id], characterRotation[id]);
        bombSkinnedMeshRenderer.enabled = true;
        yield return new WaitForSeconds(1.5f);
        spawnFx.SetActive(false);

    }
}
