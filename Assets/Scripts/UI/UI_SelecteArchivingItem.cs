using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Pool]
public class UI_SelecteArchivingItem : MonoBehaviour
{
    ArchivingItem nowItem; //当前选择的ui_item所表示的存档
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text scoreText;

    public void Init(ArchivingItem item)
    {
        nowItem = item;
        #region 获取相关组件
        startButton = transform.Find("startButton").GetComponent<Button>();
        nameText = transform.Find("nameText").GetComponent<Text>();
        timeText = transform.Find("timeText").GetComponent<Text>();
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        #endregion
        #region 按钮事件监听

        startButton.onClick.AddListener(OnClickstartButton);
        #endregion
        #region 根据存档进行文本修改
        UserData userData = ArchivingManager.Instance.LoadArchiving<UserData>(item);
        nameText.text = userData.username;
        timeText.text = item.LastUpdateTime.ToString();
        scoreText.text = userData.score.ToString();
        #endregion
    }

    private void OnClickstartButton()
    {
        //print("start");
        EventManager.Instance.EventTrigger<ArchivingItem>("EnterGame", nowItem);
    }

    public void Destroy()
    {
        nowItem = null;
        startButton.onClick.RemoveAllListeners();
        PoolManager.Instance.PushGameObj(this.gameObject);
    }
    
}
