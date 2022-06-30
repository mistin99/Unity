using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputs : MonoBehaviour
{
    private const float Deadzone = 100.0f;
    public static MobileInputs Instance { get; set; }
    private bool tap,swipeLeft,swipeRight,swipeUp,swipeDown;
    private Vector2 swipeDelta, startTouch;

    public bool Tap { get { return tap; }} 
    public Vector2 SwipeDelta { get { return swipeDelta; }}
    public bool SwipeUp { get { return swipeUp; }}
    public bool SwipeDown { get { return swipeDown; }}
    public bool SwipeLeft { get { return swipeLeft; }}
    public bool SwipeRight { get { return swipeRight; }}

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //resset all bools to false
        tap = swipeLeft = swipeRight = swipeDown = swipeUp = false;

        //check inputs
        #region StandaLone Inputs
        if(Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }
        #endregion

        #region MobileInputs
        if (Input.touches.Length != 0)
        {
           if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if ((Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled))
            {
                startTouch = swipeDelta = Vector2.zero;
            }
        }
       
        #endregion

        // Calculate Distance
        swipeDelta = Vector2.zero;
        if(startTouch != Vector2.zero)
        {
            // Check with mobile
            if(Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }

            // Check with stanalone
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;

            }
        }

        // Check if beyond deadzone
        if(swipeDelta.magnitude > Deadzone)
        {
            //confirmed swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or Right
                if(x< 0)
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
                // Up or Down
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            startTouch = swipeDelta = Vector2.zero ;
        }

    }
}
