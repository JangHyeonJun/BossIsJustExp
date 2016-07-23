using UnityEngine;
using System.Collections;

public class Disk : MonoBehaviour {

    Transform target;
    public float speed;

    // Use this for initialization
    void Start () {

        target = GameObject.Find("Player").GetComponent<Transform>();
        if (!target)
            target = GameObject.Find("Player(Clone)").GetComponent<Transform>();
        Vector2 relativePos = target.transform.position - transform.position;

        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle - 90);

        Destroy(gameObject, 3.0f);
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }
}
