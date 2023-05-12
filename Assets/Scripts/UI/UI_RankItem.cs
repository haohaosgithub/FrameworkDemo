using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RankItem : MonoBehaviour
{
    private Text nameText;
    private Text timeText;
    private Text scoreText;
    private Text rankText;
    public void Init(ArchivingItem item,int rankIndex)
    {
        #region 获取相关组件
        nameText = transform.Find("nameText").GetComponent<Text>();
        timeText = transform.Find("timeText").GetComponent<Text>();
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        rankText = transform.Find("rankText").GetComponent<Text>();
        #endregion
        #region 根据存档进行文本修改
        UserData userData = ArchivingManager.Instance.LoadArchiving<UserData>(item);
        nameText.text = userData.username;
        timeText.text = item.LastUpdateTime.ToString();
        scoreText.text = userData.score.ToString();
        rankText.text = (rankIndex + 1).ToString();
        #endregion
    }
    public void Destroy()
    {
        PoolManager.Instance.PushGameObj(this.gameObject);
    }
}
