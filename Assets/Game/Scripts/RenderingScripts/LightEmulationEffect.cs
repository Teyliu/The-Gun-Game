using UnityEngine;

public class LightEmulationEffect : MonoBehaviour
{
    public new Renderer renderer;
    public float pingPongSpeed = 1f;
    public float pingPongRange = 1f;
    private float originalIntensity;

    private void Start()
    {
        if (renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }

        // Store the original intensity value
        originalIntensity = renderer.material.GetFloat("_DarknessIntensity");
    }

    private void Update()
    {
        // Calculate the ping pong value
        float pingPongValue = Mathf.PingPong(Time.time * pingPongSpeed, pingPongRange);

        // Update the material's intensity with the ping pong value
        renderer.material.SetFloat("_DarknessIntensity", pingPongValue);
    }

    private void OnDestroy()
    {
        // Reset the material's intensity to its original value when leaving the scene
        renderer.material.SetFloat("_DarknessIntensity", originalIntensity);
    }
}
