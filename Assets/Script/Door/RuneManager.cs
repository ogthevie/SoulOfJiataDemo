using UnityEngine;
using SJ;

[DefaultExecutionOrder(1)]
public class RuneManager : MonoBehaviour
{
    public int indexPower; //il y a 03 pouvoirs : 0 = kingTake, 1 = Shango breath, 2 = sky roar
    PlayerManager playerManager;
    [SerializeField] GameObject ringStele, powerFxGO;
    [SerializeField] GameObject [] powerFxGOs = new GameObject[3];
    [SerializeField] Material onMaterial;

    private void Start() 
    {  
        playerManager = FindFirstObjectByType<PlayerManager>();
        CheckPower();
    }

    private void CheckPower()
    {
        var materialRing = ringStele.GetComponent<MeshRenderer>().material;
        var materialPowerStone = GetComponent<MeshRenderer>().material;

        if(indexPower == 0)
        {
            if(playerManager.canSurcharge)
            {
                powerFxGO.SetActive(false);
                Destroy(this,  0.2f);
            } 
        }
        else if(indexPower == 1)
        {
            if(playerManager.canArcLight)
            {
                materialRing = materialPowerStone = onMaterial;
                ringStele.GetComponent<MeshRenderer>().material = materialRing;
                GetComponent<MeshRenderer>().material = materialPowerStone;
                powerFxGO.SetActive(true);
                Destroy(this,  0.2f);
            } 
        }
        else if(indexPower == 2)
        {
            if(playerManager.canThunder)
            {
                materialRing = materialPowerStone = onMaterial;
                ringStele.GetComponent<MeshRenderer>().material = materialRing;
                GetComponent<MeshRenderer>().material = materialPowerStone;
                foreach(GameObject power in powerFxGOs) power.SetActive(true);
                Destroy(this, 0.2f);
            } 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(indexPower != 2)
        {
            if(other.gameObject.layer == 8 && !powerFxGO.activeSelf)
            {
                if(other.TryGetComponent<MagnetSphereManager>(out MagnetSphereManager component)) component.HandleDestroyMagnetSphere();
                ringStele.GetComponent<MeshRenderer>().material = onMaterial;
                GetComponent<MeshRenderer>().material = onMaterial;
                powerFxGO.SetActive(true);
            }
        }
        else
        {
            if(powerFxGOs[1].activeSelf && powerFxGOs[0].activeSelf && powerFxGOs[2].activeSelf)
            {
                ringStele.GetComponent<MeshRenderer>().material = onMaterial;
                GetComponent<MeshRenderer>().material = onMaterial;
                if(powerFxGO != null) powerFxGO.SetActive(true);
            }
        }
    }


}
