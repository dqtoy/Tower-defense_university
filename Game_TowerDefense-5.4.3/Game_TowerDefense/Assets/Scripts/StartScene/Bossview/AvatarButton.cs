using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarButton : MonoBehaviour {
    /// <summary>
    /// 换装toggle点击    性别切换
    /// </summary>
    /// <param name="isOn"></param>
    public void OnValueChanged(bool isOn) {
        //次函数绑定在 ： 切换性别的男女UI上  &&  每个部位的具体修改的UI上
        if (isOn) {
            
            if (gameObject.name == "girl") {
               
                AvatarSys._instance.SexChange(0);//切换人物   隐藏对应面板 0表示切换小女孩
                return;
            }
            else if(gameObject.name == "boy")
            {
                AvatarSys._instance.SexChange(1);//切换人物   隐藏对应面板  1表示切换小男孩
                return;
            }


            string[] names = gameObject.name.Split('_');
          
            AvatarSys._instance.OnChangePeople(names[0],names[1]);//
            switch (names[0]) { 
                case "pants":
                    PlayAnimation("item_pants");//播放换装动画
                    break;
                case  "shoes":
                    PlayAnimation("item_boots");
                    break;
                case "top":
                    PlayAnimation("item_shirt");
                    break;
                default:
                    break;
            }
        }
    
    }
    /// <summary>
    /// 播放换装动画
    /// </summary>
    /// <param name="animName"></param>
    public void PlayAnimation(string animName) { //换装动画名称

        Animation anim = GameObject.FindWithTag("Boss").GetComponent<Animation>();
        if (!anim.IsPlaying(animName)) {
            anim.Play(animName);
            anim.PlayQueued("idle1");
        }
    
    }
    
}
