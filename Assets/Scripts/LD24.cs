using UnityEngine;
using System.Collections;
using System;

public class LD24 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        FutileParams futileParams = new FutileParams(true, true, false, false);

        futileParams.AddResolutionLevel(1024, 1, 1, "");

        futileParams.origin = new Vector2(0, 0);
        futileParams.shouldLerpToNearestResolutionLevel = false;

        Futile.instance.Init(futileParams);
        Futile.screen.SignalResize += new System.Action<bool>(screen_SignalResize);
        screen_SignalResize(true);

        Futile.atlasManager.LoadAtlas("Atlases/Atlas-1");

        FSprite sprite3 = new FSprite("Background.png");
        sprite3.anchorX = 0;
        sprite3.anchorY = sprite3.height;
        sprite3.x = 0;
        sprite3.y = Futile.screen.height;
        sprite3.scaleX = Futile.screen.width;
        sprite3.scaleY = Futile.screen.height;

        Futile.stage.AddChild(sprite3);

        FSprite sprite = new FSprite("Ludum-Dare.png");
        sprite.anchorX = 0;
        sprite.anchorY = 0;
        sprite.x = 10;
        sprite.y = 10;

        Futile.stage.AddChild(sprite);

        FSprite sprite2 = new FSprite("MrPhilGamesLogo737.png");
        sprite2.anchorX = 0;
        sprite2.anchorY = 0;
        sprite2.x = 0;
        sprite2.y = Futile.screen.height - sprite2.height;

        Futile.stage.AddChild(sprite2);

        FSprite sprite4 = new FSprite("Brick.png");
        sprite4.anchorX = 0;
        sprite4.anchorY = 0;
        sprite4.x = 50;
        sprite4.y = 50;

        Futile.stage.AddChild(sprite2);
    }

    void screen_SignalResize(bool obj)
    {
        Futile.stage.scale = Math.Max(1.0f, Math.Min(Futile.screen.width / 1024, Futile.screen.height / 768));
        Debug.Log("Screen Resized!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
