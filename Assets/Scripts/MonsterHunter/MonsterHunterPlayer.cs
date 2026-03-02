using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterHunterPlayer : MonoBehaviour
{   
    public FloatingJoystick joystick;
    public float moveSpeed;

    public GameObject gameOverPanel;

    SpriteRenderer spriteRenderer;

    public Vector2 movementDirection{ get; private set; }

    float hInput, vInput;

    private int score = 0;
    public int Score {get {return score;}}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 rawInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        movementDirection = rawInput.normalized; 

        transform.Translate(
            movementDirection.x * moveSpeed * Time.fixedDeltaTime,
            movementDirection.y * moveSpeed * Time.fixedDeltaTime,
            0
        );

        if (joystick.Horizontal > 0)
    {
        spriteRenderer.flipX = false; // Turn right
    }
    else if (joystick.Horizontal < 0)
    {
        spriteRenderer.flipX = true; // Turn left
    }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }
    }

    public void MonsterHit(GameObject monster)
    {
        score += 1;
        Destroy(monster);
        Debug.Log("SCORE: " + score);
    }
}
