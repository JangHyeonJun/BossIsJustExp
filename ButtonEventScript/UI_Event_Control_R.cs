using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
public class UI_Event_Control_R : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    GameObject pl;
    bool pl_on = false;
	// Use this for initialization
	void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {

        if (!pl_on)
            pl = GameObject.Find("Player");
        else
            pl_on = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pl.GetComponent<Player_Control>().Player_Move_Input(0);
        // 여기가 터치 했다가 땟을때
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pl.GetComponent<Player_Control>().Player_Move_Input(2);
        // 여기가 터치 
    }
}
