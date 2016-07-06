using UnityEngine;
using System.Collections;

public class ItemCollection : MonoBehaviour {

    void OnTriggerEnter( Collider other )
    {
        if( other.CompareTag( "Item" ) )
        {
            other.gameObject.SetActive( false );


        }
    }

}
