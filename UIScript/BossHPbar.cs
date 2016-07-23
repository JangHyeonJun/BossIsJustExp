using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHPbar : MonoBehaviour {


    public float maxHP, currentHP;
    // Use this for initialization
    void Start()
    {

        maxHP = 1;
        currentHP = 1;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = (currentHP / maxHP);
    }
}
