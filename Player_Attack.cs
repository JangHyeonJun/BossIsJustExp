using UnityEngine;
using System.Collections;

public class Player_Attack : MonoBehaviour {

    GameObject paren;
    // Use this for initialization
    void Start () {
        paren = GameObject.Find("Player");
        
	}
	
	// Update is called once per frame
	void Update () {

       
	}
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss")
        {
            if (col.transform.position.x < paren.transform.position.x)
            {
                col.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300.0f, 2.0f));
            }
            else
                col.GetComponent<Rigidbody2D>().AddForce(new Vector2(300.0f, 2.0f));


        }
    }
}
