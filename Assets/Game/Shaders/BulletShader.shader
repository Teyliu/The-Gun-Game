Shader "Custom/GlassyNeonBulletShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _SecondaryTex("Secondary Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        _Speed("Speed", Range(1, 10)) = 5
        _Intensity("Intensity", Range(0, 1)) = 0.5
        _Shininess("Shininess", Range(0, 1)) = 0.5
        _LoopOffset("Loop Offset", Range(-1, 1)) = 0
        _Glassiness("Glassiness", Range(0, 1)) = 0.5
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 200

            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha
                ZWrite Off

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_instancing

                #include "UnityCG.cginc"

                sampler2D _MainTex;
                sampler2D _SecondaryTex;
                float4 _Color;
                float _Speed;
                float _Intensity;
                float _Shininess;
                float _LoopOffset;
                float _Glassiness;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
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
                    UNITY_SETUP_INSTANCE_ID(v);

                    // Loop the texture on the X-axis
                    float2 loopOffset = float2(_LoopOffset * _Time.y * _Speed, 0);
                    float2 offset = v.texcoord * _Speed + loopOffset;
                    float2 distortedUV = v.texcoord + sin(offset) * _Intensity;

                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = distortedUV;
                    UNITY_TRANSFER_INSTANCE_ID(o, v);
                    return o;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    fixed4 texColor = tex2D(_MainTex, IN.texcoord);
                    fixed4 secondaryTexColor = tex2D(_SecondaryTex, IN.texcoord);

                    // Calculate the brightness of the color
                    float brightness = texColor.r + texColor.g + texColor.b;

                    // Make the black color nearly transparent
                    if (brightness < 0.1) {
                        texColor.a = 0.05;
                    }

                    // Apply shininess
                    fixed4 finalColorWithAlpha = fixed4(texColor.rgb * _Color.rgb, texColor.a);
                    finalColorWithAlpha.a *= pow(finalColorWithAlpha.a, _Shininess);

                    // Mix the colors based on glassiness
                    fixed4 mixedColor = lerp(finalColorWithAlpha, secondaryTexColor, _Glassiness);

                    return mixedColor;
                }

                ENDCG
            }
        }
}
