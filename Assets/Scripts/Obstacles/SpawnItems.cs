using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public Transform[] spawnPoint;
    public float spawnTimer = 1.5f;

    public GameObject coins;
    //public GameObject[]

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("SpawnCoins", spawnTimer, spawnTimer);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void SpawnCoins()
    {
        int spawnIndex = Random.Range(0, spawnPoint.Length); //set index number of array
        Instantiate(coins, spawnPoint[spawnIndex].position, spawnPoint[spawnIndex].rotation);
    }
}
