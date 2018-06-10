using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Asyncload_panel : MonoBehaviour {
    public static Asyncload_panel instance;//单例

    Image mapbg;//需要修改的背景图片
  
   string  loadmapname;//       异步加载的场景名
    Sprite[] _bgtexture = new Sprite[2];
    Slider loadslider;//进度条
    Text slider_showtext;//进度百分比显示
    AsyncOperation async_scene;//异步加载
    float loadspeed = 1f;//速度
    bool isOnload = false;//是否正在加载
    float targetvalue;//加载进度
    void Awake() {
        if (instance==null)
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
        loadslider = transform.Find("Slider").GetComponent<Slider>();
        slider_showtext= transform.Find("Slider/SliderShoweText").GetComponent<Text>();
        mapbg = this.GetComponent<Image>();
        _bgtexture[0] = Resources.Load<Sprite>("Textures/Startscene/Asyncloadmapbg1");
        _bgtexture[1] = Resources.Load<Sprite>("Textures/Startscene/Asyncloadmapbg2");
        //判断异步加载场景名字修改背景图
        if (loadmapname == "GameScene")
        {
            mapbg.sprite = _bgtexture[0];
        }
        else if (loadmapname == "GameScene_1")
        {
            mapbg.sprite = _bgtexture[1];
        }
    }
    void Update() {

        if (isOnload)
        {
            targetvalue = async_scene.progress;//进度更新
            if (async_scene.progress>=0.9f)
            {//async_scene.progress加载到0.9就已经加载完成了
                targetvalue = 1f;
            }
            if (targetvalue!= loadslider.value)
            {//插值计算，让滑条岁场景进度炮
                loadslider.value = Mathf.Lerp(loadslider.value,targetvalue,Time.deltaTime*loadspeed);
                if (Mathf.Abs(loadslider.value-targetvalue)<0.01f)
                {
                    loadslider.value = targetvalue;
                }
            }
            slider_showtext.text = (int)(loadslider.value * 100) + "%";//Text显示
            if (loadslider.value==1f)
            {
                //加载完成
                async_scene.allowSceneActivation = true;//允许加载完场景后自动切换到下一个场景
            }


        }
    }

    /// <summary>
    /// 加载协程
    /// </summary>
    /// <param name="scenename"></param>
    /// <returns></returns>
    public IEnumerator Onloadscene(string scenename)
    {
        isOnload = true;
        loadmapname = scenename;//确定加载的场景名
        async_scene = SceneManager.LoadSceneAsync(scenename);
        async_scene.allowSceneActivation = false;//阻止加载完自动切换
        yield return async_scene;

    }





}
