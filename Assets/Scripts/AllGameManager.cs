using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGameManager : SingletonMono<AllGameManager>
{
    ArchivingItem nowItem;
    UserData userData;
    

    public  void EnterGame(ArchivingItem item)
    {
        nowItem = item;
        userData = ArchivingManager.Instance.LoadArchiving<UserData>(item);
        EventManager.Instance.Register<int>("ScoreUpdate", OnScoreUpdate);
    }

    private void OnScoreUpdate(int score)
    {
        if(score > userData.score)
        {
            userData.score = score;
            ArchivingManager.Instance.SaveArchiving(userData, nowItem);
        }
    }
    private void OnDestroy()
    {
        EventManager.Instance.Unregister<int>("ScoreUpdate", OnScoreUpdate);
    }
}
