using UnityEngine;
using SJ;

public class BaseDoorManager : MonoBehaviour 
{
    StoryManager storyManager;
    [SerializeField] GameObject baseWallOne, baseWallTwo, player,spk1, spk2;
    [SerializeField] Animation anim;
    bool isFight;
    float distance;

    void Awake()
    {
        storyManager = FindObjectOfType<StoryManager>();
    }

    void Start()
    {
        baseWallOne.SetActive(false);
        baseWallTwo.SetActive(false);

        if(storyManager.storyStep >2)
        {
            transform.position = new Vector3 (transform.position.x, 3.4f, transform.position.z);
            Destroy(spk1);
            Destroy(spk2);
            Destroy(this);
        }
        else
        {
            player = FindObjectOfType<PlayerManager>().gameObject;
            anim = GetComponent<Animation>();
        }

    }

    void LateUpdate()
    {
        CheckDistance();
        ActivateBaseDoorwall();
        DesactivateBaseDoorWall();
    }

    void CheckDistance()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        
    }

    void ActivateBaseDoorwall()
    {
        if(distance <= 30 && !isFight)
        {
            baseWallOne.SetActive(true);
            baseWallTwo.SetActive(true);
            spk1.SetActive(true);
            spk2.SetActive(true);
            isFight = true;
        }
    }

    void DesactivateBaseDoorWall()
    {
        if(spk1 == null && spk2 == null)
        {
            baseWallOne.SetActive(false);
            baseWallTwo.SetActive(false);
            anim.Play();
            Destroy(this);            
        }
    }
    
}

