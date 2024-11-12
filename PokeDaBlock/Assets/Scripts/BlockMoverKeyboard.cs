using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoverKeyboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }
    public float fallSpeed = 2f;       // Speed of constant downward movement

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position; // Changed to Vector3

        position.x += 20f * horizontal;  //* Time.deltaTime;
        position.y += 20f * vertical; //* Time.deltaTime;

        transform.position = position;  
    }

}
