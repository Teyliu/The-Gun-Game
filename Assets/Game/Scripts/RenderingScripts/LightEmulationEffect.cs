using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightEmulationEffect : MonoBehaviour
{
    public Material lightMaterial;
    public float lightIntensity;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (lightMaterial != null)
        {
            lightMaterial.SetFloat("_Intensity", lightIntensity);
            Graphics.Blit(source, destination, lightMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
