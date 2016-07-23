using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    public Transform somePerfab;
    public int[] clearBoss = new int[20]; // 20개의 보스별 클리어 횟수
    public int[] soulNum = new int[20];// 20개의 보스별 소울을 가지고 있는 횟수
    public int[] stat = new int[20]; // 0번은 추가된공격력, 1번은 추가된체력, 2번은 추가된이동속도
    public int[] ownSkill = new int[20]; // 20개의 스킬 중 아이템을 가지고있으면 해당 인덱스의 값이 1 아니면 0
    public string currentSkill; // 현재 착용중인 특수능력
    public int playerHp = 10;// 플레이어의 체력 최대치
    public int playerAtk = 10;// 플레이어의 공격력
    public float playerSpd = 10.0f; // 플레이어의 이동속도
 
    

    //건드리지마세요
    public static Manager ins;
    

    void Awake()
    {
        if(ins != null && ins != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            ins = this;
        }
    }
    //건드리지마세요, 싱글톤 하려고 써놓은 코드
    void Start()
    {

        loadData();
        int su = 0;
        su = PlayerPrefs.GetInt("su_test");
        su++;
        PlayerPrefs.SetInt("su_test", su);
        PlayerPrefs.Save();
        Debug.Log(su);

    }
    void Update()
    {
        upgradeStat();
    }


    void loadData()
    {
        clearBoss = decompData(PlayerPrefs.GetString("clearBoss"));
        soulNum = decompData(PlayerPrefs.GetString("soulNum"));
        stat = decompData(PlayerPrefs.GetString("stat"));
        ownSkill = decompData(PlayerPrefs.GetString("ownSkill"));
        currentSkill = PlayerPrefs.GetString("currentSkill");
    }
    public void saveData()
    {
        Debug.Log("세이브데스");
        PlayerPrefs.SetString("clearBoss", compData(clearBoss));
        PlayerPrefs.SetString("soulNum", compData(soulNum));
        PlayerPrefs.SetString("stat", compData(stat));
        PlayerPrefs.SetString("ownSkill", compData(ownSkill));
        PlayerPrefs.SetString("currentSkill", currentSkill);
        PlayerPrefs.Save();
    }
    public void initData()
    {
        for(int i=0; i<20; i++)
        {
            clearBoss[i] = 0;
            soulNum[i] = 0;
            stat[i] = 0;
            ownSkill[i] = 0;  
        }
        currentSkill = "";
        saveData();
    }
    public void develop()
    {
        for (int i = 0; i < 20; i++)
        {
            clearBoss[i] = 1;
            soulNum[i] = 55;
        }
    }
    void upgradeStat() //능력치 강화한 것을 체크해서 반영
    {
        if (stat[0] <= 4)  //공격력 동기화
            playerAtk = 10 + stat[0] * 2;
        else
            playerAtk = 22;

        if (stat[1] <= 4) // 체력 동기화
            playerHp = 10 + stat[1] * 2;
        else
            playerHp = 22;

        if (stat[2] <= 4) // 이동속도 동기화
            playerSpd = 10 + stat[2];
        else
            playerSpd = 17;
    }

    // int형 배열을 string 형으로 바꿔준다 '_'를 각 항을 구분한다
    string compData(int []data)
    {
        string cdata = "";
        for (int i =0; i< 20; i++)
        {
            cdata = cdata +"_" + data[i].ToString();
        }
        return cdata;
    }

    // string형 문자를 '_'로 구분하여서 int형 배열로 바꿔준다.
    int[] decompData(string data)
    {     
        string[] buf = new string[20];
        buf = data.Split('_');
        int[] dcdata = new int[20];

        for (int i=1; i<buf.Length; i++)
        {
            dcdata[i - 1] = int.Parse(buf[i]);
        }
        return dcdata;
    }
    // 데이터를 초기화 한다.
    void ResetData()
    {
        for(int i=0; i<20; i++)
        {
            clearBoss[i] = 0;
            soulNum[i] = 0;
            stat[i] = 0;
            ownSkill[i] = 0;
        }
    }
}
