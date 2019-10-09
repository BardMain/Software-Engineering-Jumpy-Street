using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public GameObject[] roadObstacles = new GameObject[1];
    private List<float> obsDirections = new List<float>();
    private List<GameObject> spawnedRdObj = new List<GameObject>();
    private GameObject[] rdSpawns;

    private float minTime = .8f;
    private float maxTime = 2f;
    private float gameEdge = 16f;

    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        CheckForSpawns();
    }

    private void Update()
    {
        ObstacleMovement();
        Camera.main.gameObject.transform.position = player.transform.position + new Vector3(1f, 3.5f, -0.9f);
    }

    private void CheckForSpawns()
    {
        //Debug.Log("checking now");
        rdSpawns = GameObject.FindGameObjectsWithTag("ObsSpwnRd"); //Checks for what spawn points are available

        //The rest spawns obstacles from currently existing spawn points
        //Does so at staggered times
        for(int x = 0; x < rdSpawns.Length; x++)
        {
            Vector3 pos = rdSpawns[x].transform.position;
            StartCoroutine(SpawnObstacles(pos));
        }

        Invoke("CheckForSpawns", maxTime);
    }

    //Spawns objects and sets their direction based on spawn pos
    IEnumerator SpawnObstacles(Vector3 pos)
    {
        float waitTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(waitTime);

        int toSpawn = Random.Range(0, roadObstacles.Length);

        Vector3 rotation = Vector3.zero;
        float direction = 0; 
        //going left
        if(pos.x > 0)
        {
            direction = -5f;
            rotation = new Vector3 (0, 270, 0);
        }
        //going right
        else if(pos.x < 0)
        {
            direction = 5f;
            rotation = new Vector3(0, 90, 0);
        }
        //Uh oh
        else
        {
            Debug.Log("Man, what did you do now?");
        }
        //Spawns the objects and sets their rotation and direction
        GameObject newObj = Instantiate(roadObstacles[toSpawn], pos, Quaternion.identity);
        newObj.transform.localEulerAngles = rotation;
        spawnedRdObj.Add(newObj);
        obsDirections.Add(direction);
    }

    //Will need to incorporate pooling later
    private void ObstacleMovement()
    {
        int toDelete = -1;
        for(int x = 0; x < spawnedRdObj.Count; x++)
        {
            float currentX = spawnedRdObj[x].transform.position.x;
            Vector3 toMove = Vector3.zero;
            toMove.x = obsDirections[x] * Time.deltaTime;
            spawnedRdObj[x].transform.position += toMove;
            if(currentX > gameEdge || currentX < -gameEdge)
            {
                toDelete = x;
            }
        }
        //If an object is out of bounds will delete it, need to rework to object pool instead
        if(toDelete > -1)
        {
            Destroy(spawnedRdObj[toDelete]);
            spawnedRdObj.RemoveAt(toDelete);
            obsDirections.RemoveAt(toDelete);
        }
    }
}
