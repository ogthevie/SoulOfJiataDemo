using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManagers;
    [SerializeField] bool haveChild;

    void LateUpdate()
    {
        CheckPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
       if(!haveChild) LoadEnemy();
    }

    public void LoadEnemy()
    {
        GameObject visuals = Instantiate(enemyManagers.gameObject, this.transform, false);
        Debug.Log("nouveau " +visuals.name);
        haveChild = true;
    }

    void CheckPoint()
    {
        if(haveChild && transform.childCount == 0) haveChild = false;
    }
}
