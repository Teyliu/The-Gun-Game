Shader "Custom/PrisonBarsShader"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 3.0
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    UNITY_FOG_COORDS(1)
                    float depth : TEXCOORD1; // Added depth value
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed4 _Color;

                v2f vert(appdata_t IN)
                {
                    v2f OUT;
                    OUT.vertex = UnityObjectToClipPos(IN.vertex);
                    OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                    UNITY_TRANSFER_FOG(OUT, OUT.vertex);
                    OUT.depth = OUT.vertex.z; // Store the depth value
                    return OUT;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, IN.uv);
                    col.rgb *= col.a;

                    // Compare the depth of the fragment with the depth of the player object
                    // If the fragment is behind the player, reduce its alpha to create the effect
                    if (IN.depth < UnityObjectToClipPos(_WorldSpaceCameraPos).z)
                    {
                        col.a *= 0.5; // Adjust the alpha value as needed
                    }

                    col *= _Color; // Apply the color property

                    return col;
                }
                ENDCG
            }
        }
            FallBack "Sprites/Default"
}
