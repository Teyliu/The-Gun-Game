Shader "Custom/CoolNeonLinesShader"
{
    Properties
    {
        _BackgroundTex("Background Texture", 2D) = "white" {}
        _LineTexUp("Line Texture Up", 2D) = "white" {}
        _LineTexDown("Line Texture Down", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        _Intensity("Intensity", Range(0, 2)) = 1.0
        _LineWidth("Line Width", Range(0, 1)) = 0.1
        _Brightness("Brightness", Range(0, 10)) = 2.0
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
        _Speed("Movement Speed", Range(-10, 10)) = 1.0
        _Transparency("Transparency", Range(0, 1)) = 0.5
        _Voids("Void Intensity", Range(0, 1)) = 0.0
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 100

            Cull Off
            ZWrite Off
            ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 3.0
                #pragma multi_compile_instancing

                #include "UnityCG.cginc"

                sampler2D _BackgroundTex;
                sampler2D _LineTexUp;
                sampler2D _LineTexDown;
                float4 _Color;
                float _Intensity;
                float _LineWidth;
                float _Brightness;
                float _Smoothness;
                float _Speed;
                float _Transparency;
                float _Voids;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float2 texcoord : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.texcoord = v.texcoord;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Sample the background texture
                    fixed4 backgroundColor = tex2D(_BackgroundTex, i.texcoord);

                // Sample the line textures
                float2 shiftedTexCoordUp = i.texcoord + float2(0, _Time.y * _Speed);
                float2 shiftedTexCoordDown = i.texcoord + float2(0, -_Time.y * _Speed);
                fixed4 lineColorUp = tex2D(_LineTexUp, shiftedTexCoordUp);
                fixed4 lineColorDown = tex2D(_LineTexDown, shiftedTexCoordDown);

                // Calculate the distance from the center of the line
                float distance = abs(i.texcoord.y - 0.5);

                // Apply line width
                float lineFactor = smoothstep(0.5 - _LineWidth * 0.5, 0.5 + _LineWidth * 0.5, distance);

                // Apply neon effect and additional intensity
                fixed4 neonColor = fixed4(_Color.rgb * _Intensity * _Brightness, 1.0);

                // Apply smoothness to the line colors' alpha
                lineColorUp.a = pow(lineColorUp.a, _Smoothness);
                lineColorDown.a = pow(lineColorDown.a, _Smoothness);

                // Combine line colors with neon effect and apply transparency
                fixed4 finalColorUp = neonColor * lineColorUp * lineFactor * (1 - _Transparency);
                fixed4 finalColorDown = neonColor * lineColorDown * lineFactor * (1 - _Transparency);

                // Combine final colors with the background
                fixed4 finalColor = lerp(backgroundColor, finalColorUp, finalColorUp.a);
                finalColor = lerp(finalColor, finalColorDown, finalColorDown.a);

                // Apply voids (make blacks transparent)
                float voidFactor = smoothstep(0, _Voids, finalColor.r + finalColor.g + finalColor.b);
                finalColor.a *= voidFactor;

                return finalColor;
            }
            ENDCG
        }
        }
}


