using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    void Awake()
    {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Sun"); // Recherche d'objets avec le tag spécifique

            if (objs.Length > 1) // Vérifier s'il y a déjà des objets persistants présents
            {
                // Si oui, détruire les doublons
                for (int i = 1; i < objs.Length; i++)
                {
                    Destroy(objs[i]);
                }
            }
            DontDestroyOnLoad(this.gameObject);
    }
}
