using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerII : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0f, 6f, -4.0f);

    //public bool IsMoving { set; get; }

    // Start is called before the first frame update
    void Start()
    {
        // offset the camera behind the player by adding to the player's position
        transform.position = player.transform.position + offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if (!IsMoving)
            //return;

        Vector3 desiredPosition = player.transform.position + offset;
        //desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
    }
}
