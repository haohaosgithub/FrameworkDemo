using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UIPanel("UI/RankPanel", 1, true)]
public class RankPanel : PanelBase
{
    private Transform content;
    private List<UI_RankItem> uiRankItemList; //排行榜列表
    private bool isNeedUpdateRankList = true; //是否需要更新存档列表
    private Button closeButton;

    public override void Init()
    {
        base.Init();
        uiRankItemList = new List<UI_RankItem>();
        #region 获取面板上的相关组件
        content = transform.Find("scrollView/viewport/content");
        closeButton = transform.Find("closeButton").GetComponent<Button>();
        #endregion
        content = transform.Find("scrollView/viewport/content");
        closeButton = transform.Find("closeButton").GetComponent<Button>();
        #region 注册面板上的按钮点击事件
        closeButton.onClick.AddListener(OnClickCloseButton);
        #endregion
    }

    public override void RegisterEventListener()
    {
        base.RegisterEventListener();
        EventManager.Instance.Register("UpdateRankData",NeedUpdateRankList);
    }

    private void NeedUpdateRankList()
    {
        isNeedUpdateRankList = true;
    }

    private void UpdateRankItemList()
    {
       
        //删除所有旧ui_item
        for (int i = 0; i < uiRankItemList.Count; i++)
        {
            uiRankItemList[i].Destroy();
        }
        uiRankItemList.Clear();
        //根据存档数据增加ui_item
        //List<ArchivingItem> itemList = ArchivingManager.Instance.GetArchivingListByUpdateTime();
        List<ArchivingItem> itemList = ArchivingManager.Instance.GetArchivingListByCondition<int>(
            (archivingItem) => 
            {
                UserData userdata = ArchivingManager.Instance.LoadArchiving<UserData>(archivingItem);
                return userdata.score;
            },true
            );
        for (int i = 0; i < itemList.Count; i++)
        {
            UI_RankItem uiItem = ResManager.Instance.Load<UI_RankItem>("UI/UI_RankItem", content);
            uiItem.Init(itemList[i],i);
            uiRankItemList.Add(uiItem);
        }
        

        isNeedUpdateRankList = false;
    }

    public override void OnShow()
    {
        base.OnShow();
        if (isNeedUpdateRankList)
        {
            UpdateRankItemList();
        }

    }

    private void OnClickCloseButton()
    {
        AudioManager.Instance.PlayOneShot("Audio/Button", GameRoot.Instance);
        this.Close();
    }
}
