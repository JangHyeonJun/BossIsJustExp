using UnityEngine;
using System.Collections;

public class Clown1_Attack : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<Animator>().GetBool("GuardOn"))
            {
                if (col.transform.position.x < transform.position.x)
                {
                    col.GetComponent<Rigidbody2D>().AddForce(new Vector2(-20.0f, 2.0f));
                }
                else
                    col.GetComponent<Rigidbody2D>().AddForce(new Vector2(20.0f, 2.0f));
            }
            else
            {
                if (col.transform.position.x < transform.position.x)
                {
                    col.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300.0f, 0.0f));
                }
                else
                    col.GetComponent<Rigidbody2D>().AddForce(new Vector2(300.0f, 0.0f));
            }

        }
    }
}
