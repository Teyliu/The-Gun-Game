using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomRenderPipeline : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier Source { get; set; }
        public Material DarknessMaterial { get; set; }
        public float DarknessIntensity { get; set; }

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
            if (DarknessMaterial == null || Source == default)
                return;

            CommandBuffer cmd = CommandBufferPool.Get("Custom Render Pass");

            // Render the scene with the darkness material
            cmd.Blit(Source, destination.Identifier(), DarknessMaterial);

            // Set the darkness intensity property in the darkness material
            DarknessMaterial.SetFloat("_Intensity", DarknessIntensity);

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
    public float darknessIntensity;

    public override void Create()
    {
        customRenderPass = new CustomRenderPass
        {
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents,
            DarknessMaterial = darknessMaterial
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        customRenderPass.Source = renderer.cameraColorTarget;
        customRenderPass.DarknessIntensity = darknessIntensity;

        renderer.EnqueuePass(customRenderPass);
    }
}
