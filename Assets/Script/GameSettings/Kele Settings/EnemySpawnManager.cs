using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManagers;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 3)
            CheckisEnemy();
    }

    private void LoadEnemy()
    {
        GameObject visuals = Instantiate(enemyManagers.gameObject);
        visuals.transform.SetParent(transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;
        Debug.Log("New kossikaze");
    }
    
    public void CheckisEnemy()
    {
        if(transform.childCount == 0)
            LoadEnemy();
            //canProduce = true;
    }
}
