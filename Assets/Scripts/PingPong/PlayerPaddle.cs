using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    [Header("Game Mode Settings")]
    public bool isAI = false;        // Check this if this paddle is the Bot!
    public bool isTopPaddle = false; // Check this if this is the top paddle (Player 2 or Bot)

    [Header("AI Settings")]
    public float aiSpeed = 3f;       // How fast the Bot moves
    private Transform ball;

    [Header("Paddle Boundaries")]
    public float minX = -2.2f;
    public float maxX = 2.2f;

    private float screenHalfHeight;

    void Start()
    {
        // Find the middle of the screen
        screenHalfHeight = Screen.height / 2f;

        // Find the ball automatically so the AI can track it
        GameObject ballObj = GameObject.Find("Ball");
        if (ballObj != null)
        {
            ball = ballObj.transform;
        }
    }

    void Update()
    {
        // Decide how this paddle should move every frame
        if (isAI)
        {
            MoveAI();
        }
        else
        {
            MovePlayer();
        }
    }

    private void MoveAI()
    {
        if (ball != null)
        {
            // Smoothly slide toward the ball's X position
            float targetX = Mathf.MoveTowards(transform.position.x, ball.position.x, aiSpeed * Time.deltaTime);
            float clampedX = Mathf.Clamp(targetX, minX, maxX);

            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }

    private void MovePlayer()
    {
        // 1. Mobile Touch Input
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                // Check if the touch is on the correct half of the screen for this specific paddle
                if ((isTopPaddle && touch.position.y > screenHalfHeight) ||
                    (!isTopPaddle && touch.position.y < screenHalfHeight))
                {
                    MoveToPosition(touch.position);
                }
            }
        }
        // 2. Mouse Input (Editor Testing)
        else if (Input.GetMouseButton(0))
        {
            if ((isTopPaddle && Input.mousePosition.y > screenHalfHeight) ||
                (!isTopPaddle && Input.mousePosition.y < screenHalfHeight))
            {
                MoveToPosition(Input.mousePosition);
            }
        }
    }

    private void MoveToPosition(Vector2 screenPos)
    {
        // Convert screen pixels to world space and clamp to boundaries
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);

        // Move paddle while maintaining the Safe Margin (original Y position)
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}