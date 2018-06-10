using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Introducepanel_ctrl : MonoBehaviour {

    Text showtext;//显示text
    string show;
    bool isshow = false;//展示bool
    bool isbtnenabled = true;//按钮是否可以用
    // Use this for initialization
    void Start () {
        showtext = transform.FindChild("ShowText").GetComponent<Text>();
        show = "坚守城池是一款3D单机电脑塔防游戏。这款游戏玩法简单、功能完善，与保卫萝卜有一定相似度。不足之处是界面相对粗糙，地图简略，所以仍需改进。玩家可以及时反馈来帮助作者改进游戏！";
        showtext.text = string.Empty;
	}

    /// <summary>
    /// 展示介绍内容
    /// </summary>
    public void showtextbtnClick()
    {
        if (!isbtnenabled)
        {//按钮不可以用
            return;
        }
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        if (isshow)
        {
            showtext.text = string.Empty;
            isshow = false;
        }
        else
        {
            showtext.DOText(show, 10f);
            isshow = true;
            StartCoroutine("Falseshowtext");
        }
        
       

    }
    /// <summary>
    /// 返回按钮
    /// </summary>
    public void EscBtnclick()
    {
        if (!isbtnenabled)
        {//按钮不可以用
            return;
        }
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        isbtnenabled = true;
        isshow = false;
        showtext.text = string.Empty;
        
        Uictrl.Instance.Showpanel(0);

    }

    IEnumerator Falseshowtext()
    {//让按钮无效
        isbtnenabled = false;
        yield return new WaitForSeconds(10);
        isbtnenabled = true;
    }
}
