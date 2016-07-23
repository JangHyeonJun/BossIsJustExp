using UnityEngine;
using System.Collections;

public class Clown1_Mine : MonoBehaviour {

    void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("push");
            if (col.gameObject.GetComponent<Animator>().GetBool("GuardOn"))
            {
                if (col.transform.position.x < transform.position.x)
                {
                    col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-20.0f, 2.0f));
                }
                else
                    col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(20.0f, 2.0f));
            }
            else
            {
                if (col.transform.position.x < transform.position.x)
                {
                    col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400.0f, 0.0f));
                }
                else
                    col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(400.0f, 0.0f));
            }
        }
    }

    public void Dest()
    {
        Destroy(gameObject);
    }
}
