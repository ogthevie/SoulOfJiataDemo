using UnityEngine;
using SJ;

public class BaseDoorManager : MonoBehaviour 
{
    StoryManager storyManager;
    [SerializeField] GameObject baseWallOne, baseWallTwo, player;
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
    }

    void CheckDistance()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        
    }

    void ActivateBaseDoorwall()
    {
        if(distance <= 30)
        {
            //baseWallOne.SetActive(true);
            //baseWallTwo.SetActive(true);
            anim.Play();
            Destroy(this);
        }
    }
    
}

