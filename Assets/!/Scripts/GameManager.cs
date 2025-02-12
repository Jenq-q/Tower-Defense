using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    [Header("Game Settings")]
    [SerializeField] private int startingLives = 20;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text scoreText;

    public UnityEvent<int> onLivesChanged = new UnityEvent<int>();
    public UnityEvent<int> onScoreChanged = new UnityEvent<int>();
    public UnityEvent onGameOver = new UnityEvent();

    private int score = 0;
    private int lives;

    public int Lives => lives;
    public int Score => score;

    private void Start()
    {
        lives = startingLives;
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        onScoreChanged.Invoke(score);
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        onLivesChanged.Invoke(lives);
        UpdateUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateUI()
    {
        if (livesText != null)
            livesText.text = $"Lives: {lives}";
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    private void GameOver()
    {
        onGameOver.Invoke();
        Debug.Log($"Game Over! Final Score: {score}");
    }
}