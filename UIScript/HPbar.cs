using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HPbar : MonoBehaviour {


    Player_Control pCtrl;
    public float maxHP, currentHP;
	// Use this for initialization
	void Start () {

        pCtrl = GameObject.Find("Player").GetComponent<Player_Control>();
        maxHP = pCtrl.Player_HP;
        currentHP = pCtrl.Player_HP;
    }
	
	// Update is called once per frame
	void Update () {

        currentHP = pCtrl.Player_HP;
        GetComponent<Image>().fillAmount = (currentHP / maxHP);
	}
}
