using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDamage : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriterenderer;
    public GameObject destroyPaticle;
    public float jumpForce = 5f;
    public int lifes = 1;
    public bool isDead = false;
    private bool canJump = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && canJump)
        {
            if (collision.transform.CompareTag("Player"))
            {
                Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                playerRigidbody.velocity = Vector2.up * jumpForce;
                LoseLifeAndHit(collision.gameObject);
                CheckLife();
            }
        }
        else if (collision.transform.CompareTag("Player"))
        {
            if (lifes > 0)
            {
                LoseLifeAndHit(collision.gameObject);
            }
        }
    }

    public void LoseLifeAndHit(GameObject player)
    {
        if (!isDead)
        {
            lifes--;
            animator.Play("Hit");

            if (lifes > 0)
            {
                PlayerLife playerLife = player.GetComponent<PlayerLife>();
                if (playerLife != null)
                {
                    playerLife.TakeDamage();
                }
            }
        }
    }

    public void CheckLife()
    {
        if (lifes == 0)
        {
            isDead = true;
            destroyPaticle.SetActive(true);
            spriterenderer.enabled = false;
            StartCoroutine(DestroyEnemy());
        }
    }

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}