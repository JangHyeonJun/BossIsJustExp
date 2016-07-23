using UnityEngine;
using System.Collections;

public class Player_Control : MonoBehaviour
{
    Manager mng;

    public enum PlayerMove { STOP, LEFT, RIGHT };

    public int Player_HP = 10;
    public int Player_Atk = 10;
    public float speed;
    public float speed_jump;
    public float attack_speed;
    public GameObject notclearButton;

    float attack_timer = 5.0f;
    PlayerMove PM;
    bool grounded;
    bool boss_on = false;
    bool attack_ok = true;

    int jump_count;

    Transform Ground_check_c, Ground_check_l, Ground_check_r;
    Collider2D[] ground_col;
    Animator anim;
    Rigidbody2D rigid;
    GameObject effectObj;

    Transform boss_trans; // 보스 몬스터 좌표 확인해서 카메라 조정
   
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Boss" && Player_HP > 0) // 이 오브젝트의 콜라이더안에 들어온 Trigger의 tag가 Boss라면 피격 애니메이션을 보여주고 HP를 10깎는다.
        {
            Debug.Log("1");
            if (anim.GetBool("GuardOn"))
                Player_HP -= 1;
            else
                Player_HP -= 2;
            anim.SetTrigger("Damaged");
        }

        if (col.collider.tag == "SideWall")
        {
            anim.SetTrigger("Wallride");
            jump_count = 0;
        }

        if(col.collider.name == "BigdiskFire_L(Clone)" || col.collider.name == "BigdiskFire_R(Clone)")
        {
            Player_HP -= 2;
            anim.SetTrigger("Damaged");
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Wallride"))
            anim.SetTrigger("Jump");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            if (anim.GetBool("GuardOn"))
                Player_HP -= 1;
            else
            {
                if (col.tag == "Helmet1Col")
                {
                    Player_HP -= 2;
                    anim.SetTrigger("Damaged");
                }
                if (col.tag == "Helmet2Col")
                {
                    Player_HP -= 2;
                    anim.SetTrigger("Damaged");
                }
                if (col.tag == "FireBall")
                {
                    Player_HP -= 5;
                    anim.SetTrigger("Damaged");
                }
                if (col.tag == "BigFireBall")
                {
                    Player_HP -= 10;
                    anim.SetTrigger("Damaged");
                }

                if (col.tag == "Laser")
                {
                    Player_HP -= 4;
                    anim.SetTrigger("Damaged");
                }
                if (col.tag == "AquaBall")
                {
                    Player_HP -= 4;
                    anim.SetTrigger("Damaged");
                }
                if(col.name == "disk1" || col.name == "disk2" || col.name == "diskFire(Clone)" || col.name == "diskFire")
                {
                    Player_HP -= 2;
                    anim.SetTrigger("Damaged");
                }
                if(col.name == "BigdiskFire_L" || col.name == "BigdiskFire_L(Clone)" || col.name == "BigdiskFire_R" || col.name == "BigdiskFire_R(Clone)")
                {
                    Player_HP -= 5;
                    anim.SetTrigger("Damaged");
                }
                if (col.name == "suicideBombing")
                {
                    Player_HP -= 10;
                    anim.SetTrigger("Damaged");
                }
                if (col.name == "ClownBoy3_Bullet_explode")
                {
                    Player_HP -= 10;
                    anim.SetTrigger("Damaged");
                }
                

            }
        }
    }

    void ground_Check()
    {
        if (rigid.velocity.y <= 0)
        {
            //접지 체크.
            ground_col = Physics2D.OverlapPointAll(Ground_check_c.position);
            foreach (Collider2D g_col in ground_col)
                if (g_col != null && !grounded)
                {
                    jump_count = 0;
                    grounded = true;
                    anim.ResetTrigger("Jump");
                    anim.SetTrigger("Grounded");
                    Debug.Log("땅이다 땅");
                }
            ground_col = Physics2D.OverlapPointAll(Ground_check_l.position);
            foreach (Collider2D g_col in ground_col)
                if (g_col != null && !grounded)
                {
                    jump_count = 0;
                    grounded = true;
                    anim.SetTrigger("Grounded");
                    Debug.Log("땅이다 땅");
                }
            ground_col = Physics2D.OverlapPointAll(Ground_check_r.position);
            foreach (Collider2D g_col in ground_col)
                if (g_col != null && !grounded)
                {
                    jump_count = 0;
                    grounded = true;
                    anim.SetTrigger("Grounded");
                    Debug.Log("땅이다 땅");
                }
        }
    }
    void player_Death()
    {
        notclearButton.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Player_Jump()
    {
        if (jump_count < 2)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, speed_jump);
            jump_count++;
            grounded = false;
            anim.ResetTrigger("Stop");
            anim.ResetTrigger("Grounded");
            anim.SetTrigger("Jump");
            Debug.Log("점프!");
            Debug.Log(grounded);
        }
    }
    public void Player_Attack()
    {
        if (attack_ok == true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                anim.SetTrigger("Jumpattack");
            else
                anim.SetTrigger("Attack");
            Debug.Log("공격");

            attack_ok = false;
            attack_timer = 5.0f;
        }
    }
    public void Player_Attack_Sound()
    {
        GetComponent<AudioSource>().Play();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////// 스킬 구현부
    //가드
    public void Player_Guard()
    {
        anim.SetBool("GuardOn", true);
    }
    public void Player_GuardOff()
    {
        anim.SetBool("GuardOn", false);
    }

    //텔레포트
    public void Player_TeleportOn()
    {
        effectObj.SetActive(true);
        if (PM == PlayerMove.LEFT || PM == PlayerMove.RIGHT)
        {
            effectObj.SetActive(true);
            if (PM == PlayerMove.LEFT)
            {
                transform.position = transform.position - new Vector3(5.0f, 0,0);
            }
            else if (PM == PlayerMove.RIGHT)
            {
                transform.position = transform.position - new Vector3(-5.0f, 0, 0);
            }
        }
        else
            return;
    }
    public void Player_TeleportOff()
    {
        effectObj.SetActive(false);
    }

    // 공간왜곡
    public void Player_WarpOn()
    {
        effectObj.SetActive(true);
        GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.5f);
        gameObject.layer = 12;
    }
    public void Player_WarpOff()
    {
        effectObj.SetActive(false);
        GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color + new Color32(0, 0, 0, 250);
        gameObject.layer = 8;
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Player_Move_Input(int PM_Input)
    {
        PM = (PlayerMove)PM_Input;
    }
    void Player_LeftMove()
    {
        if (PM == PlayerMove.LEFT && !anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            // 이동구현
            transform.Translate(-1 * speed * 0.01f, 0, 0);
            // 플레이어 걸어가는 스프라이트 지정 .
            transform.localScale = new Vector3(-1,
                                                    transform.localScale.y, transform.localScale.z);
            anim.ResetTrigger("Stop");
            anim.SetTrigger("Run");
        }

    }
    void Player_RightMove()
    {
        if (PM == PlayerMove.RIGHT && !anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            // 이동구현
            transform.Translate(1 * speed * 0.01f, 0, 0);
            // 플레이어 걸어가는 스프라이트 지정 .
            transform.localScale = new Vector3(1,
                                                    transform.localScale.y, transform.localScale.z);
            anim.ResetTrigger("Stop");
            anim.SetTrigger("Run");
        }
    }
    void Player_Stop()
    {
        if (PM == PlayerMove.STOP && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            anim.ResetTrigger("Run");
            anim.SetTrigger("Stop");
        }
    }
    public void playerSync()
    {
        if (Player_Atk != mng.playerAtk)
            Player_Atk = mng.playerAtk;
        if (Player_HP != mng.playerHp)
            Player_HP = mng.playerHp;
        if (speed != mng.playerSpd)
            speed = mng.playerSpd;
    }

    


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Start, Update
    void Start()
    {

        //변수 초기화.
        mng = GameObject.Find("GameManager").GetComponent<Manager>();
        PM = PlayerMove.STOP;
        grounded = false;
        jump_count = 1;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        effectObj = GameObject.Find("Effect");
        effectObj.SetActive(false);
        Ground_check_c = transform.Find("Ground_check_C");
        Ground_check_l = transform.Find("Ground_check_L");
        Ground_check_r = transform.Find("Ground_check_R");
        playerSync();
        
    }

    void FixedUpdate()
    {
        ground_Check();
        Player_Stop();
        Player_LeftMove();
        Player_RightMove();

        //벽타기시 중력 약하게
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Wallride"))
            rigid.velocity = new Vector2(0.0f, -2.0f);
    }

    void Update()
    {
        
        
        //플레이어 공격 딜레이 체크용 코드
        if (attack_timer >0 && 
            !(anim.GetCurrentAnimatorStateInfo(0).IsName("PixelCharAnim_Sword_quickAtk")
              || anim.GetCurrentAnimatorStateInfo(0).IsName("PixelCharAnim_Sword_jumpAtk")))
        {
            attack_timer -= 0.1f * Time.deltaTime *attack_speed;
        }

        if (attack_timer <= 0)
            attack_ok = true;

        //Debug.Log("boss_name: "+ GameObject.FindWithTag("Boss").name);
        // 보스가 검색될때까지 계속 Find
        if (!boss_on)
            try
            {
                boss_trans = GameObject.FindWithTag("Boss").GetComponent<Transform>();
            }
            catch { }
        
        if (boss_trans)
            boss_on = true;
        else
            boss_on = false;
        //////////////////////////////////////////  디버깅용 (컴퓨터로도 점프,공격 가능하게만들려고)
        if (Input.GetButtonDown("Jump"))
        {
            Player_Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Player_Attack();
        }

        // 카메라이동

        // 카메라 오토 줌 인&아웃
        if (boss_on && boss_trans.name != "ClownBoy1" && boss_trans.name != "ClownBoy1(Clone)" && boss_trans.name != "Attack") // clownboy1은 화면 커졌다작아졌다하면 어지러워서 그냥 고정시킴
        {

            if (Mathf.Abs(boss_trans.position.x - transform.position.x) < 20.0f)
            {
                Camera.main.orthographicSize = 5 + Mathf.Abs(boss_trans.position.x - transform.position.x) * 0.25f;
                Camera.main.transform.position = new Vector3(transform.position.x, 2.7f + Mathf.Abs(boss_trans.position.x - transform.position.x) * 0.2f, -1f);

            }
            else
                Camera.main.transform.position = new Vector3(this.transform.position.x, 6.7f, -1f);
        }
        else
        {
            Camera.main.orthographicSize = 10;
            Camera.main.transform.position = new Vector3(this.transform.position.x, 6.7f, -1f);
        }


        if (Player_HP <= 0 || transform.position.y < -20)
        {
            anim.SetTrigger("Death");
        }
    }


    /// <summary>
    /// 실행
    /// </summary>

}
