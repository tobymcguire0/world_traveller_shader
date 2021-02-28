Shader "Custom/Stencil"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Cut ("display name", Vector) = (0,0,0,0)
    }
    SubShader
    {
        LOD 100
        Blend One OneMinusSrcAlpha
        Tags { "Queue" = "Geometry-1" }  // Write to the stencil buffer before drawing any geometry to the screen
        ColorMask 0 // Don't write to any colour channels
        Cull Off
        ZWrite Off // Don't write to the Depth buffer

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
            float4 _Cut;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                clip(0);
                return float4(1,1,1,0);
            }
            ENDCG
        }
    }
}