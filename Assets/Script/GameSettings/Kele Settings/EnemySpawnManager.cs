using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManagers;

    void Start()
    {
        LoadEnemy();
    }

    void LateUpdate()
    {
        CheckPoint();
    }

    private void LoadEnemy()
    {
        GameObject visuals = Instantiate(enemyManagers.gameObject);
        visuals.transform.SetParent(transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = transform.rotation;
        Debug.Log("New kossikaze");
    }

    void CheckPoint()
    {
        if(transform.childCount == 0) Destroy(this.gameObject);
    }
}
