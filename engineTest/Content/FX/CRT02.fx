sampler2D TextureSampler : register(s0);

float2 Resolution;
float BlurAmount = 2.0;
float ScanlineIntensity = 0.4;
float Bloom = 0.3;

float4 MainPS(float2 texCoord : TEXCOORD0) : COLOR0
{
    float2 pixelSize = 1.0 / Resolution;
    
    // 9-tap blur
    float3 color = tex2D(TextureSampler, texCoord).rgb * 4.0;
    color += tex2D(TextureSampler, texCoord + float2(-pixelSize.x, 0) * BlurAmount).rgb * 2.0;
    color += tex2D(TextureSampler, texCoord + float2(pixelSize.x, 0) * BlurAmount).rgb * 2.0;
    color += tex2D(TextureSampler, texCoord + float2(0, -pixelSize.y) * BlurAmount).rgb * 2.0;
    color += tex2D(TextureSampler, texCoord + float2(0, pixelSize.y) * BlurAmount).rgb * 2.0;
    color += tex2D(TextureSampler, texCoord + float2(-pixelSize.x, -pixelSize.y) * BlurAmount).rgb;
    color += tex2D(TextureSampler, texCoord + float2(pixelSize.x, -pixelSize.y) * BlurAmount).rgb;
    color += tex2D(TextureSampler, texCoord + float2(-pixelSize.x, pixelSize.y) * BlurAmount).rgb;
    color += tex2D(TextureSampler, texCoord + float2(pixelSize.x, pixelSize.y) * BlurAmount).rgb;
    color /= 16.0;
    
    // Second blur pass for extra softness
    float3 color2 = tex2D(TextureSampler, texCoord).rgb * 4.0;
    float blur2 = BlurAmount * 2.0;
    color2 += tex2D(TextureSampler, texCoord + float2(-pixelSize.x, 0) * blur2).rgb * 2.0;
    color2 += tex2D(TextureSampler, texCoord + float2(pixelSize.x, 0) * blur2).rgb * 2.0;
    color2 += tex2D(TextureSampler, texCoord + float2(0, -pixelSize.y) * blur2).rgb * 2.0;
    color2 += tex2D(TextureSampler, texCoord + float2(0, pixelSize.y) * blur2).rgb * 2.0;
    color2 += tex2D(TextureSampler, texCoord + float2(-pixelSize.x, -pixelSize.y) * blur2).rgb;
    color2 += tex2D(TextureSampler, texCoord + float2(pixelSize.x, -pixelSize.y) * blur2).rgb;
    color2 += tex2D(TextureSampler, texCoord + float2(-pixelSize.x, pixelSize.y) * blur2).rgb;
    color2 += tex2D(TextureSampler, texCoord + float2(pixelSize.x, pixelSize.y) * blur2).rgb;
    color2 /= 16.0;
    
    // Blend the two blur levels
    color = lerp(color, color2, 0.5);
    
    // Bloom - add extra glow to bright areas
    float brightness = dot(color, float3(0.299, 0.587, 0.114));
    color += color * brightness * Bloom;
    
    // Scanlines
    float scanline = sin(texCoord.y * Resolution.y * 3.14159 * 2.0);
    scanline = clamp(scanline, 0.0, 1.0);
    color *= lerp(1.0 - ScanlineIntensity, 1.0, scanline);
    
    // Brightness boost
    color *= 1.3;
    
    // Vignette
    float2 vig = texCoord - 0.5;
    color *= 1.0 - dot(vig, vig) * 0.4;
    
    return float4(color, 1.0);
}

technique CRT
{
    pass Pass1
    {
        PixelShader = compile ps_3_0 MainPS();
    }
}