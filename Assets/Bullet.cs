using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    public Vector3 Direction;
    public float lifetime;
    float startTime;
    Rigidbody2D rb2;
    public float speed;
    public string hitsWhatTag;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        rb2 = GetComponent<Rigidbody2D>();
        rb2.velocity = Direction*speed;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Time.time-startTime > lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(isServer)
        {
            try
            {
                GameObject go = col.gameObject;
                Destroy(gameObject);
                if(go != null && go.tag == hitsWhatTag)
                {
                    go.GetComponent<NetController>().RpcDamageThis();
                }
            }
            catch(System.Exception e) {}
        }
        else
        {
            Destroy(gameObject);
        }
    }




    void Collide( NetworkIdentity ni)
    {
        if(ni != null)
        {
            GameObject go = GameObject.Find(ni.gameObject.name);
            if(go != null && go.tag == hitsWhatTag)
            {
                go.GetComponent<NetController>().RpcDamageThis();
            }
        }
        Destroy(gameObject);
    }
}
