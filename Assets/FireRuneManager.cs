using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class FireRuneManager : MonoBehaviour
{
    //verifier la collision
    //changer la couleur
    //instancier le brassard
    //pousser le brassard
    AudioManager audioManager;
    public Material originMaterial;
    public Material openMaterial;
    public GameObject surchargeBrassard;
    MeshRenderer meshRenderer;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        originMaterial = meshRenderer.material;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8) HandleActivateFireRune();
    }

    void HandleActivateFireRune()
    {
        meshRenderer.material = openMaterial;
        Instantiate(surchargeBrassard, transform.GetChild(0));
        Destroy(this);
        
    }
}
