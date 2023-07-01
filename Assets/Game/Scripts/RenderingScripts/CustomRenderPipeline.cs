using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomRenderPipeline : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier Source { get; set; }
        public Material Material { get; set; }
        public float Intensity { get; set; }

        private RenderTargetHandle destination;

        public CustomRenderPass()
        {
            destination.Init("_CustomRenderPassTexture");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (Material == null || Source == default)
                return;

            CommandBuffer cmd = CommandBufferPool.Get("Custom Render Pass");
            RenderTextureDescriptor cameraTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            cmd.GetTemporaryRT(destination.id, cameraTextureDescriptor);

            Blit(cmd, Source, destination.Identifier(), Material);

            // Apply intensity
            Material.SetFloat("_Intensity", Intensity);

            Blit(cmd, destination.Identifier(), Source, Material);
            cmd.ReleaseTemporaryRT(destination.id);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    CustomRenderPass customRenderPass;

    public Material lightMaterial;
    public float lightIntensity;

    public override void Create()
    {
        customRenderPass = new CustomRenderPass
        {
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        customRenderPass.Source = renderer.cameraColorTarget;
        customRenderPass.Material = lightMaterial;
        customRenderPass.Intensity = lightIntensity;

        renderer.EnqueuePass(customRenderPass);
    }
}
