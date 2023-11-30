using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public EnemyManager [] enemyManagers = new EnemyManager[5];

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 3)
            CheckisEnemy();
    }

    static int CheckRandomEnemy()
    {
        int probability = Random.Range(0,3);
        return probability;
    }

    private void LoadEnemy(int k)
    {
        GameObject visuals = Instantiate(enemyManagers[k].gameObject);
        visuals.transform.SetParent(transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;
        Debug.Log("New kossikaze");
    }
    
    private void CheckisEnemy()
    {
        if(transform.childCount == 0)
            LoadEnemy(CheckRandomEnemy());
            //canProduce = true;
    }
}
