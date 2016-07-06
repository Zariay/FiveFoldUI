using UnityEngine;
using System.Collections;

public class SpawnObjectParts : MonoBehaviour
{
    public GameObject Position;

    [SerializeField]
    private GameObject[] ObstacleInstances;
    // Use this for initialization
    void Start()
    {
        GameObject newObstacle = Instantiate(ObstacleInstances[Random.Range(0, ObstacleInstances.Length)]);
        newObstacle.SetActive(true);
        newObstacle.transform.position = this.transform.position + newObstacle.transform.position;
        newObstacle.transform.parent = transform;
    }
	
}
