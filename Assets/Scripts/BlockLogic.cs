using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class BlockLogic : MonoBehaviour
{
    public Vector3 rotationPoint;
    public Vector2 startPos;
    public Vector2 direction;
    GameLogic gameLogic;
    LoadLevel loadLevel;
    public bool movable = true;
    float timer = 0f;
    float waitForMove= 0.2f;
    float awaitingTimer = 0f;
    Vector3 moveSidewaysSpace = new Vector3(1f, 0, 0);
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
        loadLevel = FindObjectOfType<LoadLevel>();
    }
    void RegisterBlock()
    {
        foreach (Transform subBlock in transform)
        {
            if (subBlock.gameObject.transform.position.y > GameLogic.height)
            {
                loadLevel.GameOver();
            }
            else
            {
                gameLogic.grid[Mathf.FloorToInt(subBlock.position.x), Mathf.FloorToInt(subBlock.position.y)] = subBlock;
            }
        }
    }
    bool CheckValid()
    {
        foreach (Transform subBlock in transform)
        {
            if (subBlock.transform.position.x > GameLogic.width ||
                subBlock.transform.position.x < 1 ||
                subBlock.transform.position.y < 2)
            {
                return false;
            }
            if (subBlock.position.y < GameLogic.height && gameLogic.grid[Mathf.FloorToInt(subBlock.position.x), Mathf.FloorToInt(subBlock.position.y)] != null)
            {
                return false;
            }
        }
        return true;
    }
    void Update()
    {
        if (movable)
        {
            // Update the timer
            timer += 1 * Time.deltaTime;
            if (timer > gameLogic.dropTime)
            {
                gameObject.transform.position -= new Vector3(0, 1f, 0);
                timer = 0;
                if (!CheckValid())
                {
                    movable = false;
                    gameObject.transform.position += new Vector3(0, 1f, 0);
                    RegisterBlock();
                    gameLogic.CheckForLines();
                    gameLogic.SpawnBlock();
                }
            }
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    waitForMove = 0.2f;
                    startPos = touch.position;
                }
                if (touch.phase == TouchPhase.Moved && awaitingTimer < waitForMove)
                {
                    direction = touch.position - startPos;
                    // Down Movement
                    if (direction.y <= -10f && timer > gameLogic.quickDropTime)
                    {
                        gameObject.transform.position -= new Vector3(0, 1f, 0);
                        timer = 0;
                        if (!CheckValid())
                        {
                            movable = false;
                            transform.position += new Vector3(0, 1f, 0);
                            RegisterBlock();
                            gameLogic.CheckForLines();
                            gameLogic.SpawnBlock();
                        }
                    }
                    // Sideways movement
                    else if (direction.x <= -100f) // to the left
                    {
                        waitForMove = 0f;
                        transform.position -= moveSidewaysSpace;
                        if (!CheckValid())
                        {
                            transform.position += new Vector3(1f, 0, 0);
                        }
                    }
                    else if (direction.x >= 100f) // to the right
                    {
                        waitForMove = 0f;
                        transform.position += moveSidewaysSpace;
                        if (!CheckValid())
                        {
                            transform.position -= new Vector3(1f, 0, 0);
                        }
                    }
                    // Rotation
                    else if (direction.y >=100f) // Up Swipe
                    {
                        waitForMove = 0f;
                        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                        if (!CheckValid())
                        {
                            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                        }
                    }
                }
            }
        }
    }
}