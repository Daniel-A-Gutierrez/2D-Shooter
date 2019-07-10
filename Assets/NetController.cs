using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetController : NetworkBehaviour
{
    public GameObject bullet;
    public float offset;
    Rigidbody2D rb2;
    public float rotationspeed;
    public float travelSpeed;
    public string enemyTag;

    public KeyCode left;
    public KeyCode right;
    public KeyCode forward;
    public KeyCode back;
    public KeyCode shoot;
    

    void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            if(Input.GetKeyDown(shoot))
            {
                CmdFire();
            }


            if(Input.GetAxis("Horizontal")  != 0)
            {
                rb2.rotation -=  Input.GetAxis("Horizontal") * rotationspeed;
            }
            if(Input.GetKey(left))
            {
                rb2.rotation +=   rotationspeed;
            }
            if(Input.GetKey(right))
            {
                rb2.rotation -=   rotationspeed;
            }


            if(Input.GetAxis("Vertical") != 0)
            {
                rb2.position += new Vector2(transform.right.x, transform.right.y)*Input.GetAxis("Vertical") * travelSpeed;
            }
            if(Input.GetKey(forward))
            {
                rb2.position += new Vector2(transform.right.x, transform.right.y) * travelSpeed;
            }
            if(Input.GetKey(back))
            {
                rb2.position -= new Vector2(transform.right.x, transform.right.y)* travelSpeed;
            }
        }
    }

    [SyncVar]
    public int hp;

    [Command]
    void CmdFire()
    {
        RpcFireBullet();
    }

    [ClientRpc]
    void RpcFireBullet()
    {
        GameObject b = Instantiate(bullet,transform.position + transform.right *offset,Quaternion.identity);
        NetworkServer.Spawn(b);
        Bullet B = b.GetComponent<Bullet>();
        B.Direction = transform.right;
        B.hitsWhatTag = enemyTag;
    }

    [ClientRpc]
    public void RpcDamageThis()
    {
        hp--;
        if(hp<=0)
            Destroy(gameObject);
    }

}
