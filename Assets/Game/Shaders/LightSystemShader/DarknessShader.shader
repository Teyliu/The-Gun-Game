Shader "Custom/DarknessShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _DarknessIntensity("Darkness Intensity", Range(0, 1)) = 0.5
        _IsAffected("Is Affected", Float) = 1
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" }
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
                float _DarknessIntensity;
                float _IsAffected;

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

                // Apply darkness based on intensity only if the object is affected
                texColor.rgb *= (1.0 - _DarknessIntensity * _IsAffected);

                return texColor;
            }
            ENDCG
        }
        }
}
