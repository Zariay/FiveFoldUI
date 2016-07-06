using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public float distanceDepth = 1.0f;

    public Slider distance;
    public GameManager Manager;

    public float Depth = 1.0f;
    public float RotationThreshold = 1.0f;


    void Update()
    {
        Vector3 currentPos = transform.position;
        currentPos.Normalize();
        float Player_Pos = player.position.x;
        float Target_Pos = target.position.x;

        float Distance = Mathf.Abs(Player_Pos - Target_Pos);
        float Mid = (Player_Pos + Target_Pos) * 0.5f;

        if (Distance > RotationThreshold)
            transform.position = currentPos * Depth * Distance;

        if (Camera.main.orthographicSize >= 7.0f)
        {
            Camera.main.orthographicSize = (Depth * Distance > 7.0f ? Depth * Distance : 7.0f);
        }


        Vector3 MidPoint = new Vector3(Mid, 1.0f, 0.0f);
       //Vector3 RotationTweak = transform.position;
       //RotationTweak.y++;
       //transform.position = RotationTweak;
        transform.LookAt(MidPoint);
    }
}
