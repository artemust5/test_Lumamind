using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int maxPrefabs = 10;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float minDistance = 2f;

    public List<GameObject> prefabs;

    void Start()
    {
        prefabs = new List<GameObject>();

        for (int i = 0; i < maxPrefabs; i++)
        {
            SpawnPrefab();
        }
    }

    public void SpawnPrefab()
    {
        Vector3 spawnPosition = GetSpawnPosition();

        if (spawnPosition != Vector3.zero)
        {
            GameObject newPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);
            prefabs.Add(newPrefab);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPosition.y = 0;

            if (IsPositionFree(randomPosition))
            {
                return randomPosition;
            }
        }

        return Vector3.zero;
    }

    private bool IsPositionFree(Vector3 position)
    {
        foreach (GameObject prefab in prefabs)
        {
            if (Vector3.Distance(position, prefab.transform.position) < minDistance)
            {
                return false;
            }
        }

        return true;
    }
}
