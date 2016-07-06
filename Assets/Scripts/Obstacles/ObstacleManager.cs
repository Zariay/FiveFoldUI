using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] ObstacleInstances;

    private List<ObstacleBehavior> Obstacles;
    
    void Awake()
    {
       //SpawnLocation_1 = Track1.transform.position.z;
       //SpawnLocation_2 = Track2.transform.position.z;
       //SpawnLocation_3 = Track3.transform.position.z;
        Vector3 position = this.transform.position;
        GameObject newObstacle = Instantiate( ObstacleInstances[Random.Range( 0, ObstacleInstances.Length )] );

        newObstacle.transform.position = position;
        newObstacle.SetActive( true );
        newObstacle.transform.parent = transform;
        this.GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {

    }
	
    public float GetDepth(int i)
    {
       //switch (i)
       //{
       //case 1:
       //return SpawnLocation_1;
       //case 2:
       //return SpawnLocation_2;
       //case 3:
       //return SpawnLocation_3;
       //}
       return 0.0f;
    }
}
