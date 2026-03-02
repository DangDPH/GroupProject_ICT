using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public MonsterHunterPlayer player;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        if (player == null)
            player = FindFirstObjectByType<MonsterHunterPlayer>();
    }

    void Update()
    {
        if (player != null)
            scoreText.text = "SCORE : " + player.Score;
    }
}
