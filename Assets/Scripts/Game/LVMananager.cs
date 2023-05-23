using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class LVMananager : LogicManager<LVMananager> 
{
    private int score;
    bool isActiveSettingWindow = false;
    public int Score
    { 
        get { return score; }
        set
        {
            if(score != value)
            {
                score = value;
                EventManager.Instance.EventTrigger<int>("ScoreUpdate", score);
            }
        }
    }

    protected override void RegisterListener()
    {
        EventManager.Instance.Register("MonsterDie",OnMonsterDie);
    }

    private void OnMonsterDie()
    {
        ++Score;
    }

    protected override void UnRegisterListener()
    {
        EventManager.Instance.Unregister("MonsterDie", OnMonsterDie);
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.Show<GamePanel>();
        PlayerController.Instance.Init(ConfigManager.Instance.GetConfig<PlayerConfig>("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isActiveSettingWindow = !isActiveSettingWindow;
            if(isActiveSettingWindow)
            {
                Pause();
                UIManager.Instance.Show<SettingPanelInGame>();
            }
            else
            {
                UIManager.Instance.Close<SettingPanelInGame>();
                Continue();
                
            }
        }
    }
    private void Pause()
    {
        Time.timeScale = 0;   
    }
    private void Continue()
    {
        Time.timeScale = 1;
    }
}
