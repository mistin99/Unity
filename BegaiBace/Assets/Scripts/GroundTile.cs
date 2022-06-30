using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    List<GameObject> ObstacleList = new List<GameObject>();
    List <GameObject> BuildingList = new List<GameObject>();
    GroundSpawner groundSpawner;
    public GameObject Obstacle1;
    public GameObject Obstacle2;
    public GameObject Obstacle3;
    public GameObject Building1;
    public GameObject Building2;
    public GameObject Building3;
    public GameObject Bottle;
   // public static int  totalBottles = 0;
    void Start()
    {

        //Add obstacles to list
        ObstacleList.Add(Obstacle1);
        ObstacleList.Add(Obstacle2);
        ObstacleList.Add(Obstacle3);
        
        // Add Buildings to list
        BuildingList.Add(Building1);
        BuildingList.Add(Building2);
        BuildingList.Add(Building3);

        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnObstacle();
        SpawnBottle();
        SpawnBuilding();
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile();
        Destroy(gameObject,2);
    }


    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        //Select Random Obstacle
        int obstacleIndex = UnityEngine.Random.Range(0, 3);

        //Select Random point to spawn obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        //Spawn Obstacle
        Instantiate(ObstacleList[obstacleIndex], spawnPoint.position, Quaternion.identity, transform);
    }
    void SpawnBottle()
    {
        //Select Random point to spawn bottle
        int bottleSpawnIndex = Random.Range(5, 8);
        Transform spawnPoint = transform.GetChild(bottleSpawnIndex).transform;

        //Spawn bottle
        Instantiate(Bottle, spawnPoint.position, Quaternion.identity, transform);
    }
    void SpawnBuilding()
    {
        //Select Random Building
        int BuildingIndex = UnityEngine.Random.Range(0, 3);
        //Select Random point to spawn Building
        int buildingSpawnIndex = Random.Range(8, 10);
        Transform spawnPoint = transform.GetChild(buildingSpawnIndex).transform;

        //Instantiate
        Instantiate(BuildingList[BuildingIndex], spawnPoint.position, Quaternion.identity, transform);
    }
    

}
