using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UIPanel("UI/ArchivingPanel",1,true)]
public class ArchivingPanel : PanelBase
{
    private Transform content; 
    private List<UI_ArchivingItem> uiArchivingItemList; //存档列表
    private UI_SelecteArchivingItem ui_selItem;
    private bool isNeedUpdateArchivingList = true; //是否需要更新存档列表
    private ArchivingItem selItem = null;
    private Button closeButton;
    public override void Init()
    {
        base.Init();
        uiArchivingItemList = new List<UI_ArchivingItem>();
        #region 获取面板上的相关组件

        #endregion
        content = transform.Find("scrollView/viewport/content");
        closeButton = transform.Find("closeButton").GetComponent<Button>();
        #region 注册面板上的UI事件
        closeButton.onClick.AddListener(OnClickCloseButton);
        #endregion
    }

    public override void RegisterEventListener()
    {
        base.RegisterEventListener();

        //注意这个监听数据变化的事件是不能在关闭时注销监听的，因为即使关闭，我们也需要监听数据是否变化从而修改标志位
        EventManager.Instance.Register("UpdateArchivingItem", NeedUpdateArchivingItemList);
        //这个可以在关闭时注销，因为除了在当前面板可能会触发这个事件，其他外部不会修改
        EventManager.Instance.Register<ArchivingItem>("UpdateCurArchivingItem", NeedUpdateSelect);
    }

    //public override void UnRegisterEventListener()
    //{
    //    base.UnRegisterEventListener();
    //    EventManager.Instance.Unregister("UpdateArchivingItem", NeedUpdateArchivingItemList);
    //    EventManager.Instance.Unregister<ArchivingItem>("UpdateCurArchivingItem", NeedUpdateSelect);
    //}

    private void NeedUpdateSelect(ArchivingItem item)
    {
        if(selItem != item ) //产生数据变化
        {
            selItem = item;
            if(gameObject.activeSelf) //且当前面板就在存档面板时
            {
                UpdateSelect();
            }
        }
    }

    private void UpdateSelect()
    {
        if (selItem == null)
        {
            ui_selItem.Destroy();
            return;
        }
        if(ui_selItem != null)
        {
            ui_selItem.Destroy();
        }
        
        ui_selItem = ResManager.Instance.Load<UI_SelecteArchivingItem>("UI/UI_SelecteArchivingItem",this.transform);  
        ui_selItem.Init(selItem);

    }

    private void NeedUpdateArchivingItemList()
    {
        isNeedUpdateArchivingList = true;
        //如果当前就在此面板，直接更新
        //比如删除一个存档item时的情况
        if (gameObject.activeSelf) 
        {
            UpdateArchivingItemList();
        }
        
    }

    public void UpdateArchivingItemList()
    {
        #region 更新存档列表
        //删除所有旧ui_item
        for (int i = 0;i<uiArchivingItemList.Count;i++)
        {
            uiArchivingItemList[i].Destroy();
        }
        uiArchivingItemList.Clear();
        //根据存档数据增加ui_item
        List<ArchivingItem> itemList = ArchivingManager.Instance.GetArchivingListByUpdateTime();
        
        for (int i = 0; i < itemList.Count; i++)
        {
            UI_ArchivingItem uiItem = ResManager.Instance.Load<UI_ArchivingItem>("UI/UI_ArchivingItem", content);
            uiItem.Init(itemList[i]);
            uiArchivingItemList.Add(uiItem);
        }
        #endregion
        
        isNeedUpdateArchivingList = false;
    }
    public override void OnShow()
    {
        base.OnShow();
        if (isNeedUpdateArchivingList)
        {
            UpdateArchivingItemList();
        }

    }
    private void OnClickCloseButton()
    {
        AudioManager.Instance.PlayOneShot("Audio/Button", GameRoot.Instance);
        if (ui_selItem != null)
        {
            selItem = null;
            ui_selItem.Destroy();
        }
        this.Close();
    }
}
