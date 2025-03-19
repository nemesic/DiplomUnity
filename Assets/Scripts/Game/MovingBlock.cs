using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float upSpeed = 1f; // �������� �������� �����
    [SerializeField] private float downSpeed = 2f; // �������� �������� ����
    private bool movingUp = true;

    private void Start()
    {
        // ������� ��������� waypoint � ��������� ������� ���������
        float minDistance = float.MaxValue;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, waypoints[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                currentWaypointIndex = i;
            }
        }
    }

    private void Update()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            // ��������� ����������� ��������
            Vector2 direction = waypoints[currentWaypointIndex].transform.position - transform.position;

            // ������������� �������� � ������������ � ������������
            if (movingUp)
            {
                transform.position += (Vector3)direction.normalized * upSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += (Vector3)direction.normalized * downSpeed * Time.deltaTime;
            }

            // ���� ��������� ������������ � �������� waypoint'�
            if (direction.magnitude < .1f)
            {
                // ������������� �� ��������� waypoint
                currentWaypointIndex++;

                // ���������, �������� �� ����� ������� waypoint'��
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }

                // ��������� ����������� ��������
                UpdateMovementDirection();
            }
        }
    }

    // ����� ��� ���������� ����������� ��������
    private void UpdateMovementDirection()
    {
        if (currentWaypointIndex == 0)
        {
            movingUp = true;
        }
        else if (currentWaypointIndex == 1)
        {
            movingUp = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}