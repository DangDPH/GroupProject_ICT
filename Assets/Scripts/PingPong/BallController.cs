using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Ball Settings")]
    public float initialSpeed = 8f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // The PingPongManager will call LaunchBall() when the player clicks a Game Mode button.
    }

    public void LaunchBall()
    {
        // 1. Reset position to the center
        transform.position = Vector3.zero;

        // 2. Pick a random direction (either -1 or 1 for both X and Y)
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        // 3. Normalize the direction so it always moves at the exact same speed diagonally
        Vector2 direction = new Vector2(x, y).normalized;

        // 4. Push the ball! 
        // Note: Unity 6 uses linearVelocity. If it gives an error in older versions, change to rb.velocity
        rb.linearVelocity = direction * initialSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Find the PingPongManager in the scene
        PingPongManager gm = FindObjectOfType<PingPongManager>();

        // Make sure we actually found the PingPongManager
        if (gm != null)
        {
            if (collision.gameObject.CompareTag("TopGoal"))
            {
                // Ball hit the top wall, so Player 1 (Bottom) scored!
                gm.Player1Scored();
            }
            else if (collision.gameObject.CompareTag("BottomGoal"))
            {
                // Ball hit the bottom wall, so Player 2/Bot (Top) scored!
                gm.Player2Scored();
            }
        }
    }
}