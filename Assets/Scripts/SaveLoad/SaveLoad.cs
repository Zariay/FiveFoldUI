using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class SaveLoad : MonoBehaviour
{
    public GameManager Manager;
    public PlayerController Player;
    public GameObject Eva;
    public static SaveLoad SaveLoadManager;

    public int StateOfGame;
    public string SceneName;

    //game data variables
    #region
    //gameplay data
    public enum GameState
    {
        Horizonal,
        Vertical
    };

    [System.Serializable]
    public struct BeatList
    {
        public GameState State;
        public float CollectableBoost;
        public GameObject[] ObstacleInstances;
        public GameObject BeginningPlatform;
        public GameObject endlingPlatform; 
    }

    public HorizontalSpawner HorizontalSpawner;
    public VerticalSpawner VerticalSpawner;
    public SpawnManager PlatformSpawner;
    public BeatList[] Beats;

    //saving player data
    public float InitialPlayerPosX;
    public float InitialPlayerPosY;
    public float InitialPlayerPosZ;
    public GameObject Player_Model;

    //saving Eva data
    public float InitialEvaPosX;
    public float InitialEvaPosY;
    public float InitialEvaPosZ;
    #endregion

    public void Start()
    {
        if (SaveLoadManager == null)
        {
            DontDestroyOnLoad(SaveLoadManager);
            SaveLoadManager = this;
        }
        else if (SaveLoadManager != this)
            Destroy(gameObject);

        Manager = FindObjectOfType<GameManager>();
        Player = FindObjectOfType<PlayerController>();
        Eva = GameObject.FindGameObjectWithTag("Eva");
        PlatformSpawner = FindObjectOfType<SpawnManager>();
        HorizontalSpawner = PlatformSpawner.GetComponent<HorizontalSpawner>();
        VerticalSpawner = PlatformSpawner.GetComponent<VerticalSpawner>();
    }

    public void Save()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.dat", FileMode.Open);

        Data data = new Data();
        data.StateOfGame = Manager.StateOfGame;
        data.HorizontalSpawner = Manager.HorizontalSpawner;
        data.VerticalSpawner = Manager.VerticalSpawner;
        data.SceneName = SceneManager.GetActiveScene().name;
        data.Beats[data.StateOfGame].BeginningPlatform = Manager.Beats[Manager.StateOfGame].BeginningPlatform;
        data.Beats[data.StateOfGame].CollectableBoost = Manager.Beats[Manager.StateOfGame].CollectableBoost;
        data.Beats[data.StateOfGame].endlingPlatform = Manager.Beats[Manager.StateOfGame].endlingPlatform;
        data.Beats[data.StateOfGame].ObstacleInstances = Manager.Beats[Manager.StateOfGame].ObstacleInstances;

        data.InitialPlayerPosX = Player.transform.position.x;
        data.InitialPlayerPosY = Player.transform.position.y;
        data.InitialPlayerPosZ = Player.transform.position.z;

        data.InitialEvaPosX = Eva.transform.position.x;
        data.InitialEvaPosY = Eva.transform.position.y;
        data.InitialEvaPosZ = Eva.transform.position.z;

        binary.Serialize(fstream, data);
        fstream.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/saveFile.dat"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fstream = File.Open(Application.persistentDataPath + "/saveFile.dat", FileMode.Open);
            Data data = (Data)binary.Deserialize(fstream);
            fstream.Close();

            StateOfGame = data.StateOfGame;
            HorizontalSpawner = data.HorizontalSpawner;
            VerticalSpawner = data.VerticalSpawner;
            SceneName = data.SceneName;
            Beats[StateOfGame].BeginningPlatform = data.Beats[data.StateOfGame].BeginningPlatform;
            Beats[StateOfGame].CollectableBoost = data.Beats[data.StateOfGame].CollectableBoost;
            Beats[StateOfGame].endlingPlatform = data.Beats[data.StateOfGame].endlingPlatform;
            Beats[StateOfGame].ObstacleInstances = data.Beats[data.StateOfGame].ObstacleInstances;

            InitialPlayerPosX = data.InitialPlayerPosX;
            InitialPlayerPosY = data.InitialPlayerPosY;
            InitialPlayerPosZ = data.InitialPlayerPosZ;

            InitialEvaPosX = data.InitialEvaPosX;
            InitialEvaPosY = data.InitialEvaPosY;
            InitialEvaPosZ = data.InitialEvaPosZ;
        }
    }
}

[System.Serializable]
class Data
{
    public int StateOfGame;
    public string SceneName;

    //saving game data
    public enum GameState
    {
        Horizonal,
        Vertical
    };

    [System.Serializable]
    public struct BeatList
    {
        public GameState State;
        public float CollectableBoost;
        public GameObject[] ObstacleInstances;
        public GameObject BeginningPlatform;
        public GameObject endlingPlatform; 
    }

    public HorizontalSpawner HorizontalSpawner;
    public VerticalSpawner VerticalSpawner;
    public BeatList[] Beats;

    //saving player data
    public float InitialPlayerPosX;
    public float InitialPlayerPosY;
    public float InitialPlayerPosZ;

    //saving Eva data
    public float InitialEvaPosX;
    public float InitialEvaPosY;
    public float InitialEvaPosZ;
}
