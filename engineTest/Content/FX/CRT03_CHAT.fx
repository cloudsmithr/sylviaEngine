sampler2D TextureSampler : register(s0);

float2 RenderResolution;
float2 WindowResolution;
float BlurAmount = 1.5;
float ScanlineIntensity = 0.4;

float BloomStrength = 0.35;      // overall bloom intensity
float BloomThreshold = 0.35;     // 0..1 : brighter = less bloom
float BloomSoftKnee  = 0.65;     // 0..1 : how smoothly it ramps
float BloomX = 2.5;              // horizontal bloom radius multiplier
float BloomY = 1.0;              // vertical bloom radius multiplier

float Brightness = 1.2;
float TriadStrength = 0.35;
float TriadSize = 1.0;


float3 Sample(float2 uv) { return tex2D(TextureSampler, uv).rgb; }

// cheap smooth threshold
float BloomMask(float lum)
{
    // similar to your "threshold-ish" logic but stable
    return saturate((lum - BloomThreshold) / max(BloomSoftKnee, 0.0001));
}

float TriadRowOffset(float2 uv, float2 resolution)
{
    float row = floor(uv.y * resolution.y);
    return fmod(row, 2.0) * 0.5; // shift every other row
}

float3 TriadMask(float2 uv, float2 resolution, float triadSize)
{
    float x = (uv.x * resolution.x + TriadRowOffset(uv, resolution))
          / max(triadSize, 0.001);
    // fractional position inside a 3-subpixel group
    float f = frac(x / 3.0) * 3.0;

    // soft weights instead of hard stripes
    float r = saturate(1.0 - abs(f - 0.0));
    float g = saturate(1.0 - abs(f - 1.0));
    float b = saturate(1.0 - abs(f - 2.0));

    float3 m = float3(r, g, b);

    // lift dark channels
    m = lerp(float3(0.55, 0.55, 0.55), m, 0.75);

    return m;
}

float4 MainPS(float2 texCoord : TEXCOORD0) : COLOR0
{
    float2 px = 1.0 / RenderResolution;
    float2 o = px * BlurAmount;

    //barrel curve
    float2 p = texCoord * 2.0 - 1.0;
    p *= 1.0 + dot(p, p) * 0.01;
    texCoord = p * 0.5 + 0.5;
    texCoord = saturate(texCoord);
    
    // 5-tap blur (cross)
    float3 c  = Sample(texCoord) * 1;
    float lum = dot(c, float3(0.299, 0.587, 0.114));
    
    c += Sample(texCoord + float2( o.x, 0)) * 0.15;
    c += Sample(texCoord + float2(-o.x, 0)) * 0.15;
    //c += Sample(texCoord + float2( o.x*1.5, 0)) * 0.1;
    //c += Sample(texCoord + float2(-o.x*1.5, 0)) * 0.1;
    c += Sample(texCoord + float2(0,  o.y)) * 0.1;
    c += Sample(texCoord + float2(0, -o.y)) * 0.1;

    // Triads
    float3 mask = TriadMask(texCoord, WindowResolution, TriadSize);
    float triadFade = saturate(lum * 1.5); // dim areas get less mask
    c = lerp(c, c * mask, TriadStrength * triadFade);

    // --- Anisotropic Bloom (stronger horizontally) ---
    // Use the pre-triad color for luminance (triads shouldn't drive bloom)
    float bm  = BloomMask(lum);

    float2 bx = float2(px.x * BloomX, 0);
    float2 by = float2(0, px.y * BloomY);

    // 7-tap bloom kernel: wide in X, tight in Y
    float3 b = Sample(texCoord) * 0.28;
    b += Sample(texCoord + bx) * 0.18;
    b += Sample(texCoord - bx) * 0.18;
    b += Sample(texCoord + bx * 2.0) * 0.12;
    b += Sample(texCoord - bx * 2.0) * 0.12;
    b += Sample(texCoord + by) * 0.06;
    b += Sample(texCoord - by) * 0.06;

    c += b * (bm * BloomStrength);

    // Scanlines (0..1)
    float scan = 0.5 + 0.5 * sin(texCoord.y * RenderResolution.y * 6.2831853);
    scan *= 0.95 + 0.05 * sin(texCoord.y * RenderResolution.y * 0.5);
    c *= lerp(1.0 - ScanlineIntensity, 1.0, scan);

    // Vignette
    float2 v = texCoord - 0.5;
    float vig = 1.0 - dot(v, v) * 0.9;
    c *= saturate(vig);    
    
    c *= Brightness;

    return float4(saturate(c), 1.0);
}

technique CRT
{
    pass P0
    {
        PixelShader = compile ps_3_0 MainPS();
    }
}

