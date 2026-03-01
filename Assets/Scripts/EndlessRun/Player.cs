using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRunPlayer : MonoBehaviour
{
    public float playerSpeed;
    public FixedJoystick joystick;

    private Rigidbody2D rb;
    private Vector2 playerDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float directionY = joystick.Vertical;
        playerDirection = new Vector2(0, directionY).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, playerDirection.y * playerSpeed);
    }
}
