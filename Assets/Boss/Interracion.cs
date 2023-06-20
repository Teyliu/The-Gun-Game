using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interracion : MonoBehaviour
{
    public bool Interaccion = false;
    public bool InteraccionF = false;
    public List<GameObject> objetosDro; // Objetos que se dropearán al interactuar

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

        // Dropear uno de los objetos al interactuar con un 50% de probabilidad para cada uno
        if (objetosDro != null && objetosDro.Count > 0)
        {
            int random = Random.Range(0, objetosDro.Count);
            GameObject objetoDropeado = objetosDro[random];
            Instantiate(objetoDropeado, transform.position, Quaternion.identity);
        }
    }
}
