using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDesk : MonoBehaviour
{
    public Animator animator;
    public GameObject[] itemPrefabs;
    private GameObject[] displayItems;
    public float displayDuration = 2f;
    public float dropDistance = 1f; 
    public float dropAngle = 45f; 
    public AudioClip openSoundClip;
    private bool playerInRange;
    public bool isOpened;

    private void Awake()
    {
        displayItems = new GameObject[itemPrefabs.Length];
        for (int i = 0; i < itemPrefabs.Length; i++)
        {
            GameObject displayItem = new("DisplayItem");
            displayItem.transform.SetParent(transform);
            displayItem.transform.localPosition = Vector3.zero;
            displayItem.SetActive(false);

            SpriteRenderer itemRenderer = displayItem.AddComponent<SpriteRenderer>();
            itemRenderer.sprite = itemPrefabs[i].GetComponent<SpriteRenderer>().sprite;

            displayItems[i] = displayItem;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerEnteredRange();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerExitedRange();
        }
    }

    private void PlayerEnteredRange()
    {
        playerInRange = true;
    }

    private void PlayerExitedRange()
    {
        playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            OpenDeskInteraction();
        }
    }

    private void OpenDeskInteraction()
    {
        isOpened = true;
        animator.SetTrigger("Open");
        PlayOpenSound();
        StartCoroutine(DisplayItemsAndAcquire());
    }

    private IEnumerator DisplayItemsAndAcquire()
    {
        foreach (GameObject displayItem in displayItems)
        {
            displayItem.SetActive(true);
        }

        yield return new WaitForSeconds(displayDuration);

        foreach (GameObject displayItem in displayItems)
        {
            displayItem.SetActive(false);
        }

        Vector2 dropDirection = Quaternion.Euler(0f, 0f, dropAngle) * Vector2.up; // Direction in which items will be dropped
        foreach (GameObject itemPrefab in itemPrefabs)
        {
            if (itemPrefab != null)
            {
                GameObject instantiatedItem = Instantiate(itemPrefab);
                instantiatedItem.SetActive(true);

                Vector2 dropOffset = dropDirection * dropDistance;
                instantiatedItem.transform.position = transform.position + (Vector3)dropOffset;
            }
        }
    }

    private void PlayOpenSound()
    {
        if (openSoundClip != null)
        {
            AudioSource.PlayClipAtPoint(openSoundClip, transform.position);
        }
    }
}








