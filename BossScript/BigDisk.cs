using UnityEngine;
using System.Collections;

public class BigDisk : MonoBehaviour {

	// Use this for initialization
	void Start () {
       GetComponent<Rigidbody2D>().AddForce(new Vector2(-500.0f, 300.0f));

        Destroy(gameObject, 5.0f);
    }
	
	// Update is called once per frame
	void Update () {

     
	}
}
