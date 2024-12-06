using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
    public MonoBehaviour scriptToToggle; // Reference the script you want to toggle
    public float moveDistance = 1f; // Set the fixed distance to move
    public KeyCode moveKeyLeft = KeyCode.LeftArrow; // The key to trigger movement (can be any key)
    public KeyCode moveKeyRight = KeyCode.RightArrow; // The key to trigger movement (can be any key)
    private Vector2 swipeStart;
    private bool isMoving = false;
    private Vector3 targetPosition;
    public float gridSize = 1.0f;
    public float baseSpeed = 2f; // Default speed for continuous movement
    public float boostMultiplier = 10f; // Speed multiplier when input is detected
    public KeyCode boostDownKey = KeyCode.DownArrow; // Key to increase speed
    private float currentSpeed;
    public Vector3 gridOrigin = Vector3.zero;
    public BlockSpawner blockSpawner;

    void Update()
    {
        blockSpawner = FindObjectOfType<BlockSpawner>();
        // Check for keyboard input
        if (Input.GetKeyDown(moveKeyRight))
        {
            SnapAlongX(10);
        }
        if (Input.GetKeyDown(moveKeyLeft))
        {
                SnapAlongX(-10);
        }

        // Check for touch input (swipe)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended && !isMoving)
            {
                Vector2 swipeEnd = touch.position;
                Vector2 swipeDelta = swipeEnd - swipeStart;

                if (swipeDelta.magnitude > 50f) // Adjust sensitivity as needed
                {
                    Vector3 direction = swipeDelta.x > 0 ? Vector3.right : Vector3.left;
                    Move(direction);
                }
            }
        }
        // Check if boost key is held down or if there is an active touch input
        bool isBoosting = Input.GetKey(boostDownKey) || Input.touchCount > 0;

        // Adjust speed based on input
        currentSpeed = isBoosting ? baseSpeed * boostMultiplier : baseSpeed;

        // Move the object continuously in a chosen direction (e.g., right)
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Stop falling when it hits the bottom or another block
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("BlockTop"))
        {
            Debug.Log("Vertical block contact");

            SnapToGrid();
            LockBlock();
            // Despawn or deactivate this script
            ToggleScriptOff();

            // Spawn a new block at the top (you can have a block spawner)
            blockSpawner.SpawnRandomTypeBlock();
        }
        // Stop horizontal movement when it hits the walls or another block
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("BlockSide"))
        {
            //CancelHorizontalMovement();
        }
    }
    private void SnapToGrid()
    {
        transform.position = GridUtils.SnapToGrid(transform.position, gridSize);
    }

    /*    private void SnapAlongX(int direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, moveDistance);
            // Check if the ray hit anything
            if (hit.collider == null) // No collision, proceed with snapping
            {
                transform.position = GridUtils.SnapToGridOnAxis(transform.position, gridSize, gridOrigin, "x", direction);
            }
            else
            {
                Debug.Log("Blocked by: " + hit.collider.gameObject.name); // Log what blocked the movement
            }
        }*/
    private void SnapAlongX(int direction)
    {
        // Calculate the target position along the X-axis
        Vector3 targetPosition = GridUtils.SnapToGridOnAxis(transform.position, gridSize, gridOrigin, "x", direction);

        // Check if the target position is occupied
        Collider2D collider = Physics2D.OverlapBox(targetPosition, GetComponent<Collider2D>().bounds.size, 0f, LayerMask.GetMask("Blocks", "Walls", "Default") // Adjust to include relevant layers
        );

        if (collider == null)
        {
            // No collision detected; move the block
            transform.position = targetPosition;
        }
        else
        {
            Debug.Log("Blocked by: " + collider.gameObject.name);
        }
    }

  /*  private void SnapAlongX(int direction)
    {
        // Calculate the target position on the X-axis using GridUtils
        Vector3 targetPosition = GridUtils.SnapToGridOnAxis(transform.position, gridSize, gridOrigin, "x", direction);

        // Cast a ray to ensure the target position is not blocked
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y);
        Vector2 rayDirection = direction > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, Mathf.Abs(targetPosition.x - transform.position.x));

        if (hit.collider == null) // No obstacle detected
        {
            transform.position = targetPosition;
        }
        else
        {
            Debug.Log("Cannot snap along X, blocked by: " + hit.collider.gameObject.name);
        }
    }*/
    private void Move(Vector3 direction)
    {
        isMoving = true;
        targetPosition = transform.position + direction * moveDistance;
    }
    void ToggleScript()
    {
        if (scriptToToggle != null)
        {
            scriptToToggle.enabled = !scriptToToggle.enabled; // Toggle the script's enabled state
            Debug.Log($"{scriptToToggle.GetType().Name} is now {(scriptToToggle.enabled ? "enabled" : "disabled")}");
        }
    }
    void ToggleScriptOff()
    {
        if (scriptToToggle != null)
        {
            scriptToToggle.enabled = false; // Toggle the script's enabled state
            Debug.Log($"{scriptToToggle.GetType().Name} is now {(scriptToToggle.enabled ? "enabled" : "disabled")}");
        }
    }
    private void LockBlock()
    {
        // Stop any movement
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;       // Stop the Rigidbody's velocity
            rb.angularVelocity = 0f;         // Stop any rotation
        }

        rb.bodyType = RigidbodyType2D.Static; // Make the object static
    }
    /*  void CancelHorizontalMovement()
      {
          movement.x = 0; // Stop horizontal movement
      }*/
}
