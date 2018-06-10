using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadAvatar : MonoBehaviour {

 
 
    // Use this for initialization
    void Start () {

        Vector3 bosspos = new Vector3(-16, -4, -5);
        //加载新场景后  生成对应的人物模型
        if (AvatarSys._instance.nowcount == 0)
        {
            AvatarSys._instance.GirlAvatar(bosspos);
        }
        else
        {
            AvatarSys._instance.BoyAvatar(bosspos);
        }
    }
	
	
}
