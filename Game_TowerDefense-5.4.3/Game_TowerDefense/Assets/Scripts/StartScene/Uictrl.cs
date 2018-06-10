using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// UI控制  ：处理panel管理
/// </summary>
public class Uictrl : MonoBehaviour {
    public static Uictrl Instance;//单例
    GameObject[] panels;//存放所有界面
    [HideInInspector]
    public string asyncloadscene_name;//异步加载的场景名字
    void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

    }
    void Start()
    {   
        //panel数组实例化赋值
        panels = new GameObject[this.transform.childCount];
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i] = transform.GetChild(i).gameObject;
        }
        

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {//点击"ESC"退出
            Application.Quit();//退出游戏
        }


    }
    /// <summary>
    /// 界面显示 [0]mainpanel主界面[1]setting设置[2]selectmap开始游戏地图选择[3]introduce介绍[4]boss查看角色[5]异步加载
    /// </summary>
    /// <param name="index"></param>
    public void Showpanel(int index)
    {
        if (index == 0)
        {
            //主界面音效
            AudioCtrl_Startgame.Instance.BGaudioplay(0);//播放背景音乐   
        }
        else if (index == 2||index==5)
        {//选择地图  异步加载音效
            AudioCtrl_Startgame.Instance.BGaudioplay(1);//播放背景音乐  
        }
        else
        {//其他界面音效
            AudioCtrl_Startgame.Instance.BGaudioplay(2);//播放背景音乐   
        }
        for (int i = 0; i < panels.Length; i++)
        {
            if (i==index)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }

        }
        if (index==5)
        {//异步界面
            Asyncload_panel.instance.StartCoroutine("Onloadscene", asyncloadscene_name);//单例
        }

    }
    
   

}
