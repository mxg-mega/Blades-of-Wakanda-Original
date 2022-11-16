
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float  LANE_DISTANCE = 5.0f;
    private CharacterController controller;
    private float speed = 5.0f;
    private float verticalInput;
    private int desiredLane = 1; 


    // Start is called before the first frame update
   private void Start()
    {
      controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
   private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        MoveLane(false);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        MoveLane(true);

        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0) // left
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLane == 2) // right
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }
        // calculate delta move
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;
        moveVector.y = -0.1f;
        moveVector.z = speed;

        //move wakanda
        controller.Move(moveVector * Time.deltaTime);

        
      //  verticalInput = Input.GetAxis("Vertical");
       // transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);
    }
    private void MoveLane (bool goingRight) {
        //left
 desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }
}
