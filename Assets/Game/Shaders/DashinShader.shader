Shader "Custom/DashEffectShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        _Speed("Speed", Range(1, 10)) = 5
        _Intensity("Intensity", Range(0, 1)) = 0.5
        _HueSpeed("Hue Speed", Range(0, 10)) = 1
        _PingPongHue("Ping Pong Hue", Range(0, 1)) = 0
        _Shininess("Shininess", Range(0, 1)) = 0.5
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

            LOD 200

            Pass
            {
                Blend One OneMinusSrcAlpha
                ZWrite Off

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_instancing

                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float4 _Color;
                float _Speed;
                float _Intensity;
                float _HueSpeed;
                float _PingPongHue;
                float _Shininess;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
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
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = v.texcoord;
                    UNITY_TRANSFER_INSTANCE_ID(o, v);
                    return o;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    float2 offset = IN.texcoord * _Speed + _Time.y * _Speed;
                    float2 distortedUV = IN.texcoord + sin(offset) * _Intensity;

                    fixed4 texColor = tex2D(_MainTex, distortedUV);

                    float hue = _Time.y * _HueSpeed;
                    if (_PingPongHue > 0)
                    {
                        float pingPong = abs(fmod(hue, 2.0) - 1.0);
                        hue = lerp(0, 1, pingPong);
                    }

                    // Apply hue shift
                    float3 rgb = texColor.rgb;
                    float3 hsv = 0;
                    float K = 0;
                    if (rgb.g < rgb.b)
                    {
                        hsv.r = rgb.b;
                        hsv.g = rgb.g;
                        K = -1;
                    }
                    else
                    {
                        hsv.r = rgb.g;
                        hsv.g = rgb.b;
                        K = 0;
                    }
                    if (hsv.r < rgb.r)
                    {
                        hsv.b = rgb.r;
                        K -= 2 / 6;
                    }
                    else
                    {
                        hsv.b = hsv.r;
                        hsv.r = rgb.r;
                        K += 4 / 6;
                    }

                    hsv.x = abs(frac((hue + K) * 6) - _PingPongHue);
                    hsv.yz = texColor.ba;
                    rgb = hsv.z * lerp(hsv.y, saturate(hsv.y + _PingPongHue), hsv.x);

                    float3 finalColor = rgb * _Color.rgb;

                    fixed4 finalColorWithAlpha = fixed4(finalColor.rgb, texColor.a);

                    finalColorWithAlpha.a *= texColor.a;

                    finalColorWithAlpha.a = pow(finalColorWithAlpha.a, _Shininess);

                    return finalColorWithAlpha;
                }
                ENDCG
            }
        }
}
