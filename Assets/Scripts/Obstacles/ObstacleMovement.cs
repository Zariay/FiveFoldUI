using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour
{
    public static float movespeed = 1.0f;
    public Vector3 userDirection = Vector3.left;
    public Transform EndPoint;
    private bool isActive;
    public Collider Ground;

    void Awake()
    {
        transform.position = Vector3.zero;
        isActive = false;
        
       // Velocity = Vector3.zero;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( userDirection * movespeed * Time.deltaTime ); 
    
    
    }
    void OnTriggerEntered( Collider other )
    {
        gameObject.SetActive( false );
        isActive = false;
    }
    
    public void Spawn( Vector3 pos )
    {
        transform.position = pos;
        //Velocity = vel;
        gameObject.SetActive( true );
        isActive = true;
    }
    public bool IsActive(  )
    {
        return isActive;
    }
    
    public Vector3 GetEndPoint()
    {
        return EndPoint.position;
    }
    public float GetBeginningPoint()
    {
        return this.transform.position.x;
    }
    public void SetSpeed(float speed)
    {
        movespeed = speed;
    }
}
