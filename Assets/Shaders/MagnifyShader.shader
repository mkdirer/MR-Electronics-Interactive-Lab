Shader "Custom/MagnifyShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _MagnifyRenderTex("Magnify Render Texture", 2D) = "black" {}
        _MagnifyCenter("Magnify Center", Vector) = (0.5, 0.5, 0, 0)
        _MagnifyRadius("Magnify Radius", Float) = 0.25
        _MagnifyPower("Magnify Power", Float) = 1.5
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
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
                };
                sampler2D _MainTex;
                sampler2D _MagnifyRenderTex;
                float4 _MagnifyCenter;
                float _MagnifyRadius;
                float _MagnifyPower;
                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }
                fixed4 frag(v2f i) : SV_Target
                {
                    float2 uv = i.uv;
                    float2 center = _MagnifyCenter.xy;
                    float dist = distance(uv, center);

                    if (dist < _MagnifyRadius)
                    {
                        float scale = _MagnifyPower;
                        uv = center + (uv - center) * scale;
                    }

                    fixed4 color = tex2D(_MainTex, uv);
                    return color;
                }
                ENDCG
            }
        }
}
