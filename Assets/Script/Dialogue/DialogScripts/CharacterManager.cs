using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterManager : MonoBehaviour
{
    protected SibongoManager sibongoManager;
    protected Animator characterAnim;
    protected AudioSource characterAudioSource;
    protected StoryManager storyManager;
    public int dayPeriod;
    public GameObject questCursor;
    public Vector3 [] characterpositions = new Vector3[5];
    public Quaternion[] characterRotation = new Quaternion[5];
    public List<int> levelStoryActions = new ();


    protected virtual void Awake()
    {
        sibongoManager = FindFirstObjectByType<SibongoManager>();
        characterAnim = GetComponent<Animator>();
        storyManager = FindFirstObjectByType<StoryManager>();
        if(questCursor != null) questCursor.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        dayPeriod = sibongoManager.dayPeriod;
        characterAnim.SetInteger("dayPeriod", dayPeriod);
        FixedCursorPosition();
    }

    public virtual void DayJob(Vector3 mornPose, Quaternion mornRot)
    {
        transform.position = mornPose;
        transform.rotation = mornRot; 
    }

    public virtual void FixedCursorPosition()
    {
        if(levelStoryActions.Count > 0 && questCursor != null)
        {
            if(levelStoryActions.Contains(storyManager.storyStep))
            {
                GameManager gameManager = storyManager.GetComponent<GameManager>();
                StartCoroutine(gameManager.StartHandleToDo(storyManager.storyStep));
                questCursor.SetActive(true);
            } 
            else questCursor.SetActive(false);
        }        
    }
}
