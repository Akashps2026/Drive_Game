using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GroundPrefab;
    public float SpawnDistance = 120f;
    public Transform PlayerTransform;
    public GameObject gameOverPanel;
    public Light Sun;
    public float RotateSpeed;
    private Vector3 nextSpawnPosition;

    void Start()
    {
        nextSpawnPosition = Vector3.zero;
        SpawnGround();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0f; // Pause the game

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // Unpause before reloading the scene
        Time.timeScale = 1f;

        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (PlayerTransform.position.z + SpawnDistance > nextSpawnPosition.z)
            SpawnGround();
        Sun.transform.Rotate(Vector3.right*Time.deltaTime);
    }

    void SpawnGround()
    {
        Instantiate(GroundPrefab, nextSpawnPosition, Quaternion.identity);
        nextSpawnPosition.z += 120f;
    }
}
