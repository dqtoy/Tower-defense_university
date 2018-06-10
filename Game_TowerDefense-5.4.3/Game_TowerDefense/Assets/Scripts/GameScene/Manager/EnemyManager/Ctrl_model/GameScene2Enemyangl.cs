using UnityEngine;
using System.Collections;
/// <summary>
/// 小怪行走时角度改变
/// </summary>
public class GameScene2Enemyangl : MonoBehaviour {
  
    int _index;
    void Start()
    {

    }


    void Update()
    {
        _index = gameObject.GetComponentInParent<Enemy>().index;
        //控制角色旋转
        if (_index == 1)
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
        else if (_index > 9)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }

    }

}
