
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private const float LANE_DISTANCE = 3.0f;
    // Movement
     private float verticalVelocity;
    private float speed = 7.0f;

    private int desiredLane = 1; 
    // Start is called before the first frame update
     void Start()
    {
        //speed = defaultSpeed;
        controller = GetComponent<CharacterController>();
    }

        private void Update(){
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLane(false);


            if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveLane(true);

         // calculate where player should be
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0) // left
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLane == 2) // right
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

         // move vector calculation
        Vector3 moveVector = Vector3.zero;
                moveVector.x = (targetPosition - transform.position).normalized.x * speed; // where wakanda is supposed to be - where he is at the moment
                moveVector.y = -0.1f;
                moveVector.z = speed;
        // move wakanda
        controller.Move(moveVector * Time.deltaTime);        



        }

    private void MoveLane(bool goingRight)
    {
               desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }
}
