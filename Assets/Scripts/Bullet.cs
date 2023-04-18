using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * velocity);
    }
}
