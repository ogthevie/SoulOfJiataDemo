using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;

        if(this.gameObject.name == "StartZoneSibongo")
        {
            GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>().enabled = true;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>().enabled = false;
        }
    }
}
