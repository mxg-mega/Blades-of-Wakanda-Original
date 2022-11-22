using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //prefabs of the diffrent terrains at the sides
    //public GameObject[] terrainAtSides;
    //right mountain slot
    //public GameObject[] rightSideSpawn;
    //left mountain slot
    //public GameObject[] leftSideSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //spawn trhe objects after 1sec and an interval of 1sec
        //InvokeRepeating("SpawnDifferentTerrains", 1, 4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void SpawnDifferentTerrains()
    {
        //spawn position for the right mountain
        Vector3 rightSpawnPos = new Vector3(15.0f, 0f, 230.5f);
        //spawn position for the left mountain
        Vector3 leftSpawnPos = new Vector3(-24.0f, 0f, 230.5f);

        int index = Random.Range(0, leftSideSpawn.Length);
        int index2 = Random.Range(0, rightSideSpawn.Length);

        Instantiate(rightSideSpawn[index], rightSpawnPos, rightSideSpawn[index].transform.rotation);
        Instantiate(leftSideSpawn[index2], leftSpawnPos, leftSideSpawn[index2].transform.rotation);
    }

    void SpawnTerrain()
    {
        //the prefabs for the mountains
        Instantiate(rightMountainSpawn, spawnPos, rightMountainSpawn.transform.rotation);
        Instantiate(leftMountainSpawn, leftMountainPos, leftMountainSpawn.transform.rotation);
    }
    */
}
