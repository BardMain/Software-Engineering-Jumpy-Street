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

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObject");

        GameObject spawnChunk = chunks[0];

        SpawnChunk(chunkIndex, spawnChunk);

        SpawnChunk(chunkIndex, SelectChunk());
    }

    private void Update()
    {
        distanceTraveled = Mathf.RoundToInt(player.transform.position.z);

        if(distanceTraveled % 8 == 0 && distanceTraveled > ((chunkIndex - 2) * 8))
        {
            SpawnChunk(chunkIndex, SelectChunk());
            print("chunk spawning");
        }
    }

    GameObject SelectChunk()
    {
        GameObject chunkToSpawn;

        int random = Random.Range(1, chunks.Length - 1);

        chunkToSpawn = chunks[random];

        ++chunkIndex;

        return chunkToSpawn;
    }

    void SpawnChunk(int index, GameObject chunkToSpawn)
    {
        Instantiate(chunkToSpawn, new Vector3(0, 0, chunkIndex * 8), Quaternion.identity);
    }
}