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
        if(col.gameObject.tag == hitsWhatTag)
        {
            col.gameObject.GetComponent<NetController>().CmdDamageThis();
        }
        Destroy(gameObject);
    }
}
