using UnityEngine;
using System.Collections;
/// <summary>
/// 路径管理
/// </summary>

public class Waypoints:MonoBehaviour  {

    public static Transform[] positions;

    private void Awake()
    {
        positions = new Transform[transform.childCount];//实例化数组 个数为子物体数量
        //遍历添加数组元素
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.GetChild(i);//把子物体transform放入数组
        }



    }

}
