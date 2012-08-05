using UnityEngine;
using System.Collections;

public class MoveOnSpace : MonoBehaviour
{

    public int x;
    public int y;
    public int z;
    public KeyCode key;
    public Mouse
    public Camera camera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            camera.transform.position = new Vector3(x, y, z);
        }
    }
}
