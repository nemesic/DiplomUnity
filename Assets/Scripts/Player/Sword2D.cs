using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword2D : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    private BoxCollider2D collider2D;
    public Animator animator;
    public GameObject swordFather;

    void Start()
    {
        playerSpriteRenderer = transform.root.GetComponent<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (playerSpriteRenderer.flipX == true)
        {
            swordFather.transform.rotation = Quaternion.Euler(0, -179, 0);
        }
        else
        {
            swordFather.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Attack()
    {
        animator.Play("attack");
        collider2D.enabled = true;
        Invoke("DisableAttack", 2f);
    }

    private void DisableAttack()
    {
        collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
