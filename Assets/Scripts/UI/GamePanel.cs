using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UIPanel("UI/GamePanel", 1, true)]
public class GamePanel : PanelBase
{
    private Image imageFill;
    private Text textBulletNum;
    private Text textScore;
    public override void Init()
    {
        base.Init();
        imageFill = transform.Find("imageHPBar").Find("imageFill").GetComponent<Image>();
        textBulletNum = transform.Find("textBulletNum").GetComponent<Text>();
        textScore = transform.Find("textScore").GetComponent<Text>();

        EventManager.Instance.Register<int,int>("PlayerHPUpdate",UpdateimageFill);
        EventManager.Instance.Register<int, int>("BulletNumUpdate", UpdatetextBulletNum);
        EventManager.Instance.Register<int>("ScoreUpdate", UpdatetextScore);
    }

    private void UpdatetextScore(int obj)
    {
        textScore.text = obj.ToString();
    }

    private void UpdatetextBulletNum(int arg1, int arg2)
    {
        textBulletNum.text = arg1.ToString() + " / " + arg2.ToString(); ;
    }

    private void UpdateimageFill(int arg1, int arg2)
    {

        imageFill.fillAmount = (float)arg1 / (float)arg2;
    }
}
