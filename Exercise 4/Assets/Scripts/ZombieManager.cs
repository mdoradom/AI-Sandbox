using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    public GameObject zombiePrefab; // Referencia al prefab del zombi
    public int numberOfZombies = 10; // Número de zombis a spawnear

    void Start()
    {
        SpawnZombies();
    }

    void SpawnZombies()
    {
        for (int i = 0; i < numberOfZombies; i++)
        {
            Vector3 spawnPosition = GetRandomPositionOnNavMesh();
            if (spawnPosition != Vector3.zero)
            {
                GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
                zombie.transform.parent = transform; // Set the parent of the instantiated zombie
            }
        }
    }

    Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 50f; // Radio de búsqueda
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 50f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }

    // Nuevo método para distribuir el mensaje a todos los zombies
    public void NotifyPlayerDetected()
    {
        foreach (Transform zombie in transform)
        {
            zombie.SendMessage("OnPlayerDetected", SendMessageOptions.DontRequireReceiver);
        }
    }
}