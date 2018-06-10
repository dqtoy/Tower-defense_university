using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.IO;
using LitJson;


public class Selectmappanel_ctrl : MonoBehaviour {
    
    GameObject Maptwobtn;//关卡2选择按钮
    Datamapclosed _mapdata;//数据类对象
    void Start()
    {
        Maptwobtn = transform.Find("maptwo_btn").gameObject;
        //对关卡2的状态进行存取
        MaptwoIstrue();    
    }
    /// <summary>
    /// 对第二个关卡进行设置
    /// </summary>
    void MaptwoIstrue()
    {
        
        string filepath = Application.dataPath + @"/Resources/Mapdata.txt";
        if (File.Exists(filepath))
        {//判断txt存在
            StreamReader readerstr;
            readerstr = new StreamReader(filepath);
          
            if (readerstr.Peek() > -1)
            {//文本不是空文本
             //解密方法
              
                string _data = readerstr.ReadToEnd();//读取字符串数据
                _mapdata = JsonMapper.ToObject<Datamapclosed>(Des_data.DecryptDES(_data));////解密  json去读数据
                if (_mapdata.maptwoisclosed==true)
                {//关卡2开启了
                    Maptwobtn.GetComponent<Button>().interactable = true;
                }
                else
                {
                    Maptwobtn.GetComponent<Button>().interactable = false;
                }
            }
            readerstr.Close();
            readerstr.Dispose();//释放流
          
        }
        else
        {
            Maptwobtn.GetComponent<Button>().interactable = false;
            //创建文本 并将maptwo的按钮表示为失效状态
            FileInfo file = new FileInfo(filepath);
            StreamWriter str = file.CreateText();
            _mapdata = new Datamapclosed();
            _mapdata.maptwoisclosed = false;
            /*****加密DES***********/
            //数据加密处理
            string myData;//字符串数据
            myData = JsonMapper.ToJson(_mapdata);
            str.Write(Des_data.EncryptDES(myData));
            str.Close();
            str.Dispose();
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
    /// 选择地图1 按钮
    /// </summary>
    public void MaponeBtnClick()
    {
        AudioCtrl_Startgame.Instance.Otheraudioplay(6);//按钮音效
        //切换异步加载界面
        Uictrl.Instance.asyncloadscene_name = "GameScene";
        Uictrl.Instance.Showpanel(5);
       //  SceneManager.LoadScene("GameScene");//游戏场景1
    }
    /// <summary>
     ///选择地图2 按钮
     /// </summary>
    public void MaptwoBtnClick()
    { 
        
        AudioCtrl_Startgame.Instance.Otheraudioplay(6);//按钮音效 
        //切换异步加载界面
        Uictrl.Instance.asyncloadscene_name = "GameScene_1";
        Uictrl.Instance.Showpanel(5);
        //SceneManager.LoadScene("GameScene_1");//游戏场景2

    }

}
