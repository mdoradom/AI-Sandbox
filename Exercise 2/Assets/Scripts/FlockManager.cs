using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public static FlockManager FM;
    public GameObject fishPrefab;
    public GameObject leaderFishPrefab;
    public int numFish = 20;
    public int numLeaderFish = 1;
    public GameObject[] allFish;
    public List<GameObject> leaderFish;
    public Vector3 swimLimits = new Vector3(5, 5, 5);
    public Vector3 goalPos = Vector3.zero;

    [Header("Fish Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(1.0f, 5.0f)]
    public float rotationSpeed;

    [Header("Leader Influence")]
    [Range(0, 100)]
    public int leaderInfluence;

    void Start()
    {
        allFish = new GameObject[numFish];
        leaderFish = new List<GameObject>();

        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),  
                Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = Instantiate(fishPrefab, pos, Quaternion.identity);
        }

        for (int i = 0; i < numLeaderFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));
            GameObject leader = Instantiate(leaderFishPrefab, pos, Quaternion.identity);
            leaderFish.Add(leader); // Add to the leaders list
        }

        FM = this;
        goalPos = this.transform.position;
    }

    void Update()
    {
        if (Random.Range(0, 100) < leaderInfluence)
        {
            if (leaderFish.Count > 0)
            {
                // Choose a random leader's position as the new goal
                int leaderIndex = Random.Range(0, leaderFish.Count);
                goalPos = leaderFish[leaderIndex].transform.position;
            }
            else
            {
                goalPos = this.transform.position + new Vector3(
                    Random.Range(-swimLimits.x, swimLimits.x),
                    Random.Range(-swimLimits.y, swimLimits.y),  
                    Random.Range(-swimLimits.z, swimLimits.z));
            }
        }
    }
}