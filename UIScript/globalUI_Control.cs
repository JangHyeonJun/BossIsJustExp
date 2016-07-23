using UnityEngine;
using System.Collections;

public class globalUI_Control : MonoBehaviour {
    Manager mng;

    public GameObject textObject;
    public GameObject Play;
    public GameObject Combination;
    public GameObject Single;
    public GameObject Title;
    public GameObject Mode;
    public GameObject clearButton;
    public GameObject notclearButton;
    public GameObject player;
    float timer = 0.0f;
    bool on = true;
    // Use this for initialization
    void Start()
    {
        mng = GameObject.Find("GameManager").GetComponent<Manager>();
        Play = GameObject.Find("UI_Play");
        Combination = GameObject.Find("UI_Combination");
        Single = GameObject.Find("UI_Single");
        Title = GameObject.Find("UI_Title");
        Mode = GameObject.Find("UI_ModeSelect");

        Play.SetActive(false);
        Combination.SetActive(false);
        Single.SetActive(false);
        Mode.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            if (!on)
            {
                textObject.SetActive(true);
                on = true;
                timer = 0;
            }
            else
            {
                textObject.SetActive(false);
                on = false;
                timer = 0;
            }
        }
    }

    public void selInfi()
    {

    }
    public void selMulti()
    {

    }
    public void selSingle()
    {
        Single.SetActive(true);
        Mode.SetActive(false);
    }

    public void titleDestroy()
    {
        Mode.SetActive(true);
        Title.SetActive(false);
    }

    public void  combSet()
    {
        Combination.SetActive(true);
    }
    public void combUnSet()
    {
        Combination.SetActive(false);
    }
    public void playuiSet()
    {
        Play.SetActive(true);
    }
    public void clearGame()
    {
        clearButton.SetActive(false);
        Play.SetActive(false);
        Single.SetActive(true);
    }
    public void notclearGame()
    {
        GameObject[] buf;
        buf = GameObject.FindGameObjectsWithTag("Boss");
        foreach(GameObject bu in buf)
        {
            Destroy(bu);
        }
        notclearButton.SetActive(false);
        Play.SetActive(false);
        Single.SetActive(true);
    }
    public void startHunt()
    {
        player.SetActive(true);
        player.GetComponent<Player_Control>().Player_HP = mng.playerHp;
        player.transform.position = new Vector3(3, 0, 0);
    }
}
