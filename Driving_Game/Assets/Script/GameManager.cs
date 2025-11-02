using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // ✅ Needed for Text UI

public class GameManager : MonoBehaviour
{
    [Header("Ground Settings")]
    public GameObject GroundPrefab;
    public float SpawnDistance = 120f;
    public Transform PlayerTransform;
    private Vector3 nextSpawnPosition;

    [Header("UI Elements")]
    public GameObject gameOverPanel;
    public Text scoreText;       // 🟢 Assign in Inspector
    public Text highScoreText;   // 🟢 Assign in Inspector

    [Header("Sun Rotation")]
    public Light Sun;
    public float RotateSpeed = 10f;

    [Header("Score System")]
    public float score = 0f;
    private float highScore = 0f;

    void Start()
    {
        nextSpawnPosition = Vector3.zero;
        SpawnGround();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Load high score from saved data
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    void Update()
    {
        // ☀️ Rotate Sun
        Sun.transform.Rotate(Vector3.right * RotateSpeed * Time.deltaTime);

        // 🛣️ Spawn Ground as player moves
        if (PlayerTransform.position.z + SpawnDistance > nextSpawnPosition.z)
            SpawnGround();

        // 🧮 Update Score (based on player’s z position)
        score = PlayerTransform.position.z;
        UpdateScoreUI();
    }

    void SpawnGround()
    {
        Instantiate(GroundPrefab, nextSpawnPosition, Quaternion.identity);
        nextSpawnPosition.z += 120f;
    }

    public void GameOver()
    {
        Time.timeScale = 0f; // Pause game

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // 🏆 Save high score if beaten
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateScoreUI();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();

        if (highScoreText != null)
            highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore).ToString();
    }
}
