using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UIPanel("UI/NewGamePanel",1,true)]
public class NewGamePanel : PanelBase
{
    private Button newGameButton;
    private Button backMenuButton;
    private InputField inputField;

    public override void Init()
    {
        base.Init();
        #region 获取面板上的相关组件
        newGameButton = transform.Find("newGameButton").GetComponent<Button>();
        backMenuButton = transform.Find("backMenuButton").GetComponent<Button>();
        inputField = transform.Find("userName/inputField").GetComponent<InputField>();
        #endregion
        #region 注册面板上的按钮点击事件
        newGameButton.onClick.AddListener(OnClicknewGameButton);
        backMenuButton.onClick.AddListener(OnClickbackMenuButton);
        #endregion
    }
    #region 处理按钮事件
    private void OnClicknewGameButton()
    {
        AudioManager.Instance.PlayOneShot("Audio/Button", GameRoot.Instance);
        if(inputField.text.Length < 1)
        {
            UIManager.Instance.AddTips("昵称不合法，请重新输入");
        }
        else
        {
            EventManager.Instance.EventTrigger<string>("CreateNewArchivingAndEnterGame", inputField.text);
        }
        
    }

    private void OnClickbackMenuButton()
    {
        AudioManager.Instance.PlayOneShot("Audio/Button", GameRoot.Instance);
        inputField.text = "";//关闭之前清空输入框
        this.Close();
        //UIManager.Instance.Close<newGamePanel>();
        //UIManager.Instance.Show<mainMenuPanel>();
    }
    #endregion
}
