using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform wakanda; // player
    public Vector3 offset = new Vector3(0, 3.0f, -1.5f);  


                             
   
    // LateUpdate is called after the player has moved
    void LateUpdate()
    {


        // where the player should be
        Vector3 desiredPosition = wakanda.position + offset;
        desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
           }
}
