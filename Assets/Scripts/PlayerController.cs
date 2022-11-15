using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 10.0f;
    public float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);
    }
}
