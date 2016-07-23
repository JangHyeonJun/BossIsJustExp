using UnityEngine;
using System.Collections;

public class Clown3_Summon_Control : MonoBehaviour
{
    public int Speed;
    public float bossHP;
    bool w_left;
    bool countOver = false;
    float countTime =0;


    Transform Wall_check;
    Collider2D[] Wall_col;
    Animator anim;

    public Player_Control playerCtrl; // 플레이어 오브젝트 참고용

    protected float distanceToPlayer = 0.0f;
    protected float distanceToPlayerPrev = 0.0f;

    public GameObject nextBoss; // 임시용
    public AudioClip fireSound;
    bool player_on = false;

    void Start()
    {

        anim = GetComponent<Animator>();
        Destroy(gameObject, 5.0f);
    }

    void FixedUpdate()
    {

    }
    void Update()
    {

        if(!countOver)
        countTime += Time.deltaTime;

        if (countTime > 1.0f)
            countOver = true;

        //플레이어를 인식할때까지 계속 Find
        if (!player_on)
        {
            playerCtrl = GameObject.Find("Player").GetComponent<Player_Control>();
            player_on = true;
        }

        // 몬스터의 좌, 우 이동과 공격
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("ClownBoy3_Summon_run"))
        {
            if (w_left == true)
                transform.Translate(Speed * -0.1f * Time.deltaTime, 0.0f, 0.0f);
            else
                transform.Translate(Speed * 0.1f * Time.deltaTime, 0.0f, 0.0f);
        }

        if (playerCtrl.transform.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1,
                                                    transform.localScale.y, transform.localScale.z);
            w_left = true;
        }
        else
        {
            transform.localScale = new Vector3(1,
                                                    transform.localScale.y, transform.localScale.z);
            w_left = false;
        }




    }

    public void SoundOn()
    {
        GetComponent<AudioSource>().PlayOneShot(fireSound);
    }
    public


void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Attack"
            && bossHP > 0
            && (anim.GetCurrentAnimatorStateInfo(0).IsName("ClownBoy3_Summon_run"))
            && countOver) // 이 오브젝트의 콜라이더안에 들어온 Trigger의 tag가 Attack라면 피격 애니메이션을 보여주고 HP를 10깎는다.
        {
            Debug.Log("공격바듬");
            anim.SetTrigger("Dead");
        }
    }

    void OnColliderEnter2D(Collider2D col)
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        
        if (col.gameObject.tag == "Player" && col.gameObject.tag != "Attack" )
        {
           
            anim.SetTrigger("Suicide");
        }
    }
    public void Dead() // 사망 애니메이션 마지막에 호출되는 이벤트 함수. 이 스크립트를 적용한 게임오브젝트를 씬안에서 제거한다.
    {
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
