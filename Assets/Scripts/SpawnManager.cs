using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //right mountain slot
    public GameObject rightMountainSpawn;
    //left mountain slot
    public GameObject leftMountainSpawn;
    //spawn position for the right mountain
    private Vector3 spawnPos = new Vector3(15.0f, 0f, 4.0f);
    //spawn position for the left mountain
    private Vector3 leftMountainPos = new Vector3(-15.0f, 0f, 19.0f);


    // Start is called before the first frame update
    void Start()
    {
        //spawn trhe objects after 1sec and an interval of 1sec
        InvokeRepeating("SpawnTerrain", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTerrain()
    {
        //the prefabs for the mountains
        Instantiate(rightMountainSpawn, spawnPos, rightMountainSpawn.transform.rotation);
        Instantiate(leftMountainSpawn, leftMountainPos, leftMountainSpawn.transform.rotation);
    }
}
