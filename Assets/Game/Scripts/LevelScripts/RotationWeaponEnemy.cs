using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWeaponEnemy : MonoBehaviour
{
    public Transform puntoRotacion;
    public string jugadorTag = "Player";

    private Transform jugador;

    void Start()
    {
        // Buscar el objeto del jugador por su tag
        GameObject jugadorObject = GameObject.FindWithTag(jugadorTag);
        if (jugadorObject != null)
        {
            jugador = jugadorObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontr� ning�n objeto con la etiqueta " + jugadorTag);
        }
    }

    void Update()
    {
        // Verificar si el objeto del jugador ha sido encontrado
        if (jugador != null)
        {
            // Obtener la direcci�n hacia el jugador desde el punto de rotaci�n
            Vector3 direccion = jugador.position - puntoRotacion.position;

            direccion.z = 0f;

            // Obtener el �ngulo hacia el jugador en radianes
            float anguloRad = Mathf.Atan2(direccion.y, direccion.x);

            // Convertir el �ngulo a grados
            float anguloDeg = anguloRad * Mathf.Rad2Deg;

            // Aplicar la rotaci�n al arma en relaci�n al punto de rotaci�n
            transform.rotation = Quaternion.Euler(0f, 0f, anguloDeg);

            if (jugador.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (jugador.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1f, -1f, 1f);
            }
        }
    }

}