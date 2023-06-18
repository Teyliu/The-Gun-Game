Shader "Custom/LightEdgesShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        _EdgeSmoothness("Edge Smoothness", Range(0, 1)) = 0.5
        _EdgeTransparency("Edge Transparency", Range(0, 1)) = 0.1
        _LightColor("Light Color", Color) = (1, 1, 1, 1)
        _LightIntensity("Light Intensity", Range(0, 5)) = 1.0 // Increased intensity range
        _SpecularColor("Specular Color", Color) = (1, 1, 1, 1)
        _SpecularIntensity("Specular Intensity", Range(0, 5)) = 1.0 // Increased intensity range
        _Shininess("Shininess", Range(0, 1)) = 0.5
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 100

            Cull Off
            ZWrite Off
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 3.0
                #pragma multi_compile_instancing

                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float4 _Color;
                float _EdgeSmoothness;
                float _EdgeTransparency;
                float4 _LightColor;
                float _LightIntensity;
                float4 _SpecularColor;
                float _SpecularIntensity;
                float _Shininess;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float2 texcoord : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float4 worldPos : TEXCOORD1;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.texcoord = v.texcoord;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Sample the main texture
                    fixed4 texColor = tex2D(_MainTex, i.texcoord);

                // Calculate the normal from the world position
                float3 normal = normalize(i.worldPos.xyz - _WorldSpaceCameraPos.xyz);

                // Calculate the edge factor based on the dot product of the normal and the view direction
                float edgeFactor = smoothstep(_EdgeSmoothness - 0.02, _EdgeSmoothness + 0.02, dot(normal, normalize(i.vertex.xyz)));

                // Calculate the clipping factor based on the object's alpha channel
                float clipping = texColor.a;

                // Apply edge smoothness and transparency
                fixed4 finalColor = texColor * (1 - edgeFactor * _EdgeTransparency);

                // Apply light and specular reflections with increased intensity
                float4 lighting = _LightColor * _LightIntensity * max(0, dot(normal, _WorldSpaceLightPos0.xyz));
                float4 specular = _SpecularColor * _SpecularIntensity * pow(max(0, dot(reflect(-_WorldSpaceLightPos0.xyz, normal), normalize(i.worldPos.xyz))), _Shininess);
                finalColor.rgb = finalColor.rgb * (lighting.rgb + specular.rgb);

                // Apply overall color
                finalColor.rgb *= _Color.rgb;

                // Apply clipping factor
                finalColor.a *= clipping;

                return finalColor;
            }
            ENDCG
        }
        }
}