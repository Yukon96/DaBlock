using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoverTouchscreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private Vector2 startTouchPosition, endTouchPosition;
    public float fallSpeed = 2f;       // Speed of constant downward movement

    // Update is called once per frame
    void Update()
    {
        //// Check if there are any touches
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0); // Get the first touch

        //    // Convert touch position to world space
        //    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

        //    // Move the object to the touch position
        //    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
        //    {
        //        transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);
        //    }
        //}
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Detect the touch start
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            // Detect the touch end and move the object accordingly
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Vector2 swipeDirection = endTouchPosition - startTouchPosition;

                if (swipeDirection.x > 0)
                {
                    // Move right
                    transform.Translate(Vector3.right);
                }
                else if (swipeDirection.x < 0)
                {
                    // Move left
                    transform.Translate(Vector3.left);
                }
            
            }
        }

    }
}
