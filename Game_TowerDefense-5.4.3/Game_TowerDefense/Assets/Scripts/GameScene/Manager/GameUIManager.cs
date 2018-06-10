using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {
    Text Moneytext;//拥有的金币的Text UI
    Animator moneyAnimator;//金币不足时提示动画
    public static GameUIManager GameUIInstance;//单例模式
    //升级的UI Canvas 以及升级按钮 
   public  GameObject UpgradetowCanvas;
   public Button UpgradeButton;
    //获取升级canvas的动画组件
    Animator upgradeCanvasanimator;

    [HideInInspector]//选择升级的炮塔脚本信息 把选中的升级炮塔数据保存
    public  Towpoint towpoint;
    [HideInInspector]
    public GameObject showdijiboenemy;//第几波敌人展示text
    [HideInInspector]
    public  GameObject EndPanel;//结束面板
    [HideInInspector]
    public GameObject EsctipshowPanel;//返回主界面提示面板
    void Awake()
    {
        if (GameUIInstance == null)
        {
            GameUIInstance = this;
        }
        else
        {
            Destroy(GameUIInstance);
        }

    }

    // Use this for initialization
    void Start () {
       
        int a = Random.Range(3, 5);
        AudioCtrl_Startgame.Instance.BGaudioplay(a);//播放背景音乐  
        AudioCtrl_Startgame.Instance.BGaudioMute(false);//背景音播放  :不静音 但是音量不变

        showdijiboenemy = transform.Find("ShowenemydijiboText").gameObject;//实例化第几波敌人text 的gameObject
        Moneytext = transform.Find("Moneytext").GetComponent<Text>();//实例化text
        UIshow(DataManager.DataInstance.datamoney);//当游戏开始时显示之前用户金币拥有金币
        moneyAnimator = GameObject.Find("Canvas").transform.FindChild("Moneytext").GetComponent<Animator>();//绑定动画
        upgradeCanvasanimator = UpgradetowCanvas.GetComponent<Animator>();//获取升级canvas的动画组件

        EndPanel = transform.Find("EndPanel").gameObject;
        EsctipshowPanel= transform.Find("EscTipshowPanel").gameObject;
      
        
    }
	
	// Update is called once per frame
	void Update () {

        //点击ESC退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();//退出游戏
        }
       

	}
    /// <summary>
    /// 第几波敌人正在进攻
    /// </summary>
    public void showenemydijibotext(int num)
    {
        showdijiboenemy.SetActive(true);//显示第几波敌人text
        showdijiboenemy.GetComponent<Text>().text = "第" + (num) + "波敌人发起进攻";
        Invoke("Noshow",1.3f);
    }
    void Noshow()
    {//不展示第几波敌人的text
        showdijiboenemy.SetActive(false);//不显示第几波敌人text
    }

    /// <summary>
    /// UI显示  拥有的金币数量
    /// </summary>
    /// <param name="money"></param>
    public void UIshow( float money)
    {
        Moneytext.text =string.Format("¥:{0:F2}",money);
      

    }
    /// <summary>
    /// 提示动画效果
    /// </summary>
    public void TipAnimator()
    {
        //提示钱不够 动画
        AudioCtrl_Startgame.Instance.Otheraudioplay(11);//播放提示钱不够音效
        moneyAnimator.SetTrigger("MoneyAnimation");
    }


    /// <summary>
    /// 显示升级Canvas
    /// </summary>
    /// <param name="isDisabled"></param>
     public void ShowUpgrade(Vector3 pos,bool isDisabled=false)
    {
        StopCoroutine(HideUpgrade());
        UpgradetowCanvas.SetActive(false);
        UpgradetowCanvas.SetActive(true);//升级Canvas 显示出来
        UpgradetowCanvas.transform.position = pos;
      
        UpgradeButton.interactable = !isDisabled; // isDisabled为false时可以升级  为true时说明升级过了

    }

    /// <summary>
    /// 隐藏升级Canvas
    /// </summary>
    public void StartHide()
    {
        StartCoroutine(HideUpgrade());
    }
    IEnumerator HideUpgrade()
    {
        upgradeCanvasanimator.SetTrigger("HideupgradeUITrigger");//播放隐藏动画
        yield return new WaitForSeconds(0.8f);
        UpgradetowCanvas.SetActive(false);//升级Canvas 隐藏

    }
   
    /// <summary>
    /// 升级按钮
    /// </summary>
    public void OnUpgradeButtondown()
    {
       
        towpoint.UpgradeTow();
        StartHide();
    }
    /// <summary>
    /// 拆除按钮
    /// </summary>
    public void OnDeleteButtondown()
    {
        AudioCtrl_Startgame.Instance.Otheraudioplay(10);//播放拆除炮塔音效
        towpoint.DestroyTow();
        StartHide();
    }
    /********************游戏中点击返回按钮*****************************/
    /// <summary>
    /// 游戏中的退出键
    /// </summary>
    public void GameEscBtn()
    {
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音
        EsctipshowPanel.SetActive(true);//显示提示界面
        Time.timeScale = 0;

    }
    /// <summary>
    /// 确定返回主界面
    /// </summary>
    public void YesEscBtn()
    {
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
     
        SceneManager.LoadScene("StartScene");
        EsctipshowPanel.SetActive(false);//显示提示界面
        Destroy(AudioCtrl_Startgame.Instance.gameObject);//删除音效管理  防止加载游戏主场景重复生成出错
        Bossdata.Datadelete();//重新设置换装信息 不保存
        Time.timeScale = 1;
    }
    /// <summary>
     /// 不返回主界面
     /// </summary>
    public void NoEscBtn()
    {
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        EsctipshowPanel.SetActive(false);//显示提示界面
        Time.timeScale = 1;
    }

    //**********************结束界面 ***********************/
    public void OnRestartButton()
    {//重新开始按钮操作
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        //判断当前游戏在第几关 重新开始时还是本关卡
        if (GameObject.Find("GameScene")!=null)
        {//第一关
            SceneManager.LoadScene("GameScene");
        }
        else if (GameObject.Find("GameScene_1") != null)
        {//第二关
            SceneManager.LoadScene("GameScene_1");
        }


        Time.timeScale = 1;
        
    }

    public void OnEsctomainsceneButton()
    {//返回初始界面  
        AudioCtrl_Startgame.Instance.Otheraudioplay(4);//按钮音效
        SceneManager.LoadScene("StartScene");
        // AudioCtrl_Startgame.Instance.BGaudioMute(false);//恢复背景音播放  :不静音 但是音量不变
        Destroy(AudioCtrl_Startgame.Instance.gameObject);//删除音效管理  防止加载游戏主场景重复生成出错
        Bossdata.Datadelete();//重新设置换装信息 不保存
        Time.timeScale = 1;
    }

}
