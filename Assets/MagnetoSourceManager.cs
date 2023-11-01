using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetoSourceManager : MonoBehaviour
{
    public GameObject magnetoSphere;
    public void HandleSpawnMagnetSphere()
    {
        GameObject visuals = Instantiate(magnetoSphere);
        visuals.transform.SetParent(transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;
        Debug.Log("yo");  
    }
}
