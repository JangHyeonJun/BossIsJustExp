using UnityEngine;
using System.Collections;

public class Dragon2_Control : MonoBehaviour
{
    Manager mng;

    public int Speed;
    public float bossHP;
    public bool fire_left;
    public bool w_left;
    bool detect;
    bool attack_on = false;
    bool attacked = false;
    bool ready = false;
    bool hp_on = false;
    int damage_count = 0;
    float attack_timer = 0.0f;
    int pAtk;
    bool player_on = false;

    Transform Wall_check;
    Collider2D[] Wall_col;
    Animator anim;
    public GameObject firePrefab;
    public GameObject BigfirePrefab;
    Transform fireInit;
    Transform BigfireInit;
    public GameObject nextBoss; // 임시용
    public AudioClip fireSound;
    public AudioClip BigfireSound;

    UI_Control UIctrl;
    Player_Control playerCtrl; // 플레이어 오브젝트 참고용
    BossHPbar bossHPctrl;

    protected float distanceToPlayer = 0.0f;
    protected float distanceToPlayerPrev = 0.0f;


    

    void Start()
    {
        mng = GameObject.Find("GameManager").GetComponent<Manager>();
        UIctrl = GameObject.Find("UI_Play").GetComponent<UI_Control>(); // 플레이화면 UI 연동
        bossHPctrl = GameObject.Find("bossHP_2").GetComponent<BossHPbar>(); // 보스체력바 UI 연동
        fire_left = true;
        fireInit = transform.FindChild("FireInit");
        BigfireInit = transform.FindChild("BigFireInit");
        detect = false; 
        anim = GetComponent<Animator>();
        Wall_check = transform.Find("Wall_check");
        Walk_left();
    }

    void FixedUpdate()
    {

    }
    void Update()
    {

        //플레이어를 인식할때까지 계속 Find
        if (!player_on)
        {
            playerCtrl = GameObject.Find("Player").GetComponent<Player_Control>();
            pAtk = playerCtrl.Player_Atk;
            player_on = true;
        }

        if (!hp_on)
        {
            bossHPctrl.maxHP = bossHP;
            hp_on = true;
        }
        bossHPctrl.currentHP = bossHP; // 보스 체력바 현재체력 설정


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dragon_walk"))
        {
            attacked = false; // 1대만 맞게하려고 만든 부울변수

            if (w_left == true)
                transform.Translate(Speed * -0.1f * Time.deltaTime, 0.0f, 0.0f);
            else
                transform.Translate(Speed * 0.1f * Time.deltaTime, 0.0f, 0.0f);

            attack_on = false;
            if (!ready)
                attack_timer += Time.deltaTime;
        }

        if (attack_timer >= 2 && !ready)
        {
            ready = true;
            attack_timer = 0.0f;
        }
        if(damage_count > 5)
        {
            anim.SetBool("CountOver", true);
        }
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
        if (GetDistanePlayerX() < 20.0f)
            detect = true;
        else
            detect = false;

        if (detect)
        {
            if (Mathf.Abs(GetDistanePlayerX()) < 13.0f && Mathf.Abs(GetDistanePlayerY()) < 4.0f && ready )
            {

                if (Random.Range(1, 10) > 6 )
                {
                    anim.SetBool("Big", true);
                }
                else
                {
                    anim.SetBool("Big", false);
                }

                anim.SetTrigger("Fire");
            }
            else
            {
                    if (playerCtrl.transform.position.x > this.transform.position.x + 1.0f)
                    {
                        Walk_right();
                    }
                    else if (playerCtrl.transform.position.x < this.transform.position.x - 1.0f)
                        Walk_left();
            }
        }
        //사망 애니메이션 출력
        if (bossHP <= 0)
        {
            anim.SetTrigger("Dead");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dragon_Bigfire") || anim.GetCurrentAnimatorStateInfo(0).IsName("Dragon_fire"))
        {
            ready = false;
            damage_count = 0;
            anim.SetBool("CountOver", false);
            anim.ResetTrigger("Fire");
        }

    }

    void Fireball()
    {
        GameObject fire = Instantiate(firePrefab, fireInit.position, Quaternion.identity) as GameObject;
        GetComponent<AudioSource>().PlayOneShot(fireSound);
    }
    void BigFireball()
    {
        GameObject fire = Instantiate(BigfirePrefab, BigfireInit.position, Quaternion.identity) as GameObject;
        GetComponent<AudioSource>().PlayOneShot(BigfireSound);
    }

    void Walk_left()
    {
        anim.SetTrigger("Walk");
        w_left = true;
        transform.localScale = new Vector3(-1,
                                            transform.localScale.y, transform.localScale.z);

    }
    void Walk_right()
    {
        anim.SetTrigger("Walk");
        w_left = false;
        transform.localScale = new Vector3(1,
                                            transform.localScale.y, transform.localScale.z);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Attack" && bossHP > 0 && !attacked &&
            ( anim.GetCurrentAnimatorStateInfo(0).IsName("Dragon_idle")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("Dragon_walk"))) // 이 오브젝트의 콜라이더안에 들어온 Trigger의 tag가 Attack라면 피격 애니메이션을 보여주고 HP를 10깎는다.
        {
            Debug.Log("공격바듬");
            anim.SetTrigger("Damage");
            bossHP -= pAtk;
            attacked = true;
            damage_count++;
        }
    }

    //플레이어 넉백 처리.
    void OnCollisionEnter2D(Collision2D col)
    {
        
    }
    public void Dead() // 사망 애니메이션 마지막에 호출되는 이벤트 함수. 이 스크립트를 적용한 게임오브젝트를 씬안에서 제거한다.
    {
        UIctrl.BossClear();
        mng.clearBoss[5]++;
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
