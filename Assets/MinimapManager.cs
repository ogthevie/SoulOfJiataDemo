using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] Transform player;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MiniMapCam"); // Recherche d'objets avec le tag spécifique

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

    void Update()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
