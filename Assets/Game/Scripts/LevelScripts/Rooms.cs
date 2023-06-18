using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Rooms : MonoBehaviour
    {
    public GameObject objetoBloqueo1;
    public GameObject objetoBloqueo2;
    public Collider2D areaEnemigos; // Collider2D del área de los enemigos

    private bool bloqueadoresActivados;
    [SerializeField] private List<GameObject> enemigosEnArea = new List<GameObject>();

    private void Start()
    {
        DesactivarBloqueadores();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!bloqueadoresActivados)
            {
                // Activar los bloqueadores al entrar al área
                ActivarBloqueadores();
            }
        }
        else if (other.CompareTag("Enemy") && areaEnemigos.IsTouching(other))
        {
            // Agregar enemigo a la lista solo si no existe ya
            if (!enemigosEnArea.Contains(other.gameObject))
            {
                enemigosEnArea.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && areaEnemigos.IsTouching(other))
        {
            // Marcar enemigo como muerto (en lugar de eliminarlo directamente)
            other.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Limpiar la lista de enemigos muertos
        enemigosEnArea.RemoveAll(enemigo => enemigo == null || !enemigo.activeSelf);

        // Verificar si no hay más enemigos vivos en el área
        if (enemigosEnArea.Count <= 0 && bloqueadoresActivados)
        {
            // Desactivar los bloqueadores si no hay más enemigos en el área
            DesactivarBloqueadores();
        }
    }

    private void ActivarBloqueadores()
    {
        objetoBloqueo1.SetActive(true);
        objetoBloqueo2.SetActive(true);
        bloqueadoresActivados = true;
    }

    private void DesactivarBloqueadores()
    {
        objetoBloqueo1.SetActive(false);
        objetoBloqueo2.SetActive(false);
        bloqueadoresActivados = false;
    }
}

