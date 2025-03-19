using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 20f;
    public Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0f);
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                if (animator != null)
                {
                    animator.Play("JumpTrampoline");
                }
                else
                {
                    Debug.LogWarning("Animator is not assigned to the Trampoline object.");
                }
            }
            else
            {
                Debug.LogWarning("Player object does not have a Rigidbody2D component.");
            }
        }
    }
}