using UnityEngine;
using System.Collections;

public class LD24 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        FutileParams futileParams = new FutileParams(true, true, false, false);

        futileParams.AddResolutionLevel(1024, 1, 1, "");

        futileParams.origin = new Vector2(0, 0);

        Futile.instance.Init(futileParams);

        Futile.atlasManager.LoadAtlas("Atlases/Atlas-1");

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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
