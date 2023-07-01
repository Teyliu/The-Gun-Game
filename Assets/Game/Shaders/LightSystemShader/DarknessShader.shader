Shader "Custom/DarknessShader"
{
    Properties
    {
    _MainTex("Main Texture", 2D) = "white" {}
    _DarknessColor("Darkness Color", Color) = (0, 0, 0, 1)
    _DarknessIntensity("Darkness Intensity", Range(0, 1)) = 0.5
    _DarknessTransparency("Darkness Transparency", Range(0, 1)) = 0.5
    }

        SubShader
    {
        Tags { "Queue" = "Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // Enable transparency blending

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
            float4 _DarknessColor;
            float _DarknessIntensity;
            float _DarknessTransparency;

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
                texColor.rgb *= _DarknessColor.rgb * _DarknessIntensity; // Apply darkness color and intensity
                texColor.a *= _DarknessTransparency; // Apply darkness transparency
                return texColor;
            }

            ENDCG
        }
    }
}