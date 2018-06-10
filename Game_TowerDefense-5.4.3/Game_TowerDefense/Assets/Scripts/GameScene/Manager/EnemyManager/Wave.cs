using UnityEngine;
using System.Collections;
/// <summary>
/// 保存每一波敌人生成所需要的属性
/// </summary>



//可以序列化
[System.Serializable]
public class Wave  {
    public GameObject enemyPrefab;//预设物
    public int count;//生成数量
    public float rate;//生成时间间隔

}
