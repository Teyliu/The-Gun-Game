using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{

    private bool Interaccion = false;
    private bool InteraccionF = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Interaccion && !InteraccionF && Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Interaccion = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Interaccion = false;
        }
    }
    private void Interactuar()
    {
        Debug.Log("Toma 10 balas");
        InteraccionF = true;
    }


}

