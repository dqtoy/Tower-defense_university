using UnityEngine;
using System.Collections;

/// <summary>
/// 炮塔管理类
/// </summary>
[System.Serializable]
public class TurrentData {
    public GameObject turrentPrefab;//初始炮塔预设物
    public int cost;//初始炮塔需要金币
    public GameObject turrentUpgradePrefab;//升级炮塔预设物
    public int costUpgrade;//升级炮塔需要金币
    public TurrentType type;//当前设置炮塔类型  详见前端Inspector
}
public enum TurrentType {
    tow1,
    two2,
    tow3,
    tow4,
    tow5,
    tow6

}