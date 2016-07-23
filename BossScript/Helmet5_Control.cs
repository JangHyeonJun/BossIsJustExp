using UnityEngine;
using System.Collections;

public class Helmet5_Control : MonoBehaviour {

    Manager mng;

    public int Speed;
    public float bossHP;
    bool w_left;
    bool ready = false;  // 보스가 3초이상 걸어다니면 공격준비 완료가 되도록 하는 bool변수.
    bool cool_ready = true; // 보스가 강력한 공격을 한 후에 6초간 무방비가 되도록 하는 bool변수.
    bool detect;
    bool player_on = false;
    bool attack_on = false;
    bool hp_on = false;
    int pAtk;

    int damage_count = 0;  // 보스가 일정 횟수의 공격을 당하면 강제로 공격에 들어가는것을 위한 카운팅 변수
    float attack_timer = 0;  // 공격 사이의 쿨타임을 위한 타이머 변수.
    float cool_timer = 0; // 강력한 공격 후 6초를 세기위한 변수
    Transform Wall_check;
    Collider2D[] Wall_col;
    Animator anim;

    UI_Control UIctrl;
    Player_Control playerCtrl; // 플레이어 오브젝트 참고용
    BossHPbar bossHPctrl;

    protected float distanceToPlayer = 0.0f;
    protected float distanceToPlayerPrev = 0.0f;

    public GameObject nextBoss; // 임시용
    public GameObject disk;
    public GameObject Bigdisk_L;
    public GameObject Bigdisk_R;
    public GameObject disk1, disk2;
    public AudioClip atk1Sound;
    public AudioClip atk2Sound;

    void Start()
    {
        mng = GameObject.Find("GameManager").GetComponent<Manager>();
        UIctrl = GameObject.Find("UI_Play").GetComponent<UI_Control>(); // 플레이화면 UI 연동
        bossHPctrl = GameObject.Find("bossHP_2").GetComponent<BossHPbar>(); // 보스체력바 UI 연동
        detect = false;
        anim = GetComponent<Animator>();
        Wall_check = transform.Find("Wall_check"); // 벽 체크용 오브젝트 연동
        Walk_left();
    }

    void FixedUpdate()
    {

    }
    void Update()
    {
        if (!hp_on)
        {
            bossHPctrl.maxHP = bossHP;
            hp_on = true;
        }
        bossHPctrl.currentHP = bossHP; // 보스 현재체력 연동

        Debug.Log("cool_ready: " + cool_ready);
        // 몬스터의 좌, 우 이동과 공격
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("HelmetBoy5_run"))
        {
            if (w_left == true)
                transform.Translate(Speed * -0.1f * Time.deltaTime, 0.0f, 0.0f);
            else
                transform.Translate(Speed * 0.1f * Time.deltaTime, 0.0f, 0.0f);
        }

        //플레이어를 인식할때까지 계속 Find
        if (!player_on)
        {
            playerCtrl = GameObject.Find("Player").GetComponent<Player_Control>();
            pAtk = playerCtrl.Player_Atk;
            player_on = true;
        }

        // 사망체크욤 ㅎㅎ
        if (bossHP <= 0 || transform.position.y < -20)
            anim.SetTrigger("Dead");

        
        //벽을 체크하며, 몬스터가 좌, 우로 이동할때마다 스프라이트를 바꿔줌.
        Wall_col = Physics2D.OverlapPointAll(Wall_check.position);
        foreach (Collider2D w_col in Wall_col)
            if (w_col != null && w_col.tag != "Attack" && !detect) // 투사체는 벽으로 취급안함.
            {
                if (this.transform.localScale.x > 0)
                {
                    Walk_right();
                }
                else
                {
                    Walk_left();
                }
            }

        // 플레이어와의 X좌표를 비교하여 일정거리내에 플레이어가 있으면 플레이어를 쫓아감.
        if (GetDistanePlayerX() < 23.0f)
            detect = true;
        else
            detect = false;


        // 5대이상 맞으면 바로 공격모드로 상태전환
        if (damage_count > 10)
            anim.SetBool("CountOver", true);

        // 3초가 지나면 자동으로 공격
        if (attack_timer >= 3 && !ready)
        {
            ready = true;
            attack_timer = 0;
        }
        
        if(attack_timer < 3 && !ready)
        {
            attack_timer += Time.deltaTime;
        }
       
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("HelmetBoy5_run"))
        {
            attack_on = false;
        }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("HelmetBoy5_attack_1"))
        {
            anim.SetBool("CountOver", false);
            damage_count = 0;


            if (!attack_on)  // 공격모션일 경우 디스크(투사체) 생성
            {
                attack_on = true;
                StartCoroutine("firedisk");

            }

        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("HelmetBoy5_attack_2"))
        {
            anim.SetBool("CountOver", false);
            damage_count = 0;
            
            if (!attack_on)  // 공격모션일 경우 디스크(투사체) 생성
            {
                attack_on = true;
                Instantiate(Bigdisk_L, transform.position + new Vector3(-2.0f, 5.0f, 0), Quaternion.identity);
                Instantiate(Bigdisk_R, transform.position + new Vector3(2.0f, 5.0f, 0), Quaternion.identity);
                
                disk1.SetActive(false);
                disk2.SetActive(false);
                cool_timer = 0;
                cool_ready = false;
            }
        }

        if(!cool_ready)
        {
            cool_timer += Time.deltaTime;
            if(cool_timer>=6.0f)
            {
                disk1.SetActive(true);
                disk2.SetActive(true);
                cool_ready = true;
            }
        }
                // 플레이어를 감지하고 준비된 상태에서 일정거리안에 있으면 공격준비, 일정거리 밖이거나 준비가 안됐으면 그냥 걸어다님.
        if (detect)
        {
            if (GetDistanePlayerX() < 15.0f && cool_ready)
                anim.SetBool("Detect", true);
            else
                anim.SetBool("Detect", false);

            if (Mathf.Abs(GetDistanePlayerX()) <= 15.0f && ready)
            {
                
                switch (Random.Range(1, 6))
                {
                    case 1:
                        {
                            anim.SetTrigger("Attack2");
                            ready = false;
                        }
                        break;
                    default:
                        {
                            anim.SetTrigger("Attack1");
                            ready = false;
                        }
                        break;
                }
                
            }
            else if (playerCtrl.transform.position.x > this.transform.position.x + 6.0f)
            {
                Walk_right();
            }
            else if (playerCtrl.transform.position.x < this.transform.position.x - 6.0f)
                Walk_left();
        }
        

    }
    void Walk_left()
    {
        w_left = true;
        transform.localScale = new Vector3(-1,
                                            transform.localScale.y, transform.localScale.z);


    }
    void Walk_right()
    {
        w_left = false;
        transform.localScale = new Vector3(1,
                                            transform.localScale.y, transform.localScale.z);

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Attack"
            && damage_count <= 10
            && bossHP > 0
            && (anim.GetCurrentAnimatorStateInfo(0).IsName("HelmetBoy5_run"))) // 이 오브젝트의 콜라이더안에 들어온 Trigger의 tag가 Attack라면 피격 애니메이션을 보여주고 HP를 10깎는다.
        {
            Debug.Log("공격바듬");
            anim.SetTrigger("Damaged");
            damage_count += 1;
            bossHP -= pAtk;
        }

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
    public void playAtk1()
    {
        GetComponent<AudioSource>().PlayOneShot(atk1Sound);
    }
    public void playAtk2()
    {
        GetComponent<AudioSource>().PlayOneShot(atk2Sound);
    }

    void OnColliderEnter2D(Collider2D col)
    {

    }

    //플레이어 넉백 처리.
    void OnCollisionEnter2D(Collision2D col)
    {

    }

    public IEnumerator firedisk()
    {
        Instantiate(disk, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(disk, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(disk, transform.position, Quaternion.identity);
    }
    public void Dead() // 사망 애니메이션 마지막에 호출되는 이벤트 함수. 이 스크립트를 적용한 게임오브젝트를 씬안에서 제거한다.
    {
        UIctrl.BossClear();
        mng.clearBoss[4]++;
        mng.saveData();
        Destroy(this.gameObject);
        Instantiate(nextBoss, transform.position, Quaternion.identity);
    }


    // AI용 스크립트 ( 책에서 가져옴)
    public float GetDistanePlayer()
    {
        distanceToPlayerPrev = distanceToPlayer;
        distanceToPlayer = Vector3.Distance(transform.position, playerCtrl.transform.position);
        return distanceToPlayer;
    }

    public bool IsChangeDistanePlayer(float l)
    {
        return (Mathf.Abs(distanceToPlayer - distanceToPlayerPrev) > l);
    }

    public float GetDistanePlayerX()
    {
        Vector3 posA = transform.position;
        Vector3 posB = playerCtrl.transform.position;
        posA.y = 0; posA.z = 0;
        posB.y = 0; posB.z = 0;
        return Vector3.Distance(posA, posB);
    }

    public float GetDistanePlayerY()
    {
        Vector3 posA = transform.position;
        Vector3 posB = playerCtrl.transform.position;
        posA.x = 0; posA.z = 0;
        posB.x = 0; posB.z = 0;
        return Vector3.Distance(posA, posB);
    }
}
