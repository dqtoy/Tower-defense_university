using UnityEngine;
using System.Collections;

public class MainScene : MonoBehaviour {
    /********1进度加载*******/
    GameObject Slider_Widget;//进度加载总控界面
    UISlider jinduslider;//进度条
    UILabel tipshow;//加载中...  label提示框
    /********2动画播放*******/
    GameObject Mv_Widget;//视频播放总控界面
    MovieTexture mv;//视频初始化
    UITexture uitexture;//视频框
    AudioSource mvaudio;//音频初始化
    GameObject mvlabel;
    bool isshowlabel=false;//是否显示提示框bool
    bool _isplay=false;//是否在播放
    /*********3登录注册*************/
    GameObject loginregist_Widget;//登录注册管理
	
	void Start () {
        // 一：进度加载   脚本绑定实例化
        Slider_Widget = transform.Find("Slider_Widget").gameObject;//进度加载总控界面
        jinduslider = transform.Find("Slider_Widget/Slider_Background").GetComponent<UISlider>();
        tipshow= transform.Find("Slider_Widget/tIPSHOW").GetComponent<UILabel>();
        StartCoroutine("Tipshowlabel");//开启协同程序  显示加载label动画

        //  二：视频播放相关  实例化
        Mv_Widget = transform.Find("Mv_Widget").gameObject;//视频播放管理面板
        mvlabel = Mv_Widget.transform.FindChild("mvlabel").gameObject;// MV 提示框  是否退出？
        uitexture = Mv_Widget.transform.FindChild("MV_bg").GetComponent<UITexture>();//视频播放幕布
      
        mv = Resources.Load<MovieTexture>("Audio/MV/Cny");//视频文件
        mvaudio = uitexture.gameObject.GetComponent<AudioSource>();//音频文件
       
        uitexture.mainTexture = mv;//把视频加到幕布上
        mvaudio.clip = mv.audioClip;
        mv.loop = false;//不循环播放

        //三：登录注册面板管理
        loginregist_Widget = transform.FindChild("LoginRegist_Widget").gameObject;//登录注册管理面板
        loginregist_Widget.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
       
        SliderOn();//进度条加载
        
        if (Input.GetMouseButtonDown(0))
        {//点击鼠标左键  提示是否退出播放  
            if (mv.isPlaying)
            {//视频正在播放
                if (isshowlabel == false)
                {//还没显示提示框
                  
                    mvlabel.SetActive(true);
                     isshowlabel = true;
                }
                else
                {//提示框已经显示  再次点击即可退出
                  
                    MovieOver();
                }

            }

        }
       
        if (_isplay&&!mv.isPlaying)
        {//视频播放完后   也可以通过判断音频来判断视频
            MovieOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {//点击Esc键可以退出游戏
            Application.Quit();
        }


    }
    /// <summary>
    /// 加载进度条
    /// </summary>
    void SliderOn()
    {
        if (jinduslider.value<1)
        {
            jinduslider.value += 0.002f;
            if (jinduslider.value >= 0.99f)
            {
                jinduslider.value = 1;
                StopCoroutine("Tipshowlabel");
                //加载完成  
                Slider_Widget.SetActive(false);//隐藏进度界面
                Movieplay(); //播放视频
                _isplay = true;


            }
        }

        
    }
    /// <summary>
    /// 加载动画协程
    /// </summary>
    /// <returns></returns>
    IEnumerator Tipshowlabel()
    {
        while (true)
        {
            tipshow.text = "加 载 中 .";
            yield return new WaitForSeconds(0.5f);
            tipshow.text = "加 载 中 ..";
            yield return new WaitForSeconds(0.5f);
            tipshow.text = "加 载 中 ...";
            yield return new WaitForSeconds(0.5f);
        }
        

    }

    /// <summary>
    /// 视频播放
    /// </summary>
    private void Movieplay()
    {
        Mv_Widget.SetActive(true);//显示MV界面
        mv.Play();//播放视频
        mvaudio.Play();

    }
    /// <summary>
    ///退出播放   切换到登录界面
    /// </summary>
    private void MovieOver()
    {
        // 1退出播放   
        mv.Stop();//播放视频
        mvaudio.Stop();
        Mv_Widget.SetActive(false);//关闭视频窗界面  

        // 2切换到  登录界面
        loginregist_Widget.SetActive(true);

    }

}
