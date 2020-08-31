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
        if (GameManager.instance.IsPaused) return;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x > 0)
        {
            animator.SetBool("isSideWalking", true);
            spriteRenderer.flipX = true;
        }
        else if (x < 0)
        {
            animator.SetBool("isSideWalking", true);
            spriteRenderer.flipX = false;
        }
        else
        {
            animator.SetBool("isSideWalking", false);
        }

        if (y > 0)
        {
            animator.SetBool("isUpWalking", true);
        }
        else
        {
            animator.SetBool("isUpWalking", false);
        }

        if (y < 0)
        {
            animator.SetBool("isDownWalking", true);
        }
        else
        {
            animator.SetBool("isDownWalking", false);
        }
    }
}
