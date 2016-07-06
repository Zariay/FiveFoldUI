using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        Running,
        Jumping,
        Ducking,
        Dashing,
        LaneSwitch,
        Dead
    }

    public GameObject Locator1, Locator2, Locator3, PlayerLocation;
    public float Speed = 9.0f;
    public float JumpSpeed = 1.0f;
    public float SlideTime = 1.0f;
    public float DashTime = 1.0f;
    public float DashSpeed = 9.0f;
    public float SnapSpeed = 15.0f;
    public GameManager Manager;

    private Vector3 Movement;
    Rigidbody PlayerRB;
    private State CurrentState;
    private int CurrentLane;
    private bool inBetween;
    private float Timer = 0.0f;


    void Awake()
    {
        PlayerRB = GetComponent<Rigidbody>();
        CurrentState = State.Running;
        CurrentLane = 2;
        inBetween = true;
    }

    void FixedUpdate()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");
        float jump = Input.GetAxisRaw("Jump");
        float slide = Input.GetAxisRaw("Fire1");
        float dash = Input.GetAxisRaw("Fire2");

        switch (CurrentState)
        {
            case State.Running:
                Move(xAxis, zAxis);
                Jump(jump);
                Slide(slide);
                Dash(dash, xAxis, zAxis);
                break;
            case State.Jumping:
                Move(xAxis, zAxis);
                break;
            case State.Ducking:
                //Move( xAxis, zAxis );
                Check(SlideTime);
                break;
            case State.LaneSwitch:
                //Move( xAxis, zAxis );
                Check(SlideTime);
                break;
            case State.Dashing:
                PlayerRB.MovePosition(this.transform.position + Movement * Time.deltaTime);
                Check(DashTime);
                break;
            case State.Dead:
                break;
        }
        //Animate();

    }

    void Check(float StateTimer)
    {

        if (Timer < StateTimer)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            CurrentState = State.Running;
        }
    }

    void Move(float xAxis, float zAxis)
    {

        float Cur_Pos_z = this.transform.position.z;
        float Player_Pos = PlayerLocation.transform.position.x - this.transform.position.x;
        float Snap_Final = 1.0f;

        //if (Mathf.Abs(Snap_Pos_1) < Mathf.Abs(Snap_Pos_2) && Mathf.Abs(Snap_Pos_1) < Mathf.Abs(Snap_Pos_3))
        //    Snap_Final = Snap_Pos_1;
        //else if (Mathf.Abs(Snap_Pos_2) < Mathf.Abs(Snap_Pos_3))
        //    Snap_Final = Snap_Pos_2;
        //else
        //    Snap_Final = Snap_Pos_3;
        
        switch (CurrentLane)
        {
            case 1:
                Snap_Final = Locator1.transform.position.z - Cur_Pos_z;
                if (zAxis > 0 && !inBetween)
                {
                    CurrentLane = 2;
                    inBetween = true;
                    Snap_Final = Locator2.transform.position.z - Cur_Pos_z;
                }
                break;
            case 2:
                Snap_Final = Locator2.transform.position.z - Cur_Pos_z;
                if (zAxis > 0 && !inBetween)
                {
                    CurrentLane = 3;
                    inBetween = true;
                    Snap_Final = Locator3.transform.position.z - Cur_Pos_z;
                }
                else if (zAxis < 0 && !inBetween)
                {
                    CurrentLane = 1;
                    inBetween = true;
                    Snap_Final = Locator1.transform.position.z - Cur_Pos_z;
                }
                break;
            case 3:
                Snap_Final = Locator3.transform.position.z - Cur_Pos_z;
                if (zAxis < 0 && !inBetween)
                {
                    CurrentLane = 2;
                    inBetween = true;
                    Snap_Final = Locator2.transform.position.z - Cur_Pos_z;
                }
                break;
            default:
                break;
        }

        Vector3 TempMovement = new Vector3(0.0f, 0.0f, 0.0f);
        TempMovement = this.transform.position;

        if (Mathf.Abs(Snap_Final) > 0.1f)
        {
            Movement = new Vector3(0.0f, 0.0f, 1.0f);
            //int push = (Snap_Final > 0) ? 1 : -1;
            Movement = Movement.normalized * SnapSpeed * Time.deltaTime * 0.1f;

            // print( Movement.z );
            TempMovement += Movement * Snap_Final;
            //PlayerRB.AddForce( Movement );
        }
        else
        {
            inBetween = false;
        }

        if (Mathf.Abs(Player_Pos) > 0.1f)
        {
            Movement = new Vector3(1.0f, 0.0f, 0.0f);
            Movement = Movement.normalized * Speed * Time.deltaTime * 0.1f;
            TempMovement += Movement * Player_Pos;
        }

        Movement = new Vector3(xAxis, 0.0f, 0.0f);
        Movement = Movement.normalized * Speed * Time.deltaTime; //to eliminate the extra fast movement at the diagonals... 

        PlayerRB.MovePosition(TempMovement + Movement);

    }

    void Jump(float jump)
    {
        if (jump == 1)
        {
           // Movement = new Vector3(0.0f, 1.0f, 0.0f);
           // Movement = Movement.normalized * (JumpSpeed); //hardcoded. bad. so bad.
            PlayerRB.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
            CurrentState = State.Jumping;
        }
    }

    void Slide(float slide)
    {
        if (slide == 1)
        {
            Timer = 0.0f;
            CurrentState = State.Ducking;
        }
    }

    void Dash(float dash, float xAxis, float zAxis)
    {
        if (dash == 1)
        {
            Timer = 0.0f;
            Movement = new Vector3(xAxis, 0.0f, zAxis);
            Movement = Movement.normalized * DashSpeed;
            CurrentState = State.Dashing;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            CurrentState = State.Running;
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
        switch (Manager.Beats[Manager.StateOfGame].State)
        {
            case GameManager.GameState.Horizonal:
                if (collision.gameObject.tag == "Obstacle")
                {
                    PlayerLocation.transform.position -= new Vector3(Manager.CollectableBoost, 0.0f, 0.0f);
                    Debug.Log("Hello");
                }
                if (collision.gameObject.tag == "Item")
                    PlayerLocation.transform.position += new Vector3(Manager.CollectableBoost, 0.0f, 0.0f);
                //float difference = PlayerTarget.x - Player.transform.position.x;
                //difference *= 0.1f;
                //Vector3 Chase = new Vector3(1.0f, 0.0f, 0.0f);
                //Chase.x = TimerSpeed * Time.deltaTime * difference;
                //
                //Player.transform.position += Chase;
                break;
            case GameManager.GameState.Vertical:
                break;
            default:
                break;
        }
        if (collision.gameObject.tag == "Eva")
        {
            Manager.ChangeBeat(); 
        }
    }

}
