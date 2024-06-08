using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobObserver : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 2.0f;
    [SerializeField] private MobSpawner mobSpawner;

    private Queue<int> prefabDeletionQueue = new Queue<int>();

    void Start()
    {
        StartCoroutine(CheckPrefabs());
        StartCoroutine(HandlePrefabSpawn());
    }

    IEnumerator CheckPrefabs()
    {
        while (true)
        {
            for (int i = mobSpawner.prefabs.Count - 1; i >= 0; i--)
            {
                if (mobSpawner.prefabs[i] == null)
                {
                    mobSpawner.prefabs.RemoveAt(i);
                    prefabDeletionQueue.Enqueue(i);
                }
            }

            yield return null;
        }
    }

    IEnumerator HandlePrefabSpawn()
    {
        while (true)
        {
            if (prefabDeletionQueue.Count > 0)
            {
                prefabDeletionQueue.Dequeue();
                yield return new WaitForSeconds(spawnDelay);
                mobSpawner.SpawnPrefab();
            }

            yield return null;
        }
    }
}
