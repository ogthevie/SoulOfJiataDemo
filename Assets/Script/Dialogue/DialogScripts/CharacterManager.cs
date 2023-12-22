using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterManager : MonoBehaviour
{
    protected SibongoManager sibongoManager;
    protected Animator characterAnim;
    protected AudioSource characterAudioSource;
    protected int dayPeriod;
    public Vector3 [] characterpositions = new Vector3[5];
    public Quaternion[] characterRotation = new Quaternion[5];


    protected virtual void Awake()
    {
        sibongoManager = FindObjectOfType<SibongoManager>();
        characterAnim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        dayPeriod = sibongoManager.dayPeriod;
        characterAnim.SetInteger("dayPeriod", dayPeriod);
    }

    protected virtual void DayJob(Vector3 mornPose, Quaternion mornRot)
    {
        
        transform.position = mornPose;
        transform.rotation = mornRot; 
    }
}
