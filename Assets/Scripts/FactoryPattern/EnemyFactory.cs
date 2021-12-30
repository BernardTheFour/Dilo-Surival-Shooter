using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [SerializeField]
    public GameObject[] enemyPrefab;

    public GameObject FactoryMethod(int tag, Vector3 spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefab[tag], spawnPoint, Quaternion.identity);
        return enemy;
    }
}
