using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    public float Speed = 10.0f;
    private GameManager player;
    //private GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isGamestarted)
            transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(gameObject.transform.position.z == 0)
        {

        }
    }*/
}
