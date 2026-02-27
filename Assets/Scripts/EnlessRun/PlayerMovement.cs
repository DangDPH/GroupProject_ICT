using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 5f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        rb.linearVelocity = new Vector2(x, y) * speed;
    }
}
