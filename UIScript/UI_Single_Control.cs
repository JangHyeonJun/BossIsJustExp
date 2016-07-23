using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Single_Control : MonoBehaviour {

    Manager mng;
    Player_Control pCtrl;

    public GameObject selectedBoss;
    public GameObject[] bosslist = new GameObject[2];


    public GameObject[] check_1 = new GameObject[5];
    public GameObject[] boss_1 = new GameObject[5];
    public GameObject[] lockBoss_1 = new GameObject[5];

    public GameObject[] check_2 = new GameObject[3];
    public GameObject[] boss_2 = new GameObject[3];
    public GameObject[] lockBoss_2 = new GameObject[3];
    public Transform sp;
    public bool clickOn = false;
    public Text stageTxt;

    public int currentStage = 1;

    public Text atkTxt;
    public Text defTxt;
    public Text movTxt;
    public Text currentSkill;
	// Use this for initialization
	void Start () {
        mng = GameObject.Find("GameManager").GetComponent<Manager>();
        pCtrl = GameObject.Find("Player").GetComponent<Player_Control>();
        if (!pCtrl)
            pCtrl = GameObject.Find("Player(Clone)").GetComponent<Player_Control>();
    }
	
	// Update is called once per frame
	void Update () {

        viewText();

        if (currentStage == 1)
        {
            stageTxt.text = "스테이지1";
            bosslist[0].SetActive(true);
            bosslist[1].SetActive(false);
            checklockboss_1();
            checkboss_1();
            GetComponentInChildren<ScrollRect>().content = bosslist[0].GetComponent<RectTransform>();
            
        }
        else if(currentStage == 2)
        {
            stageTxt.text = "스테이지2";
            bosslist[0].SetActive(false);
            bosslist[1].SetActive(true);
            checklockboss_2();
            checkboss_2();
            GetComponentInChildren<ScrollRect>().content = bosslist[1].GetComponent<RectTransform>();

        }
        
	}

    void checkboss_1()
    {
        if (clickOn)
        {
            switch (selectedBoss.name)
            {

                case "Boss0":
                    {
                        // 만약 해당 보스가 잠금 상태라면 체크를 할 수 없음.
                        if (lockBoss_1[0].activeSelf)
                            return;

                        for (int i = 0; i < 5; i++)
                        {
                            check_1[i].SetActive(false);
                        }
                        check_1[0].SetActive(true);
                    }
                    break;
                case "Boss1":
                    {
                        // 만약 해당 보스가 잠금 상태라면 체크를 할 수 없음.
                        if (lockBoss_1[1].activeSelf)
                            return;

                        for (int i = 0; i < 5; i++)
                        {
                            check_1[i].SetActive(false);
                        }
                        check_1[1].SetActive(true);
                    }
                    break;
                case "Boss2":
                    {
                        // 만약 해당 보스가 잠금 상태라면 체크를 할 수 없음.
                        if (lockBoss_1[2].activeSelf)
                            return;

                        for (int i = 0; i < 5; i++)
                        {
                            check_1[i].SetActive(false);
                        }
                        check_1[2].SetActive(true);
                    }
                    break;
                case "Boss3":
                    {
                        // 만약 해당 보스가 잠금 상태라면 체크를 할 수 없음.
                        if (lockBoss_1[3].activeSelf)
                            return;

                        for (int i = 0; i < 5; i++)
                        {
                            check_1[i].SetActive(false);
                        }
                        check_1[3].SetActive(true);
                    }
                    break;
                case "Boss4":
                    {
                        // 만약 해당 보스가 잠금 상태라면 체크를 할 수 없음.
                        if (lockBoss_1[4].activeSelf)
                            return;

                        for (int i = 0; i < 5; i++)
                        {
                            check_1[i].SetActive(false);
                        }
                        check_1[4].SetActive(true);
                    }
                    break;
                default:
                    break;
            }
            clickOn = false;
        }
    }
    void checklockboss_1()
    {
        for (int i = 1; i < 5; i++)
        {
            if (mng.clearBoss[i-1] >= 1)
                lockBoss_1[i].SetActive(false);
        }
    }

    void checkboss_2()
    {
        if (clickOn)
        {
            switch (selectedBoss.name)
            {

                case "Boss5":
                    {
                        // 만약 해당 보스가 잠금 상태라면 체크를 할 수 없음.
                        if (lockBoss_2[0].activeSelf)
                            return;

                        for (int i = 0; i < 3; i++)
                        {
                            check_2[i].SetActive(false);
                        }
                        check_2[0].SetActive(true);
                    }
                    break;
                case "Boss6":
                    {
                        // 만약 해당 보스가 잠금 상태라면 체크를 할 수 없음.
                        if (lockBoss_2[1].activeSelf)
                            return;

                        for (int i = 0; i < 3; i++)
                        {
                            check_2[i].SetActive(false);
                        }
                        check_2[1].SetActive(true);
                    }
                    break;
                case "Boss7":
                    {
                        // 만약 해당 보스가 잠금 상태라면 체크를 할 수 없음.
                        if (lockBoss_2[2].activeSelf)
                            return;

                        for (int i = 0; i < 3; i++)
                        {
                            check_2[i].SetActive(false);
                        }
                        check_2[2].SetActive(true);
                    }
                    break;
                default:
                    break;
            }
            clickOn = false;
        }
    }
    void checklockboss_2()
    {
        for (int i = 5; i < 8; i++)
        {
            if (mng.clearBoss[i-1] >= 1)
                lockBoss_2[i-5].SetActive(false);
        }
    }

    void viewText()
    {
        // 보스 선택창에서 플레이어의 능력치와 현재 착용중인 특수능력을 보여줌
        if (int.Parse(atkTxt.text) != mng.stat[0])
            atkTxt.text = mng.stat[0].ToString();
        if (int.Parse(defTxt.text) != mng.stat[1])
            defTxt.text = mng.stat[1].ToString();
        if (int.Parse(movTxt.text) != mng.stat[2])
            movTxt.text = mng.stat[2].ToString();
        if (currentSkill.text != mng.currentSkill)
            currentSkill.text = mng.currentSkill;
    }
    public void setGameobject(GameObject obj)
    {
        selectedBoss = obj;
        clickOn = true;
    }

    public void startHunt()
    {
        if(selectedBoss)
        {
            switch (selectedBoss.name)
            {

                case "Boss0":
                    {
                        if (lockBoss_1[0].activeSelf)
                            return;
                        Instantiate(boss_1[0], sp.position, Quaternion.identity);
                    }
                    break;
                case "Boss1":
                    {
                        if (lockBoss_1[1].activeSelf)
                            return;
                        Instantiate(boss_1[1], sp.position, Quaternion.identity);
                    }
                    break;
                case "Boss2":
                    {
                        if (lockBoss_1[2].activeSelf)
                            return;
                        Instantiate(boss_1[2], sp.position, Quaternion.identity);
                    }
                    break;
                case "Boss3":
                    {
                        if (lockBoss_1[3].activeSelf)
                            return;
                        Instantiate(boss_1[3], sp.position, Quaternion.identity);
                    }
                    break;
                case "Boss4":
                    {
                        if (lockBoss_1[4].activeSelf)
                            return;
                        Instantiate(boss_1[4], sp.position, Quaternion.identity);
                    }
                    break;





                case "Boss5":
                    {
                        if (lockBoss_2[0].activeSelf)
                            return;
                        Instantiate(boss_2[0], sp.position, Quaternion.identity);
                    }
                    break;
                case "Boss6":
                    {
                        if (lockBoss_2[1].activeSelf)
                            return;
                        Instantiate(boss_2[1], sp.position, Quaternion.identity);
                    }
                    break;
                case "Boss7":
                    {
                        if (lockBoss_2[2].activeSelf)
                            return;
                        Instantiate(boss_2[2], sp.position, Quaternion.identity);
                    }
                    break;
                default:
                    break;
            }
            pCtrl.playerSync();
            GetComponentInParent<globalUI_Control>().playuiSet();
            GetComponentInParent<globalUI_Control>().startHunt();
            gameObject.SetActive(false);
            
        }
    }


    //스테이지 조절
    public void nextStage()
    {
        if (currentStage <= 1)
            currentStage++;
    }
    public void prevStage()
    {
        if (currentStage > 1)
            currentStage--;
    }
}
