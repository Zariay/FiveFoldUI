using UnityEngine;
using System.Collections;

public class DestroyObj : MonoBehaviour 
{

    public float destroyTime = 3.5f;

	// Use this for initialization
	void Start () 
    {
        Destroy(gameObject, destroyTime);
	}
	
}
