using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDesk : MonoBehaviour
{
    [SerializeField] List<GameObject> pociones;
    [SerializeField] [Range(0f, 1f)] float chance = 1f;

    private void Start()
    {

    }

    public void DropPocion()
    {
        if (Random.value < chance)
        {
            GameObject pocionSeleccionada = pociones[Random.Range(0, pociones.Count)];
            Transform t = Instantiate(pocionSeleccionada).transform;
            t.position = transform.position;
        }
    }
}
