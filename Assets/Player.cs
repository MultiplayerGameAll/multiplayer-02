using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public float moveSpeed = 0.2f;
    public float moveRotation = 10;

    [SyncVar]
    private float green;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = new Color(0, green, 0);


        if (isLocalPlayer)
        {
            transform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed);
            transform.Rotate(0, Input.GetAxis("Horizontal") * moveRotation, 0);

            if (Input.GetKey(KeyCode.C))
            {
                CmdChangeColor();
            }
        }
    }

    [Command]
    public void CmdChangeColor()
    {
        green = 255;
    }
}
