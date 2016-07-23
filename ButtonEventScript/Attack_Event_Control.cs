using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Attack_Event_Control : MonoBehaviour,  IPointerDownHandler
{
    GameObject pl;
    bool pl_on = false;
    // Use this for initialization
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pl_on)
            pl = GameObject.Find("Player");
        else
            pl_on = true;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        pl.GetComponent<Player_Control>().Player_Attack();
        // 여기가 터치 
    }
}
