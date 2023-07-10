using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private AudioClip gameOverSound;

    private PlayerHealth playerHealth;
    private MovementScript playerMovement;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMovement = FindObjectOfType<MovementScript>();

        if (playerHealth != null && playerHealth.IsDead)
        {
            GameOverSequence();
        }
    }

    private void  FixedUpdate()
    {
        if (playerHealth != null && playerHealth.IsDead)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        // Play game over sound effect   
        Debug.Log("AAAAA");     

        // Disable player movement
        playerMovement.enabled = false;

        // Display the game over panel
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
