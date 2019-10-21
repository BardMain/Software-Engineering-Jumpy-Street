using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] chunks;

    [SerializeField]
    int chunkIndex = 0;

    int distanceTraveled = 0;
    int lastRandom = -1; //Proper value assigned in SelectChunk(), avoids the same chunk spawning multiple times in a row

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObject");

        GameObject spawnChunk = chunks[0]; //Define the starting chunk which has no obstacles

        SpawnChunk(chunkIndex, spawnChunk); //Instantiate the starting chunk at index = 0

        SpawnChunk(chunkIndex, SelectChunk()); //Instantiate the following chunk at index = 1
    }

    private void Update()
    {
        distanceTraveled = Mathf.RoundToInt(player.transform.position.z); //

        if(distanceTraveled % 8 == 0 && distanceTraveled > ((chunkIndex - 2) * 8)) //If the player has finished walking through their current chunk AND less than 2 chunks are loaded ahead of the player...
        {
            SpawnChunk(chunkIndex, SelectChunk()); //Instantiate the next chunk at index = chunkIndex
        }
    }

    GameObject SelectChunk()
    {
        GameObject chunkToSpawn; //Declare the value to hold the chunk

        int random = Random.Range(1, chunks.Length - 1); //Grab a random integer from 1 to the end of the chunks[] array

        while(random == lastRandom) //If these two numbers are the same, select a new random integer to not spawn the same chunk twice in a row
        {
            random = Random.Range(1, chunks.Length - 1);
            print("Same random number picked, re-randomizing...");
        }

        chunkToSpawn = chunks[random]; //Choose a chunk to spawn based on the randomly generated integer

        ++chunkIndex; //Increment the chunkIndex so the next time that SpawnChunk is called, the chunk is spawned 8 units ahead

        lastRandom = random;

        return chunkToSpawn;
    }

    void SpawnChunk(int index, GameObject chunkToSpawn)
    {
        Instantiate(chunkToSpawn, new Vector3(0, 0, chunkIndex * 8), Quaternion.identity); //Instantiate the chunk selected with a position of 0, 0, and the appropriate depth based on the chunkIndex
    }
}