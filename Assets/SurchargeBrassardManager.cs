using UnityEngine;
using SJ;


public class SurchargeBrassardManager : MonoBehaviour
{
    PlayerManager playerManager;
    InputManager inputManager;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        inputManager = FindObjectOfType<InputManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3)
        {
            playerManager.brasL.GetComponent<SkinnedMeshRenderer>().enabled = true;
            playerManager.brassardL.GetComponent<SkinnedMeshRenderer>().enabled = true;
            //Tutoscreen
            inputManager.jiatastats.canSurcharge = true;
            Destroy(this.gameObject);
        }

        
    }
}
