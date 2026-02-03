using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance {  get; private set; }

    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;

    private float minMovinSpeed = 0.1f;
    private bool isRunning = false;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * (speed * Time.fixedDeltaTime));

        if (Mathf.Abs(inputVector.x) > minMovinSpeed || Mathf.Abs(inputVector.y) > minMovinSpeed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

    }

    public bool IsRunning()
    {
        return isRunning;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
}
