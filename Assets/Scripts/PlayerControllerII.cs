using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerII : MonoBehaviour
{
    private const float LANE_DISTANCE = 4.5f;
    private const float TURN_SPEED = 0.05f;

    // For the animations
    //private Animator anim;

    //
    private bool isRunning = false;

    //Movements
    private CharacterController controller;
    private float jumpForce = 10.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    public int lane = 1;//0 = Left, 1 = Midddle, 2 = Right

    // Modification for the speed and Management
    private float originalSpeed = 10.0f;
    private float speed;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isRunning)
        {
            return;
        }

        if(Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            GameManager.Instance.UpdateModifier(speed - originalSpeed);
        }

        //Lane Movements and the inputs
        if (MobileInput.Instance.SwipeLeft)
        {
            //Move Left
            MoveLane(false);
        }
        else if (MobileInput.Instance.SwipeRight)
        {
            //Move Right
            MoveLane(true);
        }
        
       

        //whereis the player aiming to move to
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if(lane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if(lane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        //Calculate movement Vector
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        bool isGrounded = IsGrounded();


        if(IsGrounded())//if Grounded
        {
            verticalVelocity = -0.1f;

            if (MobileInput.Instance.SwipeUp)
            {
                //Jump Up
                verticalVelocity = jumpForce;
            }
            else if (MobileInput.Instance.SwipeDown)
            {
                // Slide
                StartSliding();
                Invoke("StopSliding", 1.0f);
            }
        }
        else
        {
            //Falling if in air
            verticalVelocity -= (gravity * Time.deltaTime);

            //Fast falling Mechanic
            if (MobileInput.Instance.SwipeDown)
            {
                //Slide Down
                verticalVelocity = -jumpForce;
            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //move Player
        controller.Move(moveVector * Time.deltaTime);

        //Rotate Player to where he is moving
        Vector3 dir = controller.velocity;
        if(dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }
    }

    private void MoveLane(bool goingRight)
    {
        lane += (goingRight) ? 1 : -1;
        lane = Mathf.Clamp(lane, 0, 2);
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);

        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);

        return Physics.Raycast(groundRay, 0.2f + 0.1f);
    }

    private void StartSliding()
    {
        //anim.setbool("Sliding", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
    }

    private void StopSliding()
    {
        //anim.setbool("Sliding", true);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }

    public void StartRunning()
    {
        isRunning = true;
    }

   public void Crash()
    {
        // Death animation goes here
        isRunning = false;
        GameManager.Instance.IsDead = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
                break;
        }
    }
}
