using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 设置面板
/// </summary>
public class Settingpanel_ctrl : MonoBehaviour {
    Slider bgaudioslider;//背景音效slider
    Slider otheraudioslider;//其他音效slider
    Button bgaudiobtn;//背景音效有声音显示图标按钮
    Button otheraudiobtn;//其他音效有声音图标按钮
    void Start()
    {
        bgaudioslider = transform.FindChild("bgaudio/bgaudioSlider").GetComponent<Slider>();
        otheraudioslider = transform.FindChild("otheraudio/otheraudioSlider").GetComponent<Slider>();
        bgaudiobtn = transform.FindChild("bgaudio/bgaudioImagebtn").gameObject.GetComponent<Button>();
        otheraudiobtn=transform.FindChild("otheraudio/otheraudioImagebtn").gameObject.GetComponent<Button>();
        /**********初始化数据判断*****************/
        bgaudioslider.value = AudioCtrl_Startgame.Instance.bgaudiovolume;
        otheraudioslider.value= AudioCtrl_Startgame.Instance.otheraudiovolume;
        if (AudioCtrl_Startgame.Instance.bgaudioisnoplay)
        {//判断背景音效是否静音
            bgaudiobtn.interactable=false;
        }
        if (AudioCtrl_Startgame.Instance.otheraudioisnoplay)
        {
            otheraudiobtn.interactable = false;
        }

       

    }

    void Update()
    {//改变音量
        AudioCtrl_Startgame.Instance.BGaudioVolume(bgaudioslider.value);
        AudioCtrl_Startgame.Instance.OtheraudioVolume(otheraudioslider.value);
        if (AudioCtrl_Startgame.Instance.bgaudioisnoplay)
        {//是否静音判断  AudioCtrl_Startgame.Instance.bgaudioisnoplay=true时表示静音
            bgaudiobtn.interactable = false;
        }
        else
        {
            bgaudiobtn.interactable = true;
        }
        if (AudioCtrl_Startgame.Instance.otheraudioisnoplay)
        {
            otheraudiobtn.interactable = false;
        }
        else
        {
            otheraudiobtn.interactable = true;
        }


    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    public void EscBtnClick()
    {
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        Uictrl.Instance.Showpanel(0);

    }
    /// <summary>
    /// 静音按钮
    /// </summary>
    public void bgnoplaybtn()
    {
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        bgaudioslider.value = 0;
        AudioCtrl_Startgame.Instance.BGaudioVolume(bgaudioslider.value);
        bgaudiobtn.interactable = false;
       

    }
    /// <summary>
    /// 静音按钮
    /// </summary>
    public void othernoplaybtnClick()
    {
        otheraudiobtn.interactable = false;
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        otheraudioslider.value = 0;
        AudioCtrl_Startgame.Instance.OtheraudioVolume(otheraudioslider.value);
    }
}
