using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public int numberOfZombies;
    public GameObject[] zombiePrefab;
    public SpawnerVolume[] spawnerVolumes;

    private GameObject followObject;
    // Start is called before the first frame update
    void Start()
    {
       //followObject = GameObject.FindGameObjectWithTag("Player");

        for(int i = 0; i < numberOfZombies; i++)
        {
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {

        GameObject zombieToSpawn = zombiePrefab[Random.Range(0, zombiePrefab.Length)];

        SpawnerVolume spawner = spawnerVolumes[0];

        GameObject zombie = Instantiate(zombieToSpawn, spawner.GetPositionInBounds(), spawner.transform.rotation);

        //zombie.GetComponent<ZombieComponent>().Initialize(followObject);

        
    }
}
