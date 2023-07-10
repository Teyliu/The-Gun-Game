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

    private void Update()
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


        // Optionally, you can pause the game or perform other actions

        // Handle game over logic (e.g., save high scores, etc.)

        // Restart the game or perform other actions
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
