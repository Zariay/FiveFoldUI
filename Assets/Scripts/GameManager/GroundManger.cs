using UnityEngine;
using System.Collections;

public class GroundManger : MonoBehaviour {

	public GameObject A_Type;
	public GameObject B_Type;
	public GameObject C_Type;

	public GameObject[] Grounds;
	
	public float speed = 0.5f;
	
	// Update is called once per frame
	void Update () {
		MoveGround();
	}

	void MoveGround()
	{
		A_Type.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
		B_Type.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
		C_Type.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
		if(B_Type.transform.position.x <= 0)
		{
			DestroyGround();
		}
		
	}

	void MakeGround()
	{
		A_Type = B_Type;
		B_Type = C_Type;
		int type = Random.Range(0, Grounds.Length);
		C_Type = Instantiate(Grounds[type], new Vector3(80, 0, 0), Quaternion.identity) as GameObject;
		C_Type.transform.parent = transform; 
	}

	void DestroyGround()
	{
		Destroy(A_Type);
		MakeGround();
	}


}
