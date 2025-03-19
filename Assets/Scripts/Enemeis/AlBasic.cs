using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlBasic : MonoBehaviour
{
    public Animator animator;
    public float speed = 0.5f;
    public float startWaitTime = 2;
    public float restTime = 2;
    public Transform[] moveSpots;

    private int currentSpotIndex = 0;
    private float waitTime;
    private float restTimer;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastPosition;
    private bool isRun = false;
    private bool isResting = false;

    private void Start()
    {
        waitTime = startWaitTime;
        restTimer = restTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (!isResting)
        {
            MoveToNextSpot();
            CheckMovingDirection();
            UpdateAnimator();
        }
        else
        {
            Rest();
        }
    }

    private void MoveToNextSpot()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[currentSpotIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[currentSpotIndex].position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                currentSpotIndex = (currentSpotIndex + 1) % moveSpots.Length;
                waitTime = startWaitTime;
                isResting = true;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void CheckMovingDirection()
    {
        if (transform.position.x > lastPosition.x)
        {
            spriteRenderer.flipX = true;
            isRun = true;
        }
        else if (transform.position.x < lastPosition.x)
        {
            spriteRenderer.flipX = false;
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        lastPosition = transform.position;
    }

    private void UpdateAnimator()
    {
        if (animator != null)
        {
            animator.SetBool("Run", isRun);
            animator.SetBool("Idle", !isRun);
        }
    }

    private void Rest()
    {
        if (restTimer <= 0)
        {
            isResting = false;
            restTimer = restTime;
        }
        else
        {
            restTimer -= Time.deltaTime;
        }
    }
}