using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockMovement : MonoBehaviour
{
    public float speed = 2.0f;
    private Vector3 laneDistance = new(0, 0, 5);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        transform.Translate((Vector3.left - laneDistance) * speed * Time.deltaTime);
    }

    private void MoveRight()
    {
        transform.Translate((Vector3.right + laneDistance) * speed * Time.deltaTime);
    }
}
