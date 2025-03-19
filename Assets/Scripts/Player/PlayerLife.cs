using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject[] hearts;
    private int life = 3;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            JumpDamage enemy = collision.gameObject.GetComponent<JumpDamage>();
            if (enemy != null && !enemy.isDead)
            {
                TakeDamage();
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < life);
        }
    }

    public void TakeDamage()
    {
        life--;

        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        Invoke("RestartLevel", 0.5f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}