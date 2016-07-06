using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Horizonal,
        Vertical
    };

    public GameObject Player;
    public GameObject Player_Model;
    public GameObject Target;

    public HorizontalSpawner HorizontalSpawner;
    public VerticalSpawner VerticalSpawner;

    [System.Serializable]
    public struct BeatList
    {
        public GameState State;
        public float CollectableBoost;
        public GameObject[] ObstacleInstances;
        public GameObject BeginningPlatform;
        public GameObject endlingPlatform; //added endingplaftform
    }

    public GameObject EndingPlatform;

    public BeatList[] Beats;

    //SaveLoad
    public SaveLoad SaveLoadManager;
    public int sceneCount;

    [Range(0.0f, 20.0f)]
    public float CollectableBoost = 1.0f;

    [Range(0.0f, 10.0f)]
    public float TimerSpeed = 5.0f;



    public float InitialGap = 7.0f;

    public float CloseDistance = 0.025f;

    private float InitialPlayerTransform;
    private bool inBetween; //if the player is in between states
    private bool isVerticalExist = false;

    public int StateOfGame;

    public bool ChangedBeat = false; // check if beats were changed for cutscenes
    // Use this for initialization
    void Start()
    {
        inBetween = false;
        StateOfGame = 0;
        InitialPlayerTransform = Player_Model.transform.position.x;
        HorizontalSpawner.Initialize(Beats[0].ObstacleInstances, Beats[0].BeginningPlatform);
    }

    // Update is called once per frame
    void Update()
    {
        switch (Beats[StateOfGame].State)
        {
            case GameState.Horizonal:
                HorizontalSpawner.Spawn();
                if (Input.GetKeyDown(KeyCode.C))
                {
                    //Debug.Log("Hello");
                    Player.transform.position += new Vector3(CollectableBoost, 0.0f, 0.0f);
                }

                break;
            case GameState.Vertical:
                if (isVerticalExist == false)
                {
                    VerticalSpawner.Initialize(Beats[StateOfGame].ObstacleInstances, Beats[StateOfGame].BeginningPlatform);
                    isVerticalExist = true;
                }
                else
                {
                    VerticalSpawner.Spawn(Beats[StateOfGame].ObstacleInstances, Beats[StateOfGame].endlingPlatform);
                }
                break;
            default:
                break;
        }
        if (inBetween && InitialPlayerTransform - (Target.transform.position.x - Player_Model.transform.position.x) < 0.01f)
        {
            inBetween = false;
            ChangedBeat = false;
        }

        SaveLoadManager.InvokeRepeating("Save", 30, 30);
    }

    public void ChangeBeat()
    {
        if (!inBetween)
        {
            inBetween = true;
            StateOfGame++;
            HorizontalSpawner.Reinitialize(Beats[StateOfGame].ObstacleInstances);
            Player.transform.position = new Vector3(InitialPlayerTransform, 0.0f, 0.0f);
            ChangedBeat = true;
        }
    }
}