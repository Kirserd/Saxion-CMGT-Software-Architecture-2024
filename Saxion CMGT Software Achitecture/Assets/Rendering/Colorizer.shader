Shader "Hidden/Colorizer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Dark Main", Color) = (1, 1, 1, 1)
        _Color2 ("Light Main", Color) = (0, 0, 0, 1)
        _Color3 ("Dark Secondary", Color) = (1, 1, 1, 1)
        _Color4 ("Light Secondary", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed4 _Color1;
            fixed4 _Color2;
            fixed4 _Color3;
            fixed4 _Color4;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col = lerp(lerp(_Color1, _Color2, col.r), lerp(_Color3, _Color4, col.b), col.g);
                return col;
            }
            ENDCG
        }
    }
}
