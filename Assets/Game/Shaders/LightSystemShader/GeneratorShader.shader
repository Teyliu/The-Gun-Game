Shader "Custom/LightEmulationShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _Intensity("Intensity", Range(0, 1)) = 0.5
    }

        SubShader
        {
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _Intensity;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Apply the main texture
                    fixed4 texColor = tex2D(_MainTex, i.uv);

                // Apply light emulation effect based on intensity
                texColor.rgb += _Intensity * 0.2; // Increase brightness
                texColor.rgb = saturate(texColor.rgb); // Clamp values to [0, 1]

                return texColor;
            }
            ENDCG
        }
        }
}
