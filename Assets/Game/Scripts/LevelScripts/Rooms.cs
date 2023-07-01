using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rooms : MonoBehaviour
{
    public GameObject objetoBloqueo1;
    public GameObject objetoBloqueo2;
    public Collider2D areaEnemigos; // Collider2D del área de los enemigos
    public GameObject goArrow;
    public float goArrowDuration = 3f; 


    private bool bloqueadoresActivados;
    [SerializeField] private List<GameObject> enemigosEnArea = new();

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

        enemigosEnArea.RemoveAll(enemigo => enemigo == null || !enemigo.activeSelf);


        if (enemigosEnArea.Count <= 0 && bloqueadoresActivados)
        {
            DesactivarBloqueadores();
            GoArrow();
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

    private void GoArrow()
    {
        if (goArrow != null)
        {
            goArrow.SetActive(true);
            StartCoroutine(DeactivateGoArrowAfterDelay(goArrowDuration));
        }
    }

    private IEnumerator DeactivateGoArrowAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        goArrow.SetActive(false);
    }
}