using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    // Don vi thoi gian la giay

    [SerializeField]
    private float moveSpeed = 2f;

    private float moveDuration;

    private enum MOVE_DIRECTION
    {
        LEFT, RIGHT, UP, DOWN, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT, TOTAL_DIRECTIONS
    }

    private const float MAX_MOVE_DURATION = 5f;

    private int moveDirection;

    private int xAxisDirection = 0;

    private int yAxisDirection = 0;

    [SerializeField]
    private Transform waterSurfaceTransform;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = Random.Range(0, (int)MOVE_DIRECTION.TOTAL_DIRECTIONS);
        moveDuration = Random.Range(0, MAX_MOVE_DURATION);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PreventMoveAboveWaterSurface() && !PreventMoveBelowCamera())
        {
            SetMoveDirection();
        }

        Move();
    }

    void SetMoveDirection()
    {
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
    }

    void Move()
    {
        transform.position += new Vector3(moveSpeed * Time.deltaTime * xAxisDirection, moveSpeed * Time.deltaTime * yAxisDirection, 0);

        moveDuration -= Time.deltaTime;

        if (moveDuration <= 0)
        {
            moveDuration = Random.Range(0, MAX_MOVE_DURATION);
            moveDirection = Random.Range(0, (int)MOVE_DIRECTION.TOTAL_DIRECTIONS);
        }
    }

    private bool PreventMoveAboveWaterSurface()
    {
        if (transform.position.y >= waterSurfaceTransform.position.y)
        {
            if (yAxisDirection != -1) yAxisDirection = -1;
            return true;
        }
        return false;
    }

    private bool PreventMoveBelowCamera()
    {
        Vector3 cameraBottomVector = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        if (transform.position.y <= cameraBottomVector.y)
        {
            if (yAxisDirection != 1) yAxisDirection = 1;
            return true;
        }
        return false;
    }
}