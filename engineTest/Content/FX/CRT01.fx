sampler2D TextureSampler : register(s0);

float2 Resolution;
float PhosphorIntensity = 0.3;
float BloomAmount = 0.15;
float ScanlineIntensity = 0.2;
float Curvature = 0.05;

float4 MainPS(float2 texCoord : TEXCOORD0) : COLOR0
{
    // Barrel distortion
    float2 uv = texCoord - 0.5;
    float dist = dot(uv, uv);
    uv *= 1.0 + dist * Curvature;
    uv += 0.5;
    
    if (uv.x < 0 || uv.x > 1 || uv.y < 0 || uv.y > 1)
        return float4(0, 0, 0, 1);
    
    float4 color = tex2D(TextureSampler, uv);
    
    // Phosphor mask pattern (RGB stripes)
    float pixelX = uv.x * Resolution.x;
    int phosphorPhase = (int)pixelX % 3;
    
    float3 phosphorMask;
    if (phosphorPhase == 0)
        phosphorMask = float3(1.0, 0.2, 0.2);
    else if (phosphorPhase == 1)
        phosphorMask = float3(0.2, 1.0, 0.2);
    else
        phosphorMask = float3(0.2, 0.2, 1.0);
    
    // Apply phosphor mask
    float3 phosphorColor = color.rgb * phosphorMask;
    
    // Simulate phosphor bleed/glow by sampling neighbors and blending
    float2 pixelSize = 1.0 / Resolution;
    float3 bloom = float3(0, 0, 0);
    bloom += tex2D(TextureSampler, uv + float2(-pixelSize.x, 0)).rgb;
    bloom += tex2D(TextureSampler, uv + float2(pixelSize.x, 0)).rgb;
    bloom += tex2D(TextureSampler, uv + float2(0, -pixelSize.y)).rgb;
    bloom += tex2D(TextureSampler, uv + float2(0, pixelSize.y)).rgb;
    bloom *= 0.25;
    
    // Blend phosphor pattern with bloom
    float3 finalColor = lerp(phosphorColor, bloom, BloomAmount);
    
    // Boost brightness to compensate for phosphor darkening
    finalColor *= (1.0 + PhosphorIntensity);
    
    // Scanlines
    float scanline = sin(uv.y * Resolution.y * 3.14159);
    scanline = scanline * 0.5 + 0.5;
    finalColor *= 1.0 - ScanlineIntensity * (1.0 - scanline);
    
    // Vignette
    float2 vignetteUV = texCoord - 0.5;
    float vignette = 1.0 - dot(vignetteUV, vignetteUV) * 0.5;
    finalColor *= vignette;
    
    return float4(finalColor, 1.0);
}

technique CRT
{
    pass Pass1
    {
        PixelShader = compile ps_3_0 MainPS();
    }
}