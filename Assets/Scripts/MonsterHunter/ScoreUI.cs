using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public MonsterHunter_Player player;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        if (player == null)
            player = FindFirstObjectByType<MonsterHunter_Player>();
    }

    void Update()
    {
        if (player != null)
            scoreText.text = "SCORE : " + player.Score;
    }
}
