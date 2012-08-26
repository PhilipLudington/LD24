using UnityEngine;
using System.Collections;

public class Brick : FSprite
{
    public bool BeingKilled = false;
    public Tween Tween = null;

    public Brick()
        : base("Brick.png")
    {
        anchorX = 0;
        anchorY = 0;
        color = new Color(UnityEngine.Random.Range(0, 100) / 100.0f,
            UnityEngine.Random.Range(0, 100) / 100.0f,
            UnityEngine.Random.Range(0, 100) / 100.0f, 1);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
