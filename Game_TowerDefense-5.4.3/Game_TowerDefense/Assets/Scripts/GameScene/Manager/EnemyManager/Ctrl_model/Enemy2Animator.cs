using UnityEngine;
using System.Collections;
/// <summary>
/// Enemy2动画
/// </summary>
public class Enemy2Animator : MonoBehaviour {
    Animation Ani;//初始化Animation
    int _index;
    void Start()
    {
        Ani = GetComponent<Animation>();//实例化脚本
        // Ani.clip = Ani.GetClip("walk");
        // Ani.Play();

        Ani.Play("walk");//播放单个动画
        Ani.wrapMode = WrapMode.Loop;//循环播放

        
    }


    void Update()
    {
        _index = gameObject.GetComponentInParent<Enemy>().index;
        //控制角色旋转
        if (_index==1)
        {
            transform.localEulerAngles = new Vector3(0, 90, 0);
            // transform.FindChild("enemyall").localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (_index == 2)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            //transform.FindChild("enemyall").localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (_index == 3)
        {
            transform.localEulerAngles = new Vector3(0, -90, 0);
            //transform.FindChild("enemyall").localEulerAngles = new Vector3(0, -90, 0);
        }
        else if (_index == 4)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            //transform.FindChild("enemyall").localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (_index == 5)
        {
            transform.localEulerAngles = new Vector3(0, 90, 0);
            //transform.FindChild("enemyall").localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (_index == 6)
        {
            //transform.FindChild("enemyall").localEulerAngles = new Vector3(0, 180, 0);
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (_index == 7)
        {
            // transform.FindChild("enemyall").localEulerAngles = new Vector3(0, -90, 0);
            transform.localEulerAngles = new Vector3(0, -90, 0);
        }
        else if (_index == 8)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            // transform.FindChild("enemyall").localEulerAngles = new Vector3(0, 180, 0); 
        }
        else if (_index == 9)
        {
            //transform.FindChild("enemyall").localEulerAngles = new Vector3(0, 90, 0);
            transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if(_index>9)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
     
    }
    


}
