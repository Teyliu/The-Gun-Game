using UnityEngine;

public class LetterObject : MonoBehaviour
{
    public Sprite openedSprite;
    public Canvas letterCanvas;
    public float interactDistance = 0.5f;
    public KeyCode interactKey = KeyCode.E;

    private bool isInteracted = false;
    private SpriteRenderer spriteRenderer;
    private GameObject player;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Hide the letter canvas initially
        letterCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Check if the player is within interactDistance
        float distance = Vector2.Distance(transform.position, player.transform.position);
        bool isInRange = distance <= interactDistance;

        if (!isInteracted && isInRange && Input.GetKeyDown(interactKey))
        {
            // Change the sprite to the opened sprite
            if (openedSprite != null)
            {
                spriteRenderer.sprite = openedSprite;
            }

            // Activate the letter canvas
            letterCanvas.gameObject.SetActive(true);

            // Mark the letter as interacted
            isInteracted = true;
        }
        else if (isInteracted && !isInRange)
        {
            // Deactivate the letter canvas when the player is outside the interactDistance
            letterCanvas.gameObject.SetActive(false);
        }
    }
}
