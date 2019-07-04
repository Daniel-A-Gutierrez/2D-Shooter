using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeP2 : MonoBehaviour
{
    public Sprite altSprite;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player1");
        if(go != null) 
        {
            if(go != gameObject)
            {
                GetComponent<SpriteRenderer>().sprite = altSprite; 
                gameObject.tag = "Player2";
                GetComponent<NetController>().enemyTag = "Player1";
            }
        }
    }

}
