using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Pool]
public class UI_ArchivingItem : MonoBehaviour
{
    #region 控件相关
    [SerializeField]
    private Button clickButton;
    [SerializeField]
    private Button delButton;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text scoreText;
    #endregion
    ArchivingItem nowItem; //当前ui_item所表示的存档

    public void Init(ArchivingItem item)
    {
        nowItem = item;
        #region 获取相关组件
        clickButton = GetComponent<Button>();
        delButton = transform.Find("delButton").GetComponent<Button>();
        nameText = transform.Find("nameText").GetComponent<Text>();
        timeText = transform.Find("timeText").GetComponent<Text>();
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        #endregion
        #region 按钮事件监听
        clickButton.onClick.AddListener(OnClickclickButton);
        delButton.onClick.AddListener(OnClickdelButton);
        #endregion
        #region 根据存档进行文本修改
        UserData userData =   ArchivingManager.Instance.LoadArchiving<UserData>(item);
        nameText.text = userData.username;
        timeText.text = item.LastUpdateTime.ToString();
        scoreText.text = userData.score.ToString();
        #endregion
    }

    private void OnClickdelButton()
    {
        ArchivingManager.Instance.DeleteArchivingItem(nowItem);
        EventManager.Instance.EventTrigger("UpdateArchivingItem");
        EventManager.Instance.EventTrigger<ArchivingItem> ("UpdateCurArchivingItem",null);
    }

    private void OnClickclickButton()
    {
        //print("click " + nowItem.archivingId);
        EventManager.Instance.EventTrigger<ArchivingItem>("UpdateCurArchivingItem", nowItem); 
    }

    public void Destroy()
    {
        nowItem = null;
        clickButton.onClick.RemoveAllListeners();
        delButton.onClick.RemoveAllListeners();
        PoolManager.Instance.PushGameObj(this.gameObject);
        //Destroy(gameObject);

    }
}
