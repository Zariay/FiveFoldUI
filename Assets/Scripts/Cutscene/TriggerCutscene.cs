using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerCutscene : MonoBehaviour
{
    public GameManager Manager;

    void Start()
    {
        Manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Manager.ChangedBeat == true)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("Cutscene1", LoadSceneMode.Additive);
        }
    }


}
