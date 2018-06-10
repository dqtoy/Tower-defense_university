using UnityEngine;
using System.Collections;
/// <summary>
/// main场景的音频控制
/// </summary>
public class AudioCtrl : MonoBehaviour {
    public static AudioCtrl instance;
    AudioSource playaudio;//音频播放
    AudioClip[] _audioclip=new AudioClip[2];//存放音频
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        //下个游戏主界面的音效音量信息的保存
        PlayerPrefs.SetFloat("BGAudio_volume", 1f);
        PlayerPrefs.SetFloat("OtherAudio_volume", 1f);
    }
    void Start()
    {
        playaudio = GetComponent<AudioSource>();
        _audioclip[0] = Resources.Load<AudioClip>("Audio/button/errorshow");
        _audioclip[1] = Resources.Load<AudioClip>("Audio/button/btn");
    }
    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="index"></param>
    public  void Play(int index)
    {
        playaudio.clip = _audioclip[index];
        playaudio.loop = false;
        playaudio.Play();
    }
	
}
