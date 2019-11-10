using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x == 0 && y == 0)
        {
            animator.SetBool("isIdle", true);
        } else
        {
            animator.SetBool("isIdle", false);
        }

        if (x > 0)
        {
            animator.SetBool("isSideWalking", true);
            spriteRenderer.flipX = true;
            return;
        }
        else if (x < 0)
        {
            animator.SetBool("isSideWalking", true);
            spriteRenderer.flipX = false;
            return;
        } else
        {
            animator.SetBool("isSideWalking", false);
        }

        if (y > 0)
        {
            animator.SetBool("isUpWalking", true);
            return;
        } else
        {
            animator.SetBool("isUpWalking", false);
        }

        if (y < 0)
        {
            animator.SetBool("isDownWalking", true);
            return;
        } else
        {
            animator.SetBool("isDownWalking", false);
        }

    }
}
