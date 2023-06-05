Shader "Custom/StretchySprite"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
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

            float2 borderSize = float2(0.05, 0.05); // Adjust the border size as needed
            float2 stretchAmount = float2(0.1, 0.1); // Adjust the stretch amount as needed

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Calculate the border area
                float2 borderArea = float2(1.0, 1.0) - 2.0 * borderSize;

                // Adjust UV coordinates for stretching
                float2 uv = v.uv;
                uv -= borderSize;
                uv = float2(uv.x / borderArea.x, uv.y / borderArea.y);
                uv *= float2(1.0 - stretchAmount.x, 1.0 - stretchAmount.y);
                uv += stretchAmount * 0.5;
                uv = float2(uv.x * borderArea.x, uv.y * borderArea.y);
                uv += borderSize;

                o.uv = uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}