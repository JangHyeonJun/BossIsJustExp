using UnityEngine;
using System.Collections;

public class Clown3_Bullet_Control : MonoBehaviour {

    Player_Control playerCtrl;
    public Clown3_Control clown3;
    public float speed;

    bool playerOn = false;
    bool left;
    Animator anim;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        Destroy(gameObject, 5.0f);
        clown3 = GameObject.Find("ClownBoy3").GetComponent<Clown3_Control>();
        if(!clown3)
            clown3 = GameObject.Find("ClownBoy3(Clone)").GetComponent<Clown3_Control>();
        left = clown3.w_left;

	}
	
	// Update is called once per frame
	void Update () {

        if(!playerOn)
        {
            playerCtrl = GameObject.Find("Player").GetComponent<Player_Control>();
        }
        if(Mathf.Abs(playerCtrl.transform.position.x - transform.position.x)<0.3f)
        {
            anim.SetTrigger("Explode");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("ClownBoy3_Bullet_idle"))
        {
            if (left)
            {
                transform.Translate(-0.2f * speed * Time.deltaTime, 0f, 0f);
            }
            else
            {
                transform.Translate(0.2f * speed * Time.deltaTime, 0f, 0f);
            }
        }
    }


    public void Dead()
    {
        Destroy(gameObject);
    }
}
