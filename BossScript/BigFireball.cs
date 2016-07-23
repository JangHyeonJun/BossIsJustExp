using UnityEngine;
using System.Collections;

public class BigFireball : MonoBehaviour
{

    float speed;
    Dragon2_Control drg2;
    bool left;
    void Start()
    {
        speed = 30.0f;
        if (!drg2)
        {
            try
            {
                drg2 = GameObject.Find("ClownBoy2(Clone)").GetComponent<Dragon2_Control>();
                left = drg2.w_left;
            }
            catch
            {
                drg2 = GameObject.Find("ClownBoy2").GetComponent<Dragon2_Control>();
                left = drg2.w_left;

            }
        }
    }


    void Update()
    {

        if (left)
        {
            transform.Translate(-0.5f * speed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            transform.Translate(0.5f * speed * Time.deltaTime, 0f, 0f);
        }

        if (Mathf.Abs(drg2.gameObject.GetComponent<Transform>().position.x - transform.position.x) > 25)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Attack" && col.name != "ClownBoy2" && col.tag != "Ground" && col.name != "Fire(Clone)" && col.tag != "Boss")
        {
            Destroy(this.gameObject, 0.05f);
        }

        if (col.gameObject.tag == "Player")
        {
            Debug.Log("충돌체" + col);
            if (col.transform.position.x < transform.position.x)
            {
                col.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400.0f, 200.0f));
            }
            else
            {
                col.GetComponent<Rigidbody2D>().AddForce(new Vector2(400.0f, 200.0f));
            }

        }
    }
}
