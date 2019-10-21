using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public static ObstacleSpawnerScript instance;

    public GameObject[] roadObstacles = new GameObject[1];
    private List<float> rdDirections = new List<float>();
    private List<GameObject> spawnedRdObj = new List<GameObject>();
    private GameObject[] rdSpawns;

<<<<<<< HEAD
    public GameObject[] waterObstacles = new GameObject[1];
    private List<float> waterDirections = new List<float>();
    private List<GameObject> spawnedWaterObj = new List<GameObject>();
    private GameObject[] waterSpawns;

    private float minTime = .5f;
    private float maxTime = 2f;
    private float gameEdge = 10f;
=======
    private float minTime = .8f;
    private float maxTime = 2f;
    private float gameEdge = 16f;
>>>>>>> NelsonStart

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        CheckForSpawns();
    }

    private void Update()
    {
        ObstacleMovement();
    }

    private void CheckForSpawns()
    {
        //Debug.Log("checking now");
        rdSpawns = GameObject.FindGameObjectsWithTag("ObsSpwnRd"); //Checks for what road spawn points are available
        waterSpawns = GameObject.FindGameObjectsWithTag("WaterSpwnPnt"); //Checks for what water spawn points are available

        //The rest spawns obstacles from currently existing spawn points
        //Does so at staggered times
        for(int x = 0; x < rdSpawns.Length; x++)
        {
            Vector3 pos = rdSpawns[x].transform.position;
            StartCoroutine(SpawnRdObs(pos));
        }

        for (int x = 0; x < waterSpawns.Length; x++)
        {
            Vector3 pos = waterSpawns[x].transform.position;
            StartCoroutine(SpawnWaterObj(pos));
        }

        Invoke("CheckForSpawns", maxTime);
    }

    //Spawns objects and sets their direction based on spawn pos
    IEnumerator SpawnRdObs(Vector3 pos)
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
        rdDirections.Add(direction);
    }

    //For Spawning water objects
    IEnumerator SpawnWaterObj(Vector3 pos)
    {
        float waitTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(waitTime);

        int toSpawn = Random.Range(0, waterObstacles.Length);

        Vector3 rotation = Vector3.zero;
        float direction = 0;
        //going left
        if (pos.x > 0)
        {
            direction = -3f;
            rotation = new Vector3(0, 270, 0);
        }
        //going right
        else if (pos.x < 0)
        {
            direction = 3f;
            rotation = new Vector3(0, 90, 0);
        }
        //Uh oh
        else
        {
            Debug.Log("Man, what did you do now?");
        }
        //Spawns the objects and sets their rotation and direction
        GameObject newObj = Instantiate(waterObstacles[toSpawn], pos, Quaternion.identity);
        newObj.transform.localEulerAngles = rotation;
        spawnedWaterObj.Add(newObj);
        waterDirections.Add(direction);
    }

    //Will need to incorporate pooling later
    private void ObstacleMovement()
    {
        int toDeleteRd = -1;
        int toDeleteWater = -1;
        for(int x = 0; x < spawnedRdObj.Count; x++)
        {
            float currentX = spawnedRdObj[x].transform.position.x;
            Vector3 toMove = Vector3.zero;
            toMove.x = rdDirections[x] * Time.deltaTime;
            spawnedRdObj[x].transform.position += toMove;
            if(currentX > gameEdge || currentX < -gameEdge)
            {
                toDeleteRd = x;
            }
        }
        for (int x = 0; x < spawnedWaterObj.Count; x++)
        {
            float currentX = spawnedWaterObj[x].transform.position.x;
            Vector3 toMove = Vector3.zero;
            toMove.x = waterDirections[x] * Time.deltaTime;
            spawnedWaterObj[x].transform.position += toMove;
            if (currentX > gameEdge || currentX < -gameEdge)
            {
                toDeleteWater = x;
            }
        }
        //If an object is out of bounds will delete it, need to rework to object pool instead
        if (toDeleteRd > -1)
        {
            Destroy(spawnedRdObj[toDeleteRd]);
            spawnedRdObj.RemoveAt(toDeleteRd);
            rdDirections.RemoveAt(toDeleteRd);
        }
        if (toDeleteWater > -1)
        {
            Destroy(spawnedWaterObj[toDeleteWater]);
            spawnedWaterObj.RemoveAt(toDeleteWater);
            waterDirections.RemoveAt(toDeleteWater);
        }
    }
    
    //The player is gonna use this to find the direction to go on a log
    public float HandOverThatFloat(Collider other)
    {
        float theChosenOne = 0f;
        int thisOne = 0;

        for(int x = 0; x < spawnedWaterObj.Count; x++)
        {
            if(other.transform == spawnedWaterObj[x].transform)
            {
                thisOne = x;
            }
        }
        theChosenOne = waterDirections[thisOne];
        return (theChosenOne);
    }

}
