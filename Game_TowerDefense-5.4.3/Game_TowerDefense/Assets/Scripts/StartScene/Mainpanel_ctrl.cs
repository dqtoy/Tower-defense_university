using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 游戏主界面
/// </summary>
public class Mainpanel_ctrl : MonoBehaviour {

   
	// Use this for initialization
	void Start () {
        AudioCtrl_Startgame.Instance.BGaudioplay(0);//播放背景音乐   
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    /***开始游戏  相关介绍   查看角色   设置按钮********/
    public void StartgameToMapBtnclick()
    {//开始游戏
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        Uictrl.Instance.Showpanel(2);
    }
    public void IntroduceBtnclick()
    {//相关介绍
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        Uictrl.Instance.Showpanel(3);
    }
    public void ViewbossBtnclick()
    {//查看角色
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        Uictrl.Instance.Showpanel(4);
    }
    public void SettingBtnclick()
    {//设置
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        Uictrl.Instance.Showpanel(1);
    }
}
