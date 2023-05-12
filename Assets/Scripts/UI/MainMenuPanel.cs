using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using UnityEngine.UI;
using System;

[UIPanel("UI/MainMenuPanel",0,true)]
public class MainMenuPanel : PanelBase
{
    private Button newGameButton;
    private Button continueGameButton;
    private Button rankButton;
    private Button settingGame;
    private Button quitGameButton;
    public override void Init()
    {
        base.Init();
        #region 获取面板上的相关组件
        newGameButton = transform.Find("newGameButton").GetComponent<Button>();
        continueGameButton = transform.Find("continueGameButton").GetComponent<Button>();
        rankButton = transform.Find("rankButton").GetComponent<Button>();
        settingGame = transform.Find("settingGame").GetComponent<Button>();
        quitGameButton = transform.Find("quitGameButton").GetComponent<Button>();
        #endregion
        #region 注册面板上相关事件(当前面板为各种按钮点击事件）
        newGameButton.onClick.AddListener(OnClicknewGameButton);
        continueGameButton.onClick.AddListener(OnClickcontinueGameButton);
        rankButton.onClick.AddListener(OnClickrankButton);
        settingGame.onClick.AddListener(OnClicksettingGame);
        quitGameButton.onClick.AddListener(OnClickquitGameButton);
        #endregion
    }
    #region 处理按钮事件
    private void OnClicknewGameButton()
    {
        AudioManager.Instance.PlayOneShot("Audio/Button",GameRoot.Instance);
        UIManager.Instance.Show<NewGamePanel>();
    }

    private void OnClickcontinueGameButton()
    {
        AudioManager.Instance.PlayOneShot("Audio/Button", GameRoot.Instance);
        UIManager.Instance.Show<ArchivingPanel>();
    }

    private void OnClickrankButton()
    {
        AudioManager.Instance.PlayOneShot("Audio/Button", GameRoot.Instance);
        UIManager.Instance.Show<RankPanel>();
    }

    private void OnClicksettingGame()
    {
        AudioManager.Instance.PlayOneShot("Audio/Button", GameRoot.Instance);
        UIManager.Instance.Show(typeof(SettingPanel));
    }

    private void OnClickquitGameButton()
    {
        AudioManager.Instance.PlayOneShot("Audio/Button", GameRoot.Instance);
    }
    #endregion


    public override void OnClose()
    {
        base.OnClose();
        
    }



}
