Shader "Custom/CRT"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.6
        _Curvature ("Curvature", Range(0, 1)) = 0.2
        _VignetteIntensity ("Vignette Intensity", Range(0, 1)) = 0.3
        _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.1
        _Aberration ("Chromatic Aberration", Range(0, 5)) = 1.0
        _MaskStrength ("Shadow Mask Strength", Range(0, 1)) = 0.3
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _ScanlineIntensity;
            float _Curvature;
            float _VignetteIntensity;
            float _NoiseIntensity;
            float _Aberration;
            float _MaskStrength;

            float2 BarrelDistortion(float2 uv, float k)
            {
                uv = uv * 2.0 - 1.0;
                float r2 = dot(uv, uv);
                uv *= 1.0 + k * r2;
                return (uv + 1.0) * 0.5;
            }

            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453 + _Time.y * 10.0);
            }

            float3 SampleWithAberration(float2 uv, float aberration)
            {
                float2 offset = aberration * _MainTex_TexelSize.xy;
                float r = tex2D(_MainTex, uv + offset).r;
                float g = tex2D(_MainTex, uv).g;
                float b = tex2D(_MainTex, uv - offset).b;
                return float3(r, g, b);
            }

            float3 ApplyShadowMask(float2 uv)
{
            float screenLine = floor(frac(uv.y * _ScreenParams.y) * 3.0);
            float3 mask = float3(1, 1, 1);

            if (screenLine == 0.0)      mask = float3(1, 0.7, 0.7); // R
            else if (screenLine == 1.0) mask = float3(0.7, 1, 0.7); // G
            else                        mask = float3(0.7, 0.7, 1); // B

            return mask;
}

           fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = BarrelDistortion(i.uv, _Curvature);

                // Discard outside edges
                if (uv.x < 0 || uv.x > 1 || uv.y < 0 || uv.y > 1)
                    return float4(0, 0, 0, 1);

                // Chromatic Aberration
                float3 col = SampleWithAberration(uv, _Aberration);

                // Shadow Mask
                float3 mask = ApplyShadowMask(uv);
                col *= lerp(float3(1.0, 1.0, 1.0), mask, _MaskStrength);

                // Scanlines (dynamic vertical lines)
                float scan = sin(uv.y * _ScreenParams.y * 1.5) * 0.5 + 0.5;
                col *= lerp(1.0, scan, _ScanlineIntensity);

                // Horizontal blur (cheap smear)
                float3 blur = tex2D(_MainTex, uv + float2(_MainTex_TexelSize.x * 1.5, 0)).rgb;
                col = lerp(col, blur, 0.1);

                // Vignette
                float2 dist = uv - 0.5;
                float vignette = 1.0 - dot(dist, dist) * 2.5;
                col *= lerp(1.0, vignette, _VignetteIntensity);

                // Static noise
                float n = rand(uv * _ScreenParams.xy);
                col = lerp(col, float3(n, n, n), _NoiseIntensity);

                return float4(col, 1.0);
            }
            ENDCG
        }
    }
}
