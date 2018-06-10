using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// 建造炮塔管理
/// 每个点击的炮台基座  存储的炮塔管理  生成炮塔
/// </summary>
public class Towpoint : MonoBehaviour
{
    [HideInInspector]//隐藏public的Inspector面板显示
    public GameObject Paota;//保存当前炮塔基座身上的炮塔   每个基座都有一个此脚本
    GameObject Buildeffect;//炮塔生成特效预设物 
    GameObject Removeeffect;//一处炮塔时产生的特效预设物 
    [HideInInspector]
    public bool isUpgraded = false;//保存炮塔是否升级信息
    private SpriteRenderer paotarender;//当鼠标放在炮台基座会显示红色建造位置方框
    private TurrentData turrentdata;//储存建造的炮塔信息

   
    void Start()
    {
        paotarender = GetComponentInChildren<SpriteRenderer>();
        Buildeffect = Resources.Load<GameObject>("Prefabs/Effects/BuildEffect");//获取生成炮塔特效预设物
        Removeeffect = Resources.Load<GameObject>("Prefabs/Effects/RemoveEffect");//获取生成炮塔特效预设物
    }


    //创建炮塔
    public void BuildTow(TurrentData buildpaota)
    {
       
        isUpgraded = false;
        this.turrentdata = buildpaota;//把建造的炮塔信息储存起来
        // Paota = (GameObject)GameObject.Instantiate(buildpaota, transform.position, Quaternion.identity);
        Paota = Instantiate(buildpaota.turrentPrefab);//生成炮塔
        Paota.transform.parent = GameObject.Find("Paota").transform;
        Paota.transform.localPosition = transform.position + new Vector3(0, 0.38f, 0);
        GameObject Effects = (GameObject)GameObject.Instantiate(Buildeffect, transform.position, Quaternion.identity);//生成炮塔使用的特效
        Destroy(Effects, 0.25f);
    }
   /// <summary>
   /// 升级炮塔
   /// </summary>
   /// <param name="upgradepaota"></param>
    public void UpgradeTow()
    {
        if (isUpgraded==true)
        {
            return;//判断炮塔是否被升级过 升级过就不处理
        }
        if (DataManager.DataInstance.datamoney>= turrentdata.costUpgrade)
        {
            AudioCtrl_Startgame.Instance.Otheraudioplay(6);//播放升级音效
            //拥有的金币够升级新炮塔时
            DataManager.DataInstance.datamoney -= turrentdata.costUpgrade;
            // UI动态显示金币   游戏结束需要保存DataManager
            GameUIManager.GameUIInstance.UIshow(DataManager.DataInstance.datamoney);

            //销毁原来炮塔 生成新炮塔
            Destroy(Paota);
            isUpgraded = true;

            // Paota = (GameObject)GameObject.Instantiate(buildpaota, transform.position, Quaternion.identity);
            //扣钱
            Paota = Instantiate(turrentdata.turrentUpgradePrefab);//生成升级炮塔
            Paota.transform.parent = GameObject.Find("Paota").transform;
            Paota.transform.localPosition = transform.position + new Vector3(0, 0.38f, 0);
            GameObject Effects = (GameObject)GameObject.Instantiate(Buildeffect, transform.position, Quaternion.identity);//生成炮塔使用的特效
            Destroy(Effects, 0.25f);
        }
        else
        {
            //钱不够时
            //提示钱不够 动画
            GameUIManager.GameUIInstance.TipAnimator();
        }
       

    }
    /// <summary>
    /// 拆除炮塔
    /// </summary>
    public void DestroyTow()
    { // 销毁炮塔前 销毁的炮塔可以置换金币
        if (isUpgraded == true)
        {
            //如果炮塔升级过  
            DataManager.DataInstance.datamoney += (turrentdata.costUpgrade-35);
            // UI动态显示金币   游戏结束需要保存DataManager
            GameUIManager.GameUIInstance.UIshow(DataManager.DataInstance.datamoney);
        }
        else
        {
            //如果炮塔未升级过
          
            DataManager.DataInstance.datamoney += (turrentdata.cost-25);
            // UI动态显示金币   游戏结束需要保存DataManager
            GameUIManager.GameUIInstance.UIshow(DataManager.DataInstance.datamoney);
        }
        //销毁原来炮塔
        Destroy(Paota);
        GameObject Effects = (GameObject)GameObject.Instantiate(Removeeffect, transform.position, Quaternion.identity);//拆除炮塔使用的特效
        Destroy(Effects, 0.25f);
        isUpgraded = false;
        Paota = null;
        turrentdata = null;
    }
    void OnMouseEnter()
    {
        //鼠标放在塔台上 塔台没有物体 且 鼠标没有接触UI部分
        if (Paota==null && EventSystem.current.IsPointerOverGameObject()==false)
        {
            paotarender.enabled = true;
        }
    }
    void OnMouseExit()
    {
        paotarender.enabled = false;
    }
}
