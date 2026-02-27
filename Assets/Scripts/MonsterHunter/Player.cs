using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterHunter_Player : MonoBehaviour
{   
    public FloatingJoystick joystick;
    public float moveSpeed;

    public GameObject gameOverPanel;

    public Vector2 movementDirection{ get; private set; }

    float hInput, vInput;

    private int score = 0;
    public int Score {get {return score;}}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
