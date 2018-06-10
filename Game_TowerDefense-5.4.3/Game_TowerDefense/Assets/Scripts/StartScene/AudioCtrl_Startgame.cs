using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioCtrl_Startgame : MonoBehaviour {
    /*******************音频注释：方便对应播放查看*******************/
    //BgAudioClip中存放的对应音频名字（下标index 0-4 循环播放的）：[0]main_theme、[1]map_theme、[2]other_theme、[3]gameplay_theme2、[4]gameplay_theme3
    //OtherAudioClip中存放的对应音频名字（下标index 0-15 不循环的）：[0]btn、 [1]btn1、 [2]build_tow 、    [3]change_btn 、 [4]click_btn 、[5]click_select 、[6]click_upgrade 、[7]click_mapover、 [8]defeat、 [9]defeat3、  [10]delete_tow 、 [11]errorshow、 [12] select 、[13]select_tow、 [14]win0、 [15]win1

    public static AudioCtrl_Startgame Instance;//单例类

    AudioSource BgAudio;//背景音乐
    public List<AudioClip> BgAudioClip;
    AudioSource OtherAudio;//其他音效
    public List<AudioClip> OtherAudioClip;

    [HideInInspector]//隐藏前台控制显示
    public bool bgaudioisnoplay = false;//背景是否静音  
    [HideInInspector]
    public bool otheraudioisnoplay = false;//其他是否静音      
    [HideInInspector]
    public float bgaudiovolume;//背景音量
    [HideInInspector]
    public float otheraudiovolume ;//其他音量
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);//不要销毁音频控制脚本
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

    }
    void Start() {//实例化音效控制
        BgAudio = transform.FindChild("bgaudio").GetComponent<AudioSource>();
        OtherAudio= transform.FindChild("otheraudio").GetComponent<AudioSource>();
       
    }
    /****控制播放*******/
    public void BGaudioplay(int index)
    {//背景音乐播放 index为对应下标
      
       
        bgaudiovolume = PlayerPrefs.GetFloat("BGAudio_volume");
        
        if (bgaudiovolume!=0)
        {
            
            BgAudio.clip = BgAudioClip[index];
            BgAudio.loop = true;
            BgAudio.volume = bgaudiovolume;
            BgAudio.Play();
        }
       
    }
    public void Otheraudioplay(int index)
    {//其他音效播放 index为对应下标
       
        otheraudiovolume= PlayerPrefs.GetFloat("OtherAudio_volume");
        
        if (otheraudiovolume != 0)
        {
            OtherAudio.clip = OtherAudioClip[index];
            OtherAudio.loop = false;
            OtherAudio.volume = otheraudiovolume;
            OtherAudio.Play();
        }
      
    }
    /****调节音量*******/
    public void BGaudioVolume(float vol)
    {//控制背景音乐播放 音量 vol为音量值
        
        PlayerPrefs.SetFloat("BGAudio_volume", vol);
        bgaudiovolume = vol;
        BgAudio.volume = bgaudiovolume;//改变音量
        if (bgaudiovolume <= 0)
        {
            bgaudioisnoplay = true;
            BGaudioMute(true);//静音
        }
        else
        {
            bgaudioisnoplay = false;
            BGaudioMute(false);//静音

        }
        if (BgAudio.clip==null)
        {
            BgAudio.clip = BgAudioClip[2];
            BgAudio.Play();

        }
      
    }
    public void OtheraudioVolume(float vol)
    {//控制其他音效播放音量 vol为音量值
       
        PlayerPrefs.SetFloat("OtherAudio_volume", vol);
        otheraudiovolume = vol;
        OtherAudio.volume = otheraudiovolume;//改变音量
        if (otheraudiovolume <= 0)
        {
            otheraudioisnoplay = true;
            OtheraudioMute(true);
        }
        else
        {
            otheraudioisnoplay = false;
            OtheraudioMute(false);
        }
    }
    /**********静音************/
    /// <summary>
    /// 背景音效是否静音 ismute=true静音
    /// </summary>
    /// <param name="ismute"></param>
    public void BGaudioMute(bool ismute)
    {

        BgAudio.mute = ismute;
    }
    /// <summary>
    /// 其让音效是静音
    /// </summary>
    public void OtheraudioMute(bool ismute)
    {
        OtherAudio.mute = ismute;

    }
    public void Destroyaudiogameobject()
    {

        Destroy(this.gameObject);
    }
 }
