using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Don vi thoi gian la giay

    private float moveSpeed = 2f;

    private float moveDuration;

    private enum MOVE_DIRECTION
    {
        LEFT, RIGHT, UP, DOWN, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT, TOTAL_DIRECTIONS
    }

    private const float MAX_MOVE_DURATION = 5f;

    private int moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = Random.Range(0, (int)MOVE_DIRECTION.TOTAL_DIRECTIONS);
        moveDuration = Random.Range(0, MAX_MOVE_DURATION);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
                int xAxisDirection = 0;
        int yAxisDirection = 0;

        if (moveDirection == (int)MOVE_DIRECTION.UP)
        {
            xAxisDirection = 0;
            yAxisDirection = 1;
        }
        if (moveDirection == (int)MOVE_DIRECTION.DOWN)
        {
            xAxisDirection = 0;
            yAxisDirection = -1;
        }
        if (moveDirection == (int)MOVE_DIRECTION.LEFT)
        {
            xAxisDirection = -1;
            yAxisDirection = 0;
        }
        if (moveDirection == (int)MOVE_DIRECTION.RIGHT)
        {
            xAxisDirection = 1;
            yAxisDirection = 0;
        }
        if (moveDirection == (int)MOVE_DIRECTION.UP_LEFT)
        {
            xAxisDirection = -1;
            yAxisDirection = 1;
        }
        if (moveDirection == (int)MOVE_DIRECTION.UP_RIGHT)
        {
            xAxisDirection = 1;
            yAxisDirection = 1;
        }
        if (moveDirection == (int)MOVE_DIRECTION.DOWN_LEFT)
        {
            xAxisDirection = -1;
            yAxisDirection = -1;
        }
        if (moveDirection == (int)MOVE_DIRECTION.DOWN_RIGHT)
        {
            xAxisDirection = 1;
            yAxisDirection = -1;
        }

        transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime * xAxisDirection, transform.position.y + moveSpeed * Time.deltaTime * yAxisDirection);

        moveDuration -= Time.deltaTime;
        Debug.Log(moveDuration);

        if (moveDuration <= 0)
        {
            moveDuration = Random.Range(0, MAX_MOVE_DURATION);
            moveDirection = Random.Range(0, (int)MOVE_DIRECTION.TOTAL_DIRECTIONS);
        }
    }
}
