using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Components;
using SylviaEngine.Engine;
using SylviaEngine.Enums;

namespace SylviaEngine.Graphics;

public class RenderSystem : IRenderSystem
{
    private readonly List<RenderComponent> _backgroundList = new();
    private readonly List<RenderComponent> _spriteList = new();
    private readonly List<RenderComponent> _uiList = new();
    private readonly List<RenderComponent> _effectList = new();
    public static IRenderSystem Instance { get; set; } = new RenderSystem(); // replaceable\

    public SpriteBatch SpriteBatch { get; set; }
    public RenderTarget2D RenderTarget { get; set; }
    
    public Effect CRTEffect { get; set; }
    
    private Texture2D TestSprite;
    
    public void Register(RenderComponent r)
    {
        // Note: ZIndex is fixed at registration time. 
        // To change Z-order, unregister and re-register the object.
        
        List<RenderComponent> layer = GetLayer(r.Layer);
        
        // let's find the first ZIndex in the list larger than r's and insert it before that.
        int index = layer.FindIndex(x => x.ZIndex > r.ZIndex);
        
        if (index == -1)
            layer.Add(r);
        else
            layer.Insert(index, r);
    }

    public void Unregister(RenderComponent r)
    {
        GetLayer(r.Layer).Remove(r);   
    }
    
    public void RenderAll()
    {
        SpriteBatch.Begin(sortMode: SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp);
        foreach (var r in _backgroundList) r.Render(SpriteBatch);
        SpriteBatch.End();

        SpriteBatch.Begin(sortMode: SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp);
        foreach (var r in _spriteList) r.Render(SpriteBatch);
        SpriteBatch.End();

        SpriteBatch.Begin(sortMode: SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp);
        
        foreach (var r in _effectList) r.Render(SpriteBatch);
        SpriteBatch.End();

        SpriteBatch.Begin(sortMode: SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp);
        foreach (var r in _uiList) r.Render(SpriteBatch);
        
        if (TestSprite is null)
            TestSprite = Core.Content.Load<Texture2D>("dracula");
        SpriteBatch.Draw(TestSprite, new Vector2(10, 10), Color.White);

        SpriteBatch.End();
    }

    public void RenderToWindow(int width, int height, Color color, GameTime gameTime)
    {
        if (CRTEffect != null)
        {
            CRTEffect.Parameters["RenderResolution"].SetValue(
                new Vector2(RenderTarget.Width, RenderTarget.Height));
            CRTEffect.Parameters["WindowResolution"].SetValue(
                new Vector2(width, height));
            CRTEffect.Parameters["BlurAmount"].SetValue(0.7f); // 1.15

            CRTEffect.Parameters["BloomStrength"].SetValue(0.85f);
            CRTEffect.Parameters["BloomThreshold"].SetValue(0.25f);
            CRTEffect.Parameters["BloomSoftKnee"].SetValue(0.65f);
            CRTEffect.Parameters["BloomX"].SetValue(2.5f);
            CRTEffect.Parameters["BloomY"].SetValue(1f);

            CRTEffect.Parameters["ScanlineIntensity"].SetValue(0.2f);
            CRTEffect.Parameters["Brightness"].SetValue(1.1f);
            CRTEffect.Parameters["TriadStrength"].SetValue(0.2f);
            CRTEffect.Parameters["TriadSize"].SetValue(2f);
        }
        //SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        SpriteBatch.Begin(samplerState: SamplerState.LinearClamp, effect: CRTEffect);
        SpriteBatch.Draw(RenderTarget, new Rectangle(0, 0, width, height), color);
        SpriteBatch.End();
    }
    
    public List<RenderComponent> GetLayer(RenderLayer layer)
    {
        switch (layer)
        {
            case RenderLayer.Background:
                return _backgroundList;
            case RenderLayer.Sprite:
                return _spriteList;
            case RenderLayer.UI:
                return _uiList;
            case RenderLayer.Effect:
                return _effectList;
            default:
                return _spriteList;
        }
    }
}