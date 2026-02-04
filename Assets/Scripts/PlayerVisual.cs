using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private int num_anim = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animator.SetInteger("num_anim", num_anim);
        Animation();
    }

    private void Animation()
    {
        Vector2 inputVector = Player.Instance.Player_pos();

        if ((inputVector.y > 0.1 && (Mathf.Abs(inputVector.x) > 0.1)) || (inputVector.y > 0.1))
        {
            spriteRenderer.flipX = false;
            num_anim = 1; // вверх
        }
        else if ((inputVector.y < -0.1 && (Mathf.Abs(inputVector.x) > 0.1)) || (inputVector.y < -0.1))
        {
            spriteRenderer.flipX = false;
            num_anim = 2; //вниз
        }
        else if (inputVector.x < -0.1)
        {
            spriteRenderer.flipX = true;
            num_anim = 3; //лево
        }
        else if (inputVector.x > 0.1)
        {
            spriteRenderer.flipX = false;
            num_anim = 3; //вправо
        }
        else
        {
            spriteRenderer.flipX = false;
            num_anim = 0; // на месте
        }
    }

}
