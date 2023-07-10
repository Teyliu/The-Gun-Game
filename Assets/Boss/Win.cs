using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadCreditsScene();
        }
    }

    private void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
