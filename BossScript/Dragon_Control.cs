using UnityEngine;
using System.Collections;

public class Dragon_Control : MonoBehaviour
{
    public int Speed;
    public float dragon_HP;
    bool w_left;
    bool detect;

    Transform Wall_check;
    Collider2D[] Wall_col;
    Animator anim;
    public UI_Control UIctrl;

    protected float distanceToPlayer = 0.0f;
    protected float distanceToPlayerPrev = 0.0f;


    public Player_Control playerCtrl; // 플레이어 오브젝트 참고용

    void Start()
    {
        detect = false;
        anim = GetComponent<Animator>();
        Wall_check = transform.Find("Wall_check");
        Walk_left();
    }

    void FixedUpdate()
    {
        // 몬스터의 좌, 우 이동
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dragon_walk"))
        {
            if (w_left == true)
                transform.Translate(Speed * -0.1f * Time.deltaTime, 0.0f, 0.0f);
            else
                transform.Translate(Speed * 0.1f * Time.deltaTime, 0.0f, 0.0f);
        }
    }
    void Update()
    {
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

        // 플레이어와의 X좌표를 비교하여 일정거리내에 플레이어가 있으면 플레이어를 감지함.
        if (Mathf.Abs(GetDistanePlayerX()) < 3.0f)
            detect = true;
        else
            detect = false;


        // 플레이어를 감지했을 경우 플레이어가 바로 근처에있다면 공격, 아니면 쫓아감.
        if (detect)
        {
            if (Mathf.Abs(GetDistanePlayer()) < 2.0f)
            {
                anim.SetTrigger("Attack");
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
        if (dragon_HP <= 0) 
        {
            anim.SetTrigger("Dead");
        }

        //
        

    }






    void Walk_left()
    {
        anim.SetTrigger("Walk");
        w_left = true;
        transform.localScale = new Vector3(3,
                                            transform.localScale.y, transform.localScale.z);
          
    }
    void Walk_right()
    {
        anim.SetTrigger("Walk");
        w_left = false;
        transform.localScale = new Vector3(-3,
                                            transform.localScale.y, transform.localScale.z);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Attack" && dragon_HP > 0) // 이 오브젝트의 콜라이더안에 들어온 Trigger의 tag가 Attack라면 피격 애니메이션을 보여주고 HP를 10깎는다.
        {
            Debug.Log("공격바듬");
            anim.SetTrigger("Damage");
            dragon_HP -= 10;
        }
    }
    public void Dead() // 사망 애니메이션 마지막에 호출되는 이벤트 함수. 이 스크립트를 적용한 게임오브젝트를 씬안에서 제거한다.
    {
        UIctrl.ScoreAdd(100);
        Destroy(this.gameObject);
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
