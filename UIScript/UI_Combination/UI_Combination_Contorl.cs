using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_Combination_Contorl : MonoBehaviour {

    Manager mng;
    public GameObject selectedSkill;
    public int[] soulNum = new int[20];// 20개의 보스별 소울을 가지고 있는 횟수
    public int[] stat = new int[20]; // 0번은 추가된공격력, 1번은 추가된체력, 2번은 추가된이동속도
    public int[] ownSkill = new int[20]; // 20개의 스킬 중 아이템을 가지고있으면 해당 인덱스의 값이 1 아니면 0

    public GameObject[] checkSkill = new GameObject[4];
    public GameObject[] lockSkill = new GameObject[4];
    public Text[] soulNumtxt = new Text[4];
    public Text[] stattxt = new Text[3];
    public GameObject ErrorMsg;
    public GameObject ErrorMsg2;
    public bool clickOn = false;


    void Start () {

        mng = GameObject.Find("GameManager").GetComponent<Manager>();

	}
	
    void Update()
    {
        
        if (ownSkill != mng.ownSkill)
            ownSkill = mng.ownSkill;
        if (stat != mng.stat)
            stat = mng.stat;

        checksoulnum();
        checkstat();
        
        checkskill();
        checklockskill();
        
        
    }

    

    public void atkUP()
    {
        if (mng.soulNum[3] >= 1)
        {
            if (mng.stat[0] >= 5)
            {
                popErrorMsg2();
                return;
            }
            mng.soulNum[3]--;
            mng.stat[0]++;
        }
        else
            popErrorMsg();
    }
    public void defUP()
    {
        if (mng.soulNum[3] >= 1)
        {
            if (mng.stat[1] >= 5)
            {
                popErrorMsg2();
                return;
            }
            mng.soulNum[3]--;
            mng.stat[1]++;
        }
        else
            popErrorMsg();
    }
    public void movUP()
    {
        if (mng.soulNum[3] >= 1)
        {
            if (mng.stat[2] >= 5)
            {
                popErrorMsg2();
                return;
            }
            mng.soulNum[3]--;
            mng.stat[2]++;
        }
        else
            popErrorMsg();
    }
    public void destroyThis()
    {
        gameObject.SetActive(false);
    }


    // 최대 업그레이드 했을 경우 나오는 오류
    void popErrorMsg2()
    {
        ErrorMsg2.SetActive(true);
        StartCoroutine("unpopError2");
    }

    IEnumerator unpopError2()
    {
        yield return new WaitForSeconds(1.5f);
        ErrorMsg2.SetActive(false);
    }
    // 영혼의 갯수가 부족할 경우 나타나는 오류
    void popErrorMsg()
    {
        ErrorMsg.SetActive(true);
        StartCoroutine("unpopError");
    }

    IEnumerator unpopError()
    {
        yield return new WaitForSeconds(1.5f);
        ErrorMsg.SetActive(false);
    }

    //****************************************************//
    void checkstat()
    {
        for(int i=0; i<3; i++)
        {
            if (int.Parse(stattxt[i].text) != mng.stat[i])
                stattxt[i].text = mng.stat[i].ToString();
        }
    }

    void checksoulnum()
    {
        for(int i=0; i<4; i++)
        {
            if(int.Parse(soulNumtxt[i].text) != mng.soulNum[i])
                soulNumtxt[i].text = mng.soulNum[i].ToString();
        }
    }

    void checklockskill()
    {
        for(int i=0; i<4; i++)
        {
            if (ownSkill[i] == 1)
                lockSkill[i].SetActive(false);
        }
    }

    void checkskill()
    {
        if (clickOn)
        {
            switch (selectedSkill.name)
            {

                case "Guard_icon":
                    {
                        // 만약 소울의 갯수가 충분하면 잠금 해제
                        if (mng.ownSkill[0] != 1 && mng.soulNum[0] >= 3)
                        {
                            mng.soulNum[0] -= 3;
                            mng.ownSkill[0] = 1;
                            clickOn = false;
                            return;
                        }

                        // 만약 해당 스킬이 잠금 상태라면 체크를 할 수 없음.
                        if (lockSkill[0].activeSelf) 
                            return;
                        

                        // 해당 스킬을 제외하고 모두 체크 해제
                        for (int i = 0; i < 4; i++)
                        {
                            checkSkill[i].SetActive(false);
                        }
                        checkSkill[0].SetActive(true);

                        // 선택한 스킬을 적용시킴.
                        mng.currentSkill = "방어";
                    }
                    break;
                case "Teleport_icon":
                    {
                        // 만약 소울의 갯수가 충분하면 잠금 해제
                        if (mng.ownSkill[1] != 1 && mng.soulNum[0] >= 5 && mng.soulNum[1] >= 3)
                        {
                            mng.soulNum[0] -= 5;
                            mng.soulNum[1] -= 3;
                            mng.ownSkill[1] = 1;
                            clickOn = false;
                            return;
                        }

                        if (lockSkill[1].activeSelf)
                            return;

                        for (int i = 0; i < 4; i++)
                        {
                            checkSkill[i].SetActive(false);
                        }
                        checkSkill[1].SetActive(true);

                        mng.currentSkill = "순간이동";
                    }
                    break;
                case "Spacewarp_icon":
                    {
                        // 만약 소울의 갯수가 충분하면 잠금 해제
                        if (mng.ownSkill[2] != 1 && mng.soulNum[1] >= 5 && mng.soulNum[2] >= 5)
                        {
                            mng.soulNum[1] -= 5;
                            mng.soulNum[2] -= 5;
                            mng.ownSkill[2] = 1;
                            clickOn = false;
                            return;
                        }
                        if (lockSkill[2].activeSelf)
                            return;

                        for (int i = 0; i < 4; i++)
                        {
                            checkSkill[i].SetActive(false);
                        }
                        checkSkill[2].SetActive(true);

                        mng.currentSkill = "공간왜곡";
                    }
                    break;
                case "Gun_icon":
                    {
                        if (lockSkill[3].activeSelf)
                            return;

                        for (int i = 0; i < 4; i++)
                        {
                            checkSkill[i].SetActive(false);
                        }
                        checkSkill[3].SetActive(true);

                        mng.currentSkill = "마법탄환";
                    }
                    break;
                default:
                    break;
            }
            clickOn = false;
        }
    }
    public void setGameobject(GameObject obj)
    {
        selectedSkill = obj;
        clickOn = true;
    }

}
