using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public new ParticleSystem particleSystem;
    private PlayerScript playerScript;

    private void Start()
    {
        playerScript = PlayerScript.Instance;
    }

    private void Update()
    {
        if (playerScript.IsShot)
        {
            PlayParticleSystem();
        }
    }

    public void PlayParticleSystem()
    {
        if (particleSystem != null)
        {
            particleSystem.transform.position = transform.position;
            particleSystem.Play();
        }
    }
}
