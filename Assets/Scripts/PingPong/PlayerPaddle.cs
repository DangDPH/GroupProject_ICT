using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    [Header("Game Mode Settings")]
    public bool isAI = false;
    public bool isTopPaddle = false;

    [Header("AI Settings")]
    public float aiSpeed = 3f;
    public Transform ball; // <--- THIS MAKES THE BOT ABLE TO SEE!

    [Header("Paddle Boundaries")]
    public float minX = -2.2f;
    public float maxX = 2.2f;

    private float screenHalfHeight;

    void Start()
    {
        screenHalfHeight = Screen.height / 2f;
        // We removed the broken GameObject.Find line completely!
    }

    void Update()
    {
        if (isAI) MoveAI();
        else MovePlayer();
    }

    private void MoveAI()
    {
        if (ball != null)
        {
            float targetX = Mathf.MoveTowards(transform.position.x, ball.position.x, aiSpeed * Time.deltaTime);
            float clampedX = Mathf.Clamp(targetX, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }

    private void MovePlayer()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if ((isTopPaddle && touch.position.y > screenHalfHeight) ||
                    (!isTopPaddle && touch.position.y < screenHalfHeight))
                {
                    MoveToPosition(touch.position);
                }
            }
        }
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
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}