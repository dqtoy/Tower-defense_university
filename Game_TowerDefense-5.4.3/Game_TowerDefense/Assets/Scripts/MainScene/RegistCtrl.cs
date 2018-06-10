using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System.Text.RegularExpressions;//用于正则比对Regex.IsMatch（a，b）;

using System.Collections.Generic;
//using UnityEditor;
/// <summary>
/// 账号数据模型
/// </summary>



public class RegistCtrl : MonoBehaviour {
	bool isbtn=false;//禁用按钮
    TweenPosition Logintween;//登录tween动画

    TweenPosition Registtween;//注册tween动画
    GameObject regist_widget;//注册管理工具
    UILabel RegistErrortipshow;//注册错误提示框
    TweenScale Registerrorlabeltween;//错误提示Label的tween动画
    UIInput registaccount;//注册账号
    UIInput registpassword;//注册密码
    UIInput registpasswordagain;//注册确认密码

    modellist _regModelList;//用户模型
    // Use this for initialization
    void Start () {

       
          //实例化  过程
        Logintween = transform.Find("login").gameObject.GetComponent<TweenPosition>();

        regist_widget = transform.Find("regist").gameObject;
        Registtween = regist_widget.GetComponent<TweenPosition>();
        RegistErrortipshow = regist_widget.transform.FindChild("Errorlabelshow").GetComponent<UILabel>();
        Registerrorlabeltween = RegistErrortipshow.gameObject.GetComponent<TweenScale>();
        registaccount = regist_widget.transform.FindChild("Account_InputField").GetComponent<UIInput>();
        registpassword = regist_widget.transform.FindChild("Password_InputField").GetComponent<UIInput>();
        registpasswordagain = regist_widget.transform.FindChild("Password_InputFieldagain").GetComponent<UIInput>();
    }


    /**********注册按钮*********/
    //注册完成
    public void RegistOKbtnOnClick()
    {
         bool iswrite = false;
        StreamReader readstr;
		if (RegistErrortipshow.text != string.Empty||isbtn==true)
        {
            return;
        }

      


        //判断账号  密码是否为空
        if (registaccount.value == string.Empty || registpassword.value == string.Empty || registpasswordagain.value == string.Empty)
        {//  输入为空
            Registshowerrorlabel("(╯﹏╰)b输入不能为空！");
            AudioCtrl.instance.Play(0);//播放错误提示音效
        }
        else
        {
            string  account_tel = "0?(13|14|15|17|18|19)[0-9]{9}";//手机号正则 用于账号
            string password_zuhe = "^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{12,16}$";//密码正则判断
            //Regex.IsMatch(registaccount.value, path);//返回bool值 比对两个值
            if (Regex.IsMatch(registaccount.value, account_tel))
            {//判断账号是否合规  为手机号
                if (Regex.IsMatch(registpassword.value, password_zuhe) && Regex.IsMatch(registpasswordagain.value, password_zuhe))
                {//密码合规  长度12-16  字母大小写、数字0-9组合
                 //密码一致性
                    if (registpassword.value != registpasswordagain.value)
                    {//密码不一致
                        Registshowerrorlabel("(；′⌒`)密码不一致！");
                        AudioCtrl.instance.Play(0);//播放错误提示音效
                    }
                    else
                    {
                        string filepath = Application.dataPath + @"/Resources/Userdata.txt";
                        if (File.Exists(filepath))
                        {


                            readstr = new StreamReader(filepath);
                            if (readstr.Peek() > -1)
                            {
                                //解密方法一
                                string _data = readstr.ReadToEnd();//读取字符串数据
                                _regModelList = JsonMapper.ToObject<modellist>(Des_data.DecryptDES(_data));////解密  json去读数据
                                                                                                           //不加密方法二
                                                                                                           //TextAsset asset = Resources.Load<TextAsset>("userdata");
                                                                                                           //_regModelList = JsonMapper.ToObject<modellist>(asset.text);
                                for (int i = 0; i < _regModelList.datalis.Count; i++)
                                {
                                    if (_regModelList.datalis[i].account == registaccount.value)
                                    {
                                        Registshowerrorlabel("(；′⌒`)账号已经存在！");
                                        AudioCtrl.instance.Play(0);//播放错误提示音效
                                        iswrite = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                _regModelList = new modellist();
                            }
                            readstr.Close();
                            readstr.Dispose();
                        }
                        else
                        {
                            _regModelList = new modellist();

                        }


                        if (iswrite == false)
                        {
                            FileInfo file = new FileInfo(filepath);
                            StreamWriter str = file.CreateText();

                            modeldata datas = new modeldata();
                            datas.account = registaccount.value;
                            datas.password = registpassword.value;

                            _regModelList.datalis.Add(datas);
                            //string jsonstr = JsonMapper.ToJson(_regModelList);//不加密处理方法
                            //str.Write(jsonstr);//不加密处理方法

                            /*****加密DES***********/
                            //数据加密处理
                            string myData;//字符串数据
                            myData = JsonMapper.ToJson(_regModelList);
                            str.Write(Des_data.EncryptDES(myData));
                            str.Close();
                            str.Dispose();
                            // AssetDatabase.Refresh();
                            // Registshowerrorlabel("(￣▽￣)~*注册成功！");
                            //注册成功  加载界面
                            LoginCtrl.instance.loadlabelinput = "\"(￣▽￣)~* 注册成功！";
                            LoginCtrl.instance.OnLoadTween();
                            AudioCtrl.instance.Play(1);//播放转换加载音效
                            isbtn = true;//禁用按钮

                        }
                    }

                }
                else
                {
                    Registshowerrorlabel("(╯﹏╰)密码 不合规！要求：字母数字组合，长度12-16");
                    AudioCtrl.instance.Play(0);//播放错误提示音效
                }

            }
            else
            {//提示账号不合规矩  应该是手机号
                Registshowerrorlabel("(╯﹏╰)账号不是手机号 不合规！");
                AudioCtrl.instance.Play(0);//播放错误提示音效
            }



          
       }
		Clearinput ();//清空输入框
    }





    /// <summary>
    /// 注册 提示label框
    /// </summary>
    /// <param name="errormessage"></param>
    void Registshowerrorlabel(string errormessage)
    {
        RegistErrortipshow.text = errormessage;
        Registerrorlabeltween.PlayForward();
        Invoke("registerrorshow", 3f);
    }
    /// <summary>
    /// 隐藏提示  label
    /// </summary>
    void registerrorshow()
    {
        RegistErrortipshow.text = string.Empty;
        Registerrorlabeltween.PlayReverse();
    }
    //跳转到登录界面
    public void ToLoginpanelbtnOnClick()
    {
        if (isbtn)
        {
            return;//当登录时会禁用按钮，按钮失效
        }
		Clearinput ();
        Logintween.PlayReverse();//切换界面
        Registtween.PlayReverse();//切换界面

    }
	/// <summary>
	/// 清空输入框
	/// </summary>
	void Clearinput()
	{
		//清空输入框
		registaccount.value=string.Empty;
		registpassword.value=string.Empty;
		registpasswordagain.value = string.Empty;

	}
}
