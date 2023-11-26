using SJ;
using UnityEngine;

public class PlayerInstanceManager : MonoBehaviour
{
    private static GameObject persistentObject;

    public GameObject objectToPersist; // Cet objet sera préservé entre les scènes

    public void Awake()
    {
        if (persistentObject == null)
        {
            // Si aucun objet persistant n'existe, créer un nouvel objet persistant
            persistentObject = objectToPersist;
            DontDestroyOnLoad(persistentObject);
        }
        else
        {
            // S'assurer que seul un objet persistant existe, détruire les doublons
            if (persistentObject != objectToPersist)
            {
                Destroy(objectToPersist);
            }
        }
    }
}

