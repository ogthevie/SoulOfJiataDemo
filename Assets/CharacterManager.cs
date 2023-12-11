using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterManager : MonoBehaviour
{
    protected SibongoManager sibongoManager;
    protected Animator characterAnim;
    protected int dayPeriod;
    public Vector3 [] characterpositions = new Vector3[5];
    public Quaternion[] characterRotation = new Quaternion[5];


    void Awake()
    {
        sibongoManager = FindObjectOfType<SibongoManager>();
        characterAnim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        dayPeriod = sibongoManager.dayPeriod;
        characterAnim.SetInteger("dayPeriod", dayPeriod);
        Debug.Log(dayPeriod);
    }

    protected virtual void dayJob(Vector3 mornPose, Quaternion mornRot)
    {
        
        transform.position = mornPose;
        transform.rotation = mornRot; 
    }
}
