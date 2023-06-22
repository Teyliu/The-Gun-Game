Shader "Custom/SoftLightTransparent"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _LightColor("Light Color", Color) = (1, 1, 1, 1)
        _LightIntensity("Light Intensity", Range(0, 1)) = 1
        _Radius("Radius", Range(0, 1)) = 0.5
        _Transparency("Transparency", Range(0, 1)) = 0.5
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

            Blend SrcAlpha OneMinusSrcAlpha

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
                float4 _MainTex_ST;
                float4 _LightColor;
                float _LightIntensity;
                float _Radius;
                float _Transparency;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 texColor = tex2D(_MainTex, i.uv);
                    float2 center = float2(0.5, 0.5);
                    float2 offset = i.uv - center;
                    float distance = length(offset);
                    float transparency = 1 - smoothstep(_Radius, _Radius + _Transparency, distance);

                    fixed4 lightColor = _LightColor * _LightIntensity;
                    fixed4 finalColor = texColor + (lightColor - texColor) * transparency;
                    finalColor.a = texColor.a; // Preserve the original alpha value

                    return finalColor;
                }
                ENDCG
            }
        }
}
