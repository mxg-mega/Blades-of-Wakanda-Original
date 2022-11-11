using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject rightMountainSpawn;
    public GameObject leftMountainSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnTerrain", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTerrain()
    {
        Vector3 spawnPos = new Vector3(15.0f, 0f, 4.0f);
        Vector3 leftMountainPos = new Vector3(-15.0f, 0f, 19.0f);

        Instantiate(rightMountainSpawn, spawnPos, rightMountainSpawn.transform.rotation);
        Instantiate(leftMountainSpawn, leftMountainPos, leftMountainSpawn.transform.rotation);
    }
}
