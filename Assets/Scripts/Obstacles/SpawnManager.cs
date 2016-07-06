using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    //Singleton

    static public SpawnManager Instance;

    //Obstacle pool

    //[SerializeField]
    //private GameObject[] ObstacleInstances;
    //
    private List<ObstacleMovement> ManagedPlatforms;
    private ObstacleMovement BeginningPlatformMover;
    private ObstacleMovement EndingPlatformMover;

    public int NumberOfObstacles;

    private int IndexToSpawn;
    private int CurrentPlatform;


    ////Spawning
    //public GameObject BeginningPlatform;
    //public GameObject EndingPlatform;


    public Transform SpawnLocation_1;
    public Transform SpawnLocation_2;
    public Transform SpawnLocation_3;

    public Transform EvaPosition;
    public Transform GabPosition;

    public float Speed = 1.0f;

    private bool Playing;

    private float Offset = 0.04f;

    void Initialize(GameObject[] ObstacleInstances, GameObject BeginningPlatform)
    {
        Instance = this;

        ManagedPlatforms = new List<ObstacleMovement>();

        for( int i = 0; i < NumberOfObstacles; ++i )
        {
            GameObject newObstacle = Instantiate( ObstacleInstances[Random.Range( 0, ObstacleInstances.Length )]);
            newObstacle.SetActive( false );
            newObstacle.transform.parent = transform;
            ManagedPlatforms.Add( newObstacle.GetComponent<ObstacleMovement>() );
            ManagedPlatforms[i].SetSpeed( Speed );
        }

        IndexToSpawn = 0;
        Playing = true;
        Vector3 Spawn_platform = new Vector3( EvaPosition.position.x, 0.0f, 0.0f );
        ManagedPlatforms[(IndexToSpawn++ % NumberOfObstacles)].Spawn( Spawn_platform );
        CurrentPlatform = 0;

        GameObject beginningObstacle = (GameObject)Instantiate( BeginningPlatform );
        beginningObstacle.transform.parent = transform;
        Vector3 End = beginningObstacle.GetComponent<ObstacleMovement>().GetEndPoint();
        End.x -= Offset;
        Spawn_platform = new Vector3( EvaPosition.position.x - End.x, 0.0f, 0.0f );
        beginningObstacle.GetComponent<ObstacleMovement>().Spawn( Spawn_platform );
    }

    // Update is called once per frame
    void Spawn()
    {
        if( Playing )
        {
            if( ManagedPlatforms[CurrentPlatform % NumberOfObstacles].enabled )
            {
                Vector3 End = ManagedPlatforms[CurrentPlatform % NumberOfObstacles].GetEndPoint();
                End.x -= Offset;
                if ( End.x < EvaPosition.position.x + ManagedPlatforms[CurrentPlatform % NumberOfObstacles].Ground.bounds.size.x )
                {
                    CurrentPlatform = IndexToSpawn;
                    Vector3 Spawn_platform = new Vector3( End.x , End.y, 0.0f );
                    //Vector3 Spawn_platform = new Vector3(ManagedPlatforms[CurrentPlatform % NumberOfObstacles].Ground.bounds.size.x , End.y, 0.0f);
                    Debug.Log(End.x);
                    ManagedPlatforms[((IndexToSpawn++ % NumberOfObstacles))].Spawn( Spawn_platform );
                }
            }
        }
    }
    void Destroy()
    {
    }
    
    public void StartGame()
    {
        IndexToSpawn = 0;
        Playing = true;
    }

    public void EndGame()
    {
        Playing = false;
    }
    public float GetDepth( int i )
    {
        switch( i )
        {
        case 1:
        return SpawnLocation_1.transform.position.z;
        case 2:
        return SpawnLocation_2.transform.position.z;
        case 3:
        return SpawnLocation_3.transform.position.z;
        }
        return 0.0f;
    }
}
