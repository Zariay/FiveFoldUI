using UnityEngine;
using System.Collections;

public class VerticalSpawner : MonoBehaviour
{


    private GameObject[] platform = new GameObject[5];
    private float DestroyPoint_Y;
    private float SpawanPoint_Y;

    private bool final;
    private bool playing;

    public float Speed = 5.0f;
    public float TimeLeft = 100.0f;
    public float V_prefabSize_Y_Length;


    public void Initialize(GameObject[] ObstacleInstances, GameObject BeginningPlatform)
    {

        playing = true;
        final = false;

        float YLength = 0;

        platform[0] = BeginningPlatform;

        platform[0] = Instantiate(BeginningPlatform, new Vector3(0, YLength, 0), Quaternion.identity) as GameObject;
        platform[0].transform.parent = transform;

        for (int i = 1; i < platform.Length; ++i)
        {
            YLength += V_prefabSize_Y_Length;
            platform[i] = ObstacleInstances[Random.Range(0, ObstacleInstances.Length)];
            platform[i] = Instantiate(platform[i], new Vector3(0, -YLength, 0), Quaternion.identity) as GameObject;
            platform[i].transform.parent = transform;

        }

        DestroyPoint_Y = platform[2].GetComponent<Transform>().position.y;
        SpawanPoint_Y = platform[3].GetComponent<Transform>().position.y;
    }

    public void Spawn(GameObject[] ObstacleInstances, GameObject endlingPlatform)
    {
        TimeLeft -= Time.deltaTime;
        if (TimeLeft < 0)
        {
            GameOver();
        }

        if (playing)
        {
            MoveGround(ObstacleInstances, endlingPlatform);
        }

    }

    void MoveGround(GameObject[] ObstacleInstances, GameObject endlingPlatform)
    {

        float tempSpeed = Speed * Time.deltaTime;

        for (int i = 0; i < platform.Length; ++i)
        {
            platform[i].transform.Translate(Vector3.up * tempSpeed);
        }

        if (platform[4].transform.position.y >= DestroyPoint_Y)
        {
            DestroyGround(ObstacleInstances, endlingPlatform);
        }

    }

    void MakeGround(GameObject[] ObstacleInstances, GameObject endlingPlatform)
    {


        for (int i = 0; i < (platform.Length - 1); ++i)
        {
            platform[i] = platform[i + 1];
        }

        if (!final)
        {
            platform[4] = Instantiate(ObstacleInstances[Random.Range(0, ObstacleInstances.Length)], new Vector3(0, SpawanPoint_Y, 0), Quaternion.identity) as GameObject;
            platform[4].transform.parent = transform;
        }

        else
        {
            platform[4] = Instantiate(endlingPlatform, new Vector3(0, SpawanPoint_Y, 0), Quaternion.identity) as GameObject;
            platform[4].transform.parent = transform;
        }

    }

    void DestroyGround(GameObject[] ObstacleInstances, GameObject endlingPlatform)
    {
        Destroy(platform[0]);
        MakeGround(ObstacleInstances, endlingPlatform);
    }

    void GameOver()
    {
        playing = false;
    }

    void Final_stair()
    {
        final = true;
    }
}


