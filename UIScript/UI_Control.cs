using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Control : MonoBehaviour{
    Manager mng;

    public GameObject GuardButton;
    public GameObject teleportButton;
    public GameObject SpaceWarpButton;

    public TextAsset textFile;
    string[] dialogLines;
    int lineNumber;
    int Score = 0;
    public Canvas Canvas;
    public Text DlgText, ScoText;

    public GameObject clearButton;
    

    void Start()
    {
        mng = GameObject.Find("GameManager").GetComponent<Manager>();

        

        if (textFile)
        {
            dialogLines = (textFile.text.Split("\n"[0]));
        }
        ScoText.text = Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        changeSkill();


        if (lineNumber < 0)
        {
            lineNumber = 0;
        }
        string dialog = dialogLines[lineNumber];
        DlgText.text = dialog;
    }

    public void ScoreAdd(int sco)
    {

        Score += sco;
        ScoText.text = Score.ToString();
    }
    public void Next()
    {
        lineNumber += 1;
    }
    public void Back()
    {
        lineNumber -= 1;
    }
    public void BossClear()
    {
        clearButton.SetActive(true);
    }
    public void changeSkill()
    {
        if (mng.currentSkill == "방어")
        {
            GuardButton.SetActive(true);
            teleportButton.SetActive(false);
            SpaceWarpButton.SetActive(false);
        }
        if (mng.currentSkill == "순간이동")
        {
            GuardButton.SetActive(false);
            teleportButton.SetActive(true);
            SpaceWarpButton.SetActive(false);
        }
        if (mng.currentSkill == "공간왜곡")
        {
            GuardButton.SetActive(false);
            teleportButton.SetActive(false);
            SpaceWarpButton.SetActive(true);
        }
    }
}
