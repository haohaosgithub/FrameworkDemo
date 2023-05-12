using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UIPanel("UI/SettingPanel", 1, true)]
public class SettingPanel : PanelBase
{
    #region 控件
    private Slider musicSlider;
    private Slider musicEffectSlider;
    private Button okButton;
    private Button cancelButton;
    #endregion
    float musicVolume;
    float musicEffectVolume;
    public override void Init()
    {
        base.Init();
        #region 获取控件
        musicSlider = transform.Find("musicSlider").GetComponent<Slider>();
        musicEffectSlider = transform.Find("musicEffectSlider").GetComponent<Slider>();
        okButton = transform.Find("okButton").GetComponent<Button>();
        cancelButton = transform.Find("cancelButton").GetComponent<Button>();
        #endregion
        #region 监听面板控件事件
        musicSlider.onValueChanged.AddListener(OnValueChangeMusicSlider);
        musicEffectSlider.onValueChanged.AddListener(OnValueChangeMusicEffectSlider);
        okButton.onClick.AddListener(OnClickokbutton);
        cancelButton.onClick.AddListener(OnClickCancelButton);
        #endregion
        
    }

    private void OnClickCancelButton()
    {
        //重置成打开设置面板时的数值
        AudioManager.Instance.MusicVolume = musicVolume;
        AudioManager.Instance.EffectAudioVolume = musicEffectVolume;
        AudioManager.Instance.PlayOneShot("Audio/Button", GameRoot.Instance);

        this.Close();
    }

    private void OnClickokbutton()
    {
        AudioManager.Instance.PlayOneShot("Audio/Button", GameRoot.Instance);
        this.Close();
    }

    private void OnValueChangeMusicEffectSlider(float arg0)
    {
        AudioManager.Instance.EffectAudioVolume = arg0;
    }

    private void OnValueChangeMusicSlider(float arg0)
    {
        AudioManager.Instance.MusicVolume = arg0;
    }
    
    public override void OnShow()
    {
        musicVolume = AudioManager.Instance.MusicVolume;
        musicEffectVolume = AudioManager.Instance.EffectAudioVolume;
    }
}
