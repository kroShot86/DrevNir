using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance {  get; private set; }

    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * (speed * Time.fixedDeltaTime));
    }

    public Vector2 Player_pos()
    {
        return GameInput.Instance.GetMovementVector();
    }
}
