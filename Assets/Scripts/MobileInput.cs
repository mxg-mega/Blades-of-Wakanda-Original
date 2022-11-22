using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    private const float DEADZONE = 100;

    public static MobileInput Instance { set; get; }

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeMovement, startTouch;

    public bool Tap { get { return tap; } }
    public Vector2 SwipeMovement { get { return swipeMovement; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //resetting all booleans
        tap = swipeDown = swipeLeft = swipeRight = swipeUp = false;

        #region Standalone Inputs

        //check inputs of the mouse
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeMovement = Vector2.zero;
        }
        #endregion

        #region Mobile Inputs

        //check inputs of the mouse
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeMovement = Vector2.zero;
            }
        }
        #endregion

        swipeMovement = Vector2.zero;
        if(startTouch != Vector2.zero)
        {
            // Let's check with mobile
            if(Input.touches.Length != 0)
            {
                swipeMovement = Input.touches[0].position - startTouch;
            }
            // Check with Standalone inputs i.e mouse of the PC
            else if (Input.GetMouseButton(0))
            {
                swipeMovement = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if(swipeMovement.magnitude > DEADZONE)
        {
            // Confirm which side we move to
            float x = swipeMovement.x;
            float y = swipeMovement.y;

            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Move Left or Right
                if(x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            startTouch = swipeMovement = Vector2.zero;
        }
    }
}
