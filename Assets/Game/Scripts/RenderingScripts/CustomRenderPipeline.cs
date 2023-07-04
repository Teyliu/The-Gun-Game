using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomRenderPipeline : ScriptableRendererFeature
{
    class DarknessRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier Source { get; set; }
        public Material DarknessMaterial { get; set; }

        private RenderTargetHandle destination;

        public DarknessRenderPass()
        {
            destination.Init("_DarknessRenderPassTexture");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (DarknessMaterial == null || Source == default)
                return;

            CommandBuffer cmd = CommandBufferPool.Get("Darkness Render Pass");
            RenderTextureDescriptor cameraTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            cmd.GetTemporaryRT(destination.id, cameraTextureDescriptor);

            Blit(cmd, Source, destination.Identifier(), DarknessMaterial);

            Blit(cmd, destination.Identifier(), Source);

            cmd.ReleaseTemporaryRT(destination.id);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    class LightRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier Source { get; set; }
        public Material LightMaterial { get; set; }

        private RenderTargetHandle destination;

        public LightRenderPass()
        {
            destination.Init("_LightRenderPassTexture");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (LightMaterial == null || Source == default)
                return;

            CommandBuffer cmd = CommandBufferPool.Get("Light Render Pass");
            RenderTextureDescriptor cameraTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            cmd.GetTemporaryRT(destination.id, cameraTextureDescriptor);

            Blit(cmd, Source, destination.Identifier());

            cmd.SetGlobalTexture("_DarknessTexture", destination.Identifier());
            Blit(cmd, destination.Identifier(), Source, LightMaterial);

            cmd.ReleaseTemporaryRT(destination.id);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    DarknessRenderPass darknessRenderPass;
    LightRenderPass lightRenderPass;

    public Material darknessMaterial;
    public Material lightMaterial;

    public override void Create()
    {
        darknessRenderPass = new DarknessRenderPass
        {
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents,
            DarknessMaterial = darknessMaterial
        };

        lightRenderPass = new LightRenderPass
        {
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents,
            LightMaterial = lightMaterial
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        darknessRenderPass.Source = renderer.cameraColorTarget;
        lightRenderPass.Source = renderer.cameraColorTarget;

        renderer.EnqueuePass(darknessRenderPass);
        renderer.EnqueuePass(lightRenderPass);
    }
}
