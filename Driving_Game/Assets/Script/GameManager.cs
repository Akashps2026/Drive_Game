using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    [Header("Ground Settings")]
    public GameObject GroundPrefab;
    public float SpawnDistance = 120f;
    public Transform PlayerTransform;
    private Vector3 nextSpawnPosition;

    [Header("UI Elements")]
    public GameObject gameOverPanel;
    public Text scoreText;       
    public Text highScoreText;   

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

       
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    void Update()
    {
       
        Sun.transform.Rotate(Vector3.right * RotateSpeed * Time.deltaTime);

       
        if (PlayerTransform.position.z + SpawnDistance > nextSpawnPosition.z)
            SpawnGround();

        
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
        Time.timeScale = 0f; 

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

       
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
