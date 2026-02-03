using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance {  get; private set; }

    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;

    private int num_anim = 0;

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


        if ((inputVector.y > 0.1 && (Mathf.Abs(inputVector.x) > 0.1)) || (inputVector.y > 0.1))
        {
            num_anim = 1; // вверх
        }
        else if ((inputVector.y < -0.1 && (Mathf.Abs(inputVector.x) > 0.1)) || (inputVector.y < -0.1))
        {
            num_anim = 2; //вниз
        }
        else if(inputVector.x < -0.1)
        {
            num_anim = 3; //лево
        }
        else if (inputVector.x > 0.1)
        {
            num_anim = 4; //вправо
        }
        else
        {
            num_anim = 0; // на месте
        }

    }

    public int Num_anim()
    {
        return num_anim;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
}
