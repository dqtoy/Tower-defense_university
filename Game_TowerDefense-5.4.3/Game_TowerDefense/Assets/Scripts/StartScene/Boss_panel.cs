using UnityEngine;
using System.Collections;

public class Boss_panel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    /// <summary>
    /// 退出按钮
    /// </summary>
    public void EscBtnClick()
    {

        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        Uictrl.Instance.Showpanel(0);

    }


    /// <summary>
    /// 切换性别音频播放
    /// </summary>
    public void PlaySexchangeaudio()
    {
        AudioCtrl_Startgame.Instance.Otheraudioplay(0);//按钮音效

    }
}
