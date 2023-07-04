using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomRenderPipeline : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier Source { get; set; }
        public Material DarknessMaterial { get; set; }
        public Material LightMaterial { get; set; }
        public float DarknessIntensity { get; set; }
        public float LightIntensity { get; set; }

        private RenderTargetHandle destination;

        public CustomRenderPass()
        {
            destination.Init("_CustomRenderPassTexture");
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(destination.id, cameraTextureDescriptor);
            ConfigureTarget(destination.Identifier());
            ConfigureClear(ClearFlag.All, Color.clear);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (DarknessMaterial == null || LightMaterial == null || Source == default)
                return;

            CommandBuffer cmd = CommandBufferPool.Get("Custom Render Pass");

            // Render the scene with the darkness material
            cmd.Blit(Source, destination.Identifier(), DarknessMaterial);

            // Set the light intensity property in the light material
            LightMaterial.SetFloat("_Intensity", LightIntensity);

            // Render the objects with the light material
            Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                Material[] materials = renderer.sharedMaterials;
                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i].shader == LightMaterial.shader)
                    {
                        // Set the darkness intensity property in the light material
                        materials[i].SetFloat("_DarknessIntensity", DarknessIntensity);
                    }
                }

                cmd.DrawRenderer(renderer, LightMaterial);
            }

            // Blit the result to the source
            cmd.Blit(destination.Identifier(), Source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }


        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(destination.id);
        }
    }

    CustomRenderPass customRenderPass;

    public Material darknessMaterial;
    public Material lightMaterial;
    public float darknessIntensity;
    public float lightIntensity;

    public override void Create()
    {
        customRenderPass = new CustomRenderPass
        {
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents,
            DarknessMaterial = darknessMaterial,
            LightMaterial = lightMaterial
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        customRenderPass.Source = renderer.cameraColorTarget;
        customRenderPass.DarknessIntensity = darknessIntensity;
        customRenderPass.LightIntensity = lightIntensity;

        renderer.EnqueuePass(customRenderPass);
    }
}
