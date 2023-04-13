using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript: MonoBehaviour
{
    public static PlayerScript instance { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


}