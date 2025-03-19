using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material mat;
    float distance;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;

    public enum Direction
    {
        Right,
        Up,
        Down
    }

    public Direction moveDirection = Direction.Right;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector2 offset = Vector2.zero;

        switch (moveDirection)
        {
            case Direction.Right:
                offset = Vector2.right * distance;
                break;
            case Direction.Up:
                offset = Vector2.up * distance;
                break;
            case Direction.Down:
                offset = Vector2.down * distance;
                break;
        }

        distance += Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", offset);
    }
}
