using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Guard_Event_Control : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
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

    public void OnPointerUp(PointerEventData eventData)
    {
        pl.GetComponent<Player_Control>().Player_GuardOff();
        // 여기가 터치 했다가 땟을때
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pl.GetComponent<Player_Control>().Player_Guard();
        // 여기가 터치 
    }
}
