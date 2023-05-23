using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using System;
using UnityEngine.SceneManagement;

public class MenuSceneManager : LogicManager<MenuSceneManager>
{
    private void Start()
    {
        AudioManager.Instance.PlayBGMusic("Audio/BG/Menu");
        UIManager.Instance.Show<MainMenuPanel>();
    }
    protected override void RegisterListener()
    {
        EventManager.Instance.Register<string>("CreateNewArchivingAndEnterGame", CreateNewArchivingAndEnterGame);
        EventManager.Instance.Register<ArchivingItem>("EnterGame", EnterGame);
    }
    protected override void UnRegisterListener()
    {
        EventManager.Instance.Unregister<string>("CreateNewArchivingAndEnterGame", CreateNewArchivingAndEnterGame);
        EventManager.Instance.Unregister<ArchivingItem>("EnterGame", EnterGame);
    }
    #region 该场景监听事件的处理
    private void CreateNewArchivingAndEnterGame(string name) //创建新存档并进入游戏
    {
        print("创建新存档");
        ArchivingItem archivingItem = ArchivingManager.Instance.CreateArchivingItem();
        UserData userData = new UserData(name, 0);
        ArchivingManager.Instance.SaveArchiving(userData, archivingItem);
        EventManager.Instance.EventTrigger("UpdateArchivingItem");
        EventManager.Instance.EventTrigger("UpdateRankData");
        print("进入游戏");
        EnterGame(archivingItem); //基于当前存档进入游戏

    }

    private void EnterGame(ArchivingItem item)
    {
        UserData userdata  = ArchivingManager.Instance.LoadArchiving<UserData>(item);
        //print(userdata.username + "    " + userdata.score);
        UIManager.Instance.CloseAll();
        //UIManager.Instance.Show<LoadingPanel>();
        //SceneMgr.Instance.LoadAsync("Game");
        AllGameManager.Instance.EnterGame(item);
        SceneManager.LoadSceneAsync("Game");

    }
    #endregion
    


}
