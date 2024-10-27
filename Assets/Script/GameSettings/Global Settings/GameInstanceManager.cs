using UnityEngine;

public class GameInstanceManager : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] gameManagerInstances = GameObject.FindGameObjectsWithTag("GameManager");

        if (gameManagerInstances.Length > 2)
        {
            for (int i = 1; i < gameManagerInstances.Length; i++)
            {
                Destroy(gameManagerInstances[i]);
            }
        }

        GameObject[] jiataInstances = GameObject.FindGameObjectsWithTag("Player");

        if (jiataInstances.Length > 2)
        {
            for (int i = 1; i < jiataInstances.Length; i++)
            {
                Destroy(jiataInstances[i]);
            }
        }

        GameObject[] cameraInstances = GameObject.FindGameObjectsWithTag("MainCamera");

        if (cameraInstances.Length > 2)
        {
            for (int i = 1; i < cameraInstances.Length; i++)
            {
                Destroy(cameraInstances[i]);
            }
        }   

        GameObject[] globalVolumeInstances = GameObject.FindGameObjectsWithTag("Volume");

        if (globalVolumeInstances.Length > 2)
        {
            for (int i = 1; i < globalVolumeInstances.Length; i++)
            {
                Destroy(globalVolumeInstances[i]);
            }
        }

        GameObject[] directionnalLightInsyances = GameObject.FindGameObjectsWithTag("Sun");

        if (directionnalLightInsyances.Length > 2)
        {
            for (int i = 1; i < directionnalLightInsyances.Length; i++)
            {
                Destroy(directionnalLightInsyances[i]);
            }
        }

        GameObject[] UIinstances = GameObject.FindGameObjectsWithTag("Playerui");

        if (UIinstances.Length > 2)
        {
            for (int i = 1; i < UIinstances.Length; i++)
            {
                Destroy(UIinstances[i]);
            }
        }
    }
}
