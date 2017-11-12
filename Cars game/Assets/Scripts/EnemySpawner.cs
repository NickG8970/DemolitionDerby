using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyType> enemyCars;
    public Transform spawnPointGO;

    private List<Transform> spawnPoints = new List<Transform>();
    private System.Random prng; // Pseudo-Random Number Generator

    void Start()
    {
        prng = new System.Random();
        for (var i = 0; i < spawnPointGO.childCount; i++)
        {
            spawnPoints.Add(spawnPointGO.GetChild(i));
        }
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Transform sp = spawnPoints[i];
            Debug.Log(sp);
            int enemyPrefabIndex = prng.Next(0, enemyCars.Count);
            Debug.Log(enemyPrefabIndex);
            EnemyType enemyToInstantiate = enemyCars[enemyPrefabIndex];
            Debug.Log(enemyToInstantiate.prefab);
            GameObject enemyGO = Instantiate(enemyToInstantiate.prefab, new Vector3(sp.position.x, enemyToInstantiate.prefab.transform.position.y, sp.position.z), enemyToInstantiate.prefab.transform.rotation);
            Debug.Log(enemyGO);
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            Debug.Log(enemy);
            enemy.enemyType.type = enemyToInstantiate.type;

            GameMaster.instance.enemiesAlive++;
        }
    }
}
