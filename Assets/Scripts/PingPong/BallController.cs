using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Ball Settings")]
    public float initialSpeed = 8f;
    private Rigidbody2D rb;

    void Awake() // Fixed the race condition!
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void LaunchBall()
    {
        transform.position = Vector3.zero;
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector2 direction = new Vector2(x, y).normalized;
        rb.linearVelocity = direction * initialSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PingPongManager manager = FindObjectOfType<PingPongManager>();
        if (manager != null)
        {
            if (collision.gameObject.CompareTag("TopGoal")) manager.Player1Scored();
            else if (collision.gameObject.CompareTag("BottomGoal")) manager.Player2Scored();
        }
    }
}