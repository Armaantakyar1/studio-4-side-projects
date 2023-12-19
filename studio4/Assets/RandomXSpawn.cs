using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomXSpawn : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] float minX = -5f;
    [SerializeField] float maxX = 5f;
    [SerializeField] float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }
    void SpawnObject()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}