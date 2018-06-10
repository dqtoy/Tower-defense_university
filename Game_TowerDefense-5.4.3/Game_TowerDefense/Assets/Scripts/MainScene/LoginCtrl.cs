using UnityEngine;
using System.Collections;

using LitJson;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;//用于正则比对Regex.IsMatch（a，b）;

/// <summary>
/// 登录 注册   控制管理
/// </summary>
public class LoginCtrl : MonoBehaviour {

	public static LoginCtrl instance;//单例

	GameObject  Loading_widget;//跳转加载面板
	TweenScale  loadingtween;//加载弹出动画
	UILabel loadlabelshow;//加载提示框
	[HideInInspector]//隐藏不在 unity控制台显示
	public	string loadlabelinput;//跳转家在等待面板 时现实的文字
	bool isbtn=false;//禁用按钮

    string filepath;//用户注册信息 --文件路径
    string path;//Resources下的路径信息

    TweenPosition Logintween;//登录tween动画
    GameObject login_widget;//登录管理工具
    UILabel LoginErrortipshow;//登陆错误提示框
    TweenScale Loginerrorlabeltween;//错误提示Label的tween动画
    UIInput loginaccount;//登录账号
    UIInput loginpassword;//登录密码

    TweenPosition Registtween;//注册tween动画


	modellist _logModelList;//用户模型

	void Awake()
	{//单例
		if (instance == null) {
			instance = this;
		} else {
		
			Destroy (instance);
		}
	
	}

    // Use this for initialization
    void Start () {
        filepath = Application.dataPath + @"/Resources/Userdata.txt";// 账号数据路径
       
        //实例化  过程
        login_widget = transform.Find("login").gameObject;     
        Logintween = login_widget.GetComponent<TweenPosition>();
        LoginErrortipshow = login_widget.transform.FindChild("Errorlabelshow").GetComponent<UILabel>();
        Loginerrorlabeltween = LoginErrortipshow.gameObject.GetComponent<TweenScale>();
        loginaccount = login_widget.transform.FindChild("Account_InputField").GetComponent<UIInput>();
        loginpassword = login_widget.transform.FindChild("Password_InputField").GetComponent<UIInput>();

        Registtween = transform.Find("regist").gameObject.GetComponent<TweenPosition>();

		//
		Loading_widget=transform.Find("Loading").gameObject;
		loadingtween = Loading_widget.GetComponentInChildren<TweenScale> ();
		loadlabelshow= loadingtween.gameObject.GetComponentInChildren<UILabel> ();
    }

    /*************登录*******************/
    //登陆完成
    public void LoginOKbtnOnClick()
    {
		
		StreamReader readstr;
       
		if (LoginErrortipshow.text != string.Empty||isbtn==true)
        {
            return;
        }

     
      
        //账号  密码空判断
        if (loginaccount.value==string.Empty|| loginpassword.value==string.Empty)
        {
            Loginshowerrorlabel("￣へ￣输入不能为空！");
            AudioCtrl.instance.Play(0);//播放错误提示音效
        }
        else
        {
            string account_tel = "0?(13|14|15|17|18|19)[0-9]{9}";//手机号正则 用于账号
            string password_zuhe = "^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{12,16}$";//密码正则判断
            if (Regex.IsMatch(loginaccount.value, account_tel))
            {//账号是否为手机号  合规
                if (Regex.IsMatch(loginpassword.value, password_zuhe))
                {//密码合规
                 //判断账号是否存在  密码是否正确
                    if (File.Exists(filepath))
                    {//txt文本存在						
                        readstr = new StreamReader(filepath);
                        if (readstr.Peek() > -1)
                        {//文本不是空文本
                         //不加密写法
                         //TextAsset asset = Resources.Load<TextAsset> ("userdata");
                         //_logModelList = JsonMapper.ToObject<modellist> (asset.text);
                         /**********加密写法  解密读取数据**********/
                         //解密方法
                            string _data = readstr.ReadToEnd();//读取字符串数据
                            _logModelList = JsonMapper.ToObject<modellist>(Des_data.DecryptDES(_data));////解密  json去读数据


                            for (int i = 0; i < _logModelList.datalis.Count; i++)
                            {
                                if (_logModelList.datalis[i].account == loginaccount.value)
                                {
                                    if (_logModelList.datalis[i].password == loginpassword.value)
                                    {
                                        //输入账号密码正确 跳转加载。。。
                                        loadlabelinput = "\"(￣▽￣)~* 登陆成功！";
                                        OnLoadTween();
                                        AudioCtrl.instance.Play(1);//播放转换加载音效
                                        isbtn = true;//禁用按钮
                                        break;
                                    }
                                    else
                                    {

                                        Loginshowerrorlabel("￣へ￣密码错误！");
                                        AudioCtrl.instance.Play(0);//播放错误提示音效
                                        break;
                                    }


                                }
                                else if (i == _logModelList.datalis.Count - 1)
                                {
                                    //没有找到账号
                                    Loginshowerrorlabel("￣へ￣用户不存在！");
                                    AudioCtrl.instance.Play(0);//播放错误提示音效
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //txt为空文本
                            Loginshowerrorlabel("￣へ￣用户不存在  注册吧！");
                            AudioCtrl.instance.Play(0);//播放错误提示音效
                        }
                        readstr.Close();
                        readstr.Dispose();

                    }
                    else
                    {
                        //txt不存在
                        Loginshowerrorlabel("￣へ￣用户不存在  注册吧！");
                        AudioCtrl.instance.Play(0);//播放错误提示音效
                    }
                }
                else
                {
                    Loginshowerrorlabel("￣へ￣密码应该是字母数字组合 长度12-16！");
                    AudioCtrl.instance.Play(0);//播放错误提示音效
                }
            }
            else
            {//账号不是手机号
                Loginshowerrorlabel("￣へ￣账号应该为手机号！");
                AudioCtrl.instance.Play(0);//播放错误提示音效
            }

          
      }
		//清空输入框
		Clearinput ();
   }
    /// <summary>
    /// 显示错误提示 label
    /// </summary>
    /// <param name="errormessage"></param>
    void Loginshowerrorlabel(string errormessage)
    {
        LoginErrortipshow.text = errormessage;
        Loginerrorlabeltween.PlayForward();
        Invoke("loginerrorshow", 3f);
    }
    /// <summary>
    /// 隐藏提示  label
    /// </summary>
    void loginerrorshow()
    {
        LoginErrortipshow.text = string.Empty;
        Loginerrorlabeltween.PlayReverse();
    }
    /// <summary>
    /// 跳转到注册界面
    /// </summary>
    public void ToregistpanelbtnOnClick()
    {
        if (isbtn)
        {
            return;
        }
		Clearinput ();//清空输入
        Logintween.PlayForward();//切换界面
        Registtween.PlayForward();//切换界面

    }

	/// <summary>
	/// 清空输入框
	/// </summary>
	void Clearinput()
	{
		//清空输入框
		loginaccount.value=string.Empty;
		loginpassword.value=string.Empty;
	

	}

	/// <summary>
	/// 登陆成功  显示加载界面
	/// </summary>
	public	void   OnLoadTween()
	{
		loadingtween.PlayForward ();
		StartCoroutine ("loadlabel");
		Invoke ("GameScene",5f);//跳转
	}


	IEnumerator loadlabel()
	{
		while (true) {
			loadlabelshow.text =  loadlabelinput+ "\n" + "正在加载界面.";
			yield return new WaitForSeconds (1f);
			loadlabelshow.text = loadlabelinput + "\n" + "正在加载界面..";
			yield return new WaitForSeconds (1f);
			loadlabelshow.text = loadlabelinput + "\n" + "正在加载界面...";
			yield return new WaitForSeconds (1f);
		}
	}
	/// <summary>
	/// 跳转到游戏主界面
	/// </summary>
	void GameScene()
	{
	
		StopCoroutine ("loadlabel");
		SceneManager.LoadScene ("StartScene");
	}


}



