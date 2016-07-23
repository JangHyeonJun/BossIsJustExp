using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager current;



    // 여기서부터 관리할 변수 목록
    public Transform somePrefab;

    public int score;
    public string playerName;




    //
    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
            current = this;
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
