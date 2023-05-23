using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UIPanel("UI/SettingPanelInGame", 2, true)]
public class SettingPanelInGame : PanelBase
{
    private Slider musicSlider;
    private Slider musicEffectSlider;
    private Button okButton;
    private Button cancelButton;
    public override void Init()
    {
        base.Init();
        #region 获取控件
        musicSlider = transform.Find("musicSlider").GetComponent<Slider>();
        musicEffectSlider = transform.Find("musicEffectSlider").GetComponent<Slider>();
        okButton = transform.Find("okButton").GetComponent<Button>();
        cancelButton = transform.Find("cancelButton").GetComponent<Button>();
        #endregion
        okButton.onClick.AddListener(OnClickokbutton);
        cancelButton.onClick.AddListener(OnClickCancelButton);
    }

    private void OnClickCancelButton()
    {
        UIManager.Instance.Close<SettingPanelInGame>();
        Time.timeScale = 1;
    }

    private void OnClickokbutton()
    {
        
    }
}
