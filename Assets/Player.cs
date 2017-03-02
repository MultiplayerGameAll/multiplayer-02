using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public float moveSpeed = 0.2f;
    public float moveRotation = 10;
    public GameObject powerup;

    [SyncVar]
    private float green;

   private NetworkStartPosition[] spawnPoints;

   private static int idx;
   
   public GameObject[] objects;

    // Use this for initialization
    void Start()
    {
        GameObject obj = Instantiate(objects[idx++], transform.position, transform.rotation);
        if(idx >= objects.Length){
            idx = 0;
        }
        obj.transform.parent = transform;
   //    (Instantiate (m_Prefab, position, rotation) as GameObject).transform.parent = parentGameObject.transform; 
       spawnPoints = FindObjectsOfType<NetworkStartPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        // Renderer renderer = GetComponent<Renderer>();
        // renderer.material.color = new Color(0, green, 0);


        if (isLocalPlayer)
        {
            transform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed);
            transform.Rotate(0, Input.GetAxis("Horizontal") * moveRotation, 0);

            if (Input.GetKey(KeyCode.C))
            {
                CmdChangeColor();
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                CmdSpawnPowerup();
            }
        }
    }

    [Command]
    public void CmdChangeColor()
    {
        green = 255;
    }

    [Command]
    public void CmdSpawnPowerup()
    {
       Vector3 spawnPoint = transform.position;

        // If there is a spawn point array and the array is not empty, pick a spawn point at random
       if (spawnPoints != null && spawnPoints.Length > 0)
        {
           spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }        


         var go = (GameObject)Instantiate(powerup, spawnPoint + new Vector3(0,1,0), Quaternion.identity);
         NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

    }
}
