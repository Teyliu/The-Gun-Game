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
            Debug.LogWarning("No se encontró ningún objeto con la etiqueta " + jugadorTag);
        }
    }

    void Update()
    {
        // Verificar si el objeto del jugador ha sido encontrado
        if (jugador != null)
        {
            // Obtener la dirección hacia el jugador desde el punto de rotación
            Vector3 direccion = jugador.position - puntoRotacion.position;

            direccion.z = 0f;

            // Obtener el ángulo hacia el jugador en radianes
            float anguloRad = Mathf.Atan2(direccion.y, direccion.x);

            // Convertir el ángulo a grados
            float anguloDeg = anguloRad * Mathf.Rad2Deg;

            // Aplicar la rotación al arma en relación al punto de rotación
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