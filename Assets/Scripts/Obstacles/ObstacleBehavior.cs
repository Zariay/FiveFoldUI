using UnityEngine;
using System.Collections;

public class ObstacleBehavior : MonoBehaviour 
{

    public GameObject Track1;
    public GameObject Track2;
    public GameObject Track3;
    
    public static float movespeed = 1.0f;
   // private bool isActive;

    void Awake()
    {
        //transform.position = Vector3.zero;
        //isActive = true;
        float Cur_Pos_z = this.transform.position.z;
        float Snap_Pos_1 = Track1.transform.position.z - Cur_Pos_z;
        float Snap_Pos_2 = Track2.transform.position.z - Cur_Pos_z;
        float Snap_Pos_3 = Track3.transform.position.z - Cur_Pos_z;
        float zValue = 0.0f;
        if( Mathf.Abs( Snap_Pos_1 ) < Mathf.Abs( Snap_Pos_2 ) && Mathf.Abs( Snap_Pos_1 ) < Mathf.Abs( Snap_Pos_3 ) )
            zValue = Track1.transform.position.z;
        else if( Mathf.Abs( Snap_Pos_2 ) < Mathf.Abs( Snap_Pos_3 ) )
            zValue = Track2.transform.position.z;
        else
            zValue = Track3.transform.position.z;

        Vector3 newPos = new Vector3( this.transform.position.x, this.transform.position.y, zValue );
        this.transform.position = newPos;

        this.GetComponent<BoxCollider>().isTrigger = true;
    }
}
