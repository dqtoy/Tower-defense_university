using UnityEngine;
using System.Collections;
using UnityEngine.UI;


using System.IO;
using LitJson;


public class Home_Boss : MonoBehaviour {
    public static Home_Boss instance;
   
    Slider mybossslider;
    float bossallhp=500;//老窝总血量
    float temphp;//目前血量
 
     bool isGameOver = false;//有熊是否失败
    bool iswinshow = false;//是否显示了胜利

   
    //单例类
    void Awake()
    {
        Time.timeScale = 1;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

    }
    // Use this for initialization
    void Start () {
        mybossslider = GameObject.Find("FloodSlider").GetComponent<Slider>();
        temphp = bossallhp;
        HPupdate(0);//初始时更新血量

    }
	
	// Update is called once per frame
	void Update () {
        if (Enemyspawner.CountEnemyAlive == 0 && Enemyspawner.dijiboenemy == 4&&isGameOver==false&& iswinshow==false)
        {
            //最后一波敌人都消灭了
            ShowEndPanel("游戏 胜利!");
            int a = Random.Range(14, 16);
            AudioCtrl_Startgame.Instance.BGaudioMute(true);//停止背景音播放  :静音 但是音量不变
            AudioCtrl_Startgame.Instance.Otheraudioplay(a);//胜利音效
            iswinshow=true;

            //开启下一个关卡  地图2的锁被打开  只有是在游戏关卡1时才会有写入操作
            if (GameObject.Find("GameScene") != null)
            {//第一关
                MaptwoIstrue();
            }
          
            return;

        }
    }

    /// <summary>
    /// 对第二个关卡进行设置
    /// </summary>
    void MaptwoIstrue()
    {
        string filepath = Application.dataPath + @"/Resources/Mapdata.txt";

        if (File.Exists(filepath))
        {//判断txt存在
    
            //直接写入数据将关卡2置为false
            FileInfo file = new FileInfo(filepath);
            StreamWriter str = file.CreateText();
            Datamapclosed _mapdata;//关卡2的数据类对象
            _mapdata = new Datamapclosed();
            _mapdata.maptwoisclosed = true;
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
    /// 更新血量
    /// </summary>
    public void HPupdate(float addhp)
    {
        temphp += addhp;
        mybossslider.value = temphp / bossallhp;
        if (mybossslider.value == 0 && isGameOver == false)
        {
            //血量为0 游戏结束  停止生成  游戏暂停
            isGameOver = true;
            AudioCtrl_Startgame.Instance.BGaudioMute(true);//停止背景音播放  :静音 但是音量不变
            int a = Random.Range(8,10);
            AudioCtrl_Startgame.Instance.Otheraudioplay(a);//失败音效
            ShowEndPanel("游戏 失败!");
            return;

        }
    }
    //游戏结束UI显示
    void ShowEndPanel(string  textshow )
    {
        GameUIManager.GameUIInstance.EndPanel.SetActive(true);//显示结束面板
        GameUIManager.GameUIInstance.EndPanel.transform.FindChild("ShowText").GetComponent<Text>().text = textshow;
        Invoke("Timepause", 0.8f);

    }
    void Timepause()
    {

        Time.timeScale = 0;
    }

}
