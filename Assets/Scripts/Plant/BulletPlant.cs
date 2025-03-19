using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlant : MonoBehaviour
{
    public float speed = 2;
    public float lifeTime = 1;
    public bool left;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        if (left)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Отримання скрипту гравця і зменшення його здоров'я
            PlayerLife playerLife = collision.gameObject.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(); // Прийняття шкоди гравцем

                // Видалення пулі зразу після попадання
                Destroy(gameObject);
            }
        }
    }
}
