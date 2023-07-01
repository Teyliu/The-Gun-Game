Shader "Custom/LightShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _LightColor("Light Color", Color) = (1, 1, 1, 1)
        _LightIntensity("Light Intensity", Range(0, 1)) = 0.5
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" }
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }
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
                float4 _LightColor;
                float _LightIntensity;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 texColor = tex2D(_MainTex, i.uv);
                    texColor.rgb *= _LightColor.rgb * _LightIntensity; // Apply light color and intensity
                    return texColor;
                }

                ENDCG
            }
        }
}
