using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWeaponEnemy : MonoBehaviour
{
    public Transform jugador;

    void Update()
    {
        // Obtener la dirección hacia el jugador
        Vector3 direccion = jugador.position - transform.position;

        // Obtener el ángulo hacia el jugador en radianes
        float anguloRad = Mathf.Atan2(direccion.y, direccion.x);

        // Convertir el ángulo a grados
        float anguloDeg = anguloRad * Mathf.Rad2Deg;

        // Rotar el objeto hacia el jugador
        transform.rotation = Quaternion.Euler(0f, 0f, anguloDeg);
    }


}