using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 建造炮塔管理
/// </summary>
public class BuildManager : DataManager{
    //几种炮塔
    public TurrentData tow1TurrentData;
    public TurrentData tow2TurrentData;
    public TurrentData tow3TurrentData;
    public TurrentData tow4TurrentData;
    public TurrentData tow5TurrentData;
    public TurrentData tow6TurrentData;


   
    //表示当前选择的炮台  需要建造的炮台
    private TurrentData selectedTurrentData;

    [HideInInspector]//隐藏public的Inspector面板显示
    public Towpoint upgradeselectPaotai;//当前升级时UI显示选中的炮台




    // datamoney拥有的金钱用于中间修改数据   它属于数据脚本

    void Start()
    {
         
        //money =  DataManager.DataInstance.datamoney;//把初始的金钱数据给临时使用的money

    }
    /// <summary>
    /// 金币被改变了
    /// </summary>
    /// <param name="change"></param>
    void ChangeMoney(int change)
    {
        datamoney += change;

        GameUIManager.GameUIInstance.UIshow(datamoney);// UI动态显示金币   游戏结束需要保存DataManager
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //鼠标点击是否在UI面板上进行判断  防止点击了按钮，点按钮时就不产生射线  true时鼠标在UI上
            //判断鼠标点击UI括号里面可以不设置参数，如果是手机设置需要设置参数哪个判断手指点击
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //生成射线
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapTowers")))
                {
                   Towpoint maptowers = hit.collider.GetComponent<Towpoint>();//得到点击的maptowers
                   
                    if (maptowers.Paota==null)
                    {
                        if (selectedTurrentData!=null)//已经点击了UI选择了需要建造的炮塔
                        {
                            //可以创建  判断金币是否够买东西
                            if (datamoney >= selectedTurrentData.cost)
                            {
                                //当金钱够用时减去购买开销  显示剩余金币
                                ChangeMoney(-selectedTurrentData.cost);
                                maptowers.BuildTow(selectedTurrentData);//调用炮塔生成函数
                                AudioCtrl_Startgame.Instance.Otheraudioplay(2);//播放创建音效

                            }
                            else
                            {
                                //提示钱不够 动画
                                GameUIManager.GameUIInstance.TipAnimator();
                               
                            }
                        }
                      


                    }
                    else if (selectedTurrentData != null)
                    {
                       
                        //升级处理

                        if (maptowers== upgradeselectPaotai&&GameUIManager.GameUIInstance.UpgradetowCanvas.activeInHierarchy)
                        {
                            GameUIManager.GameUIInstance.StartHide();//隐藏升级Canvas
                        }
                        else
                        {
                            //if (maptowers.isUpgraded)
                            //{
                            //    GameUIManager.GameUIInstance.ShowUpgrade(maptowers.transform.position,true);
                            //}
                            //else
                            //{
                            //    GameUIManager.GameUIInstance.ShowUpgrade(maptowers.transform.position, false);
                            //}
                            //显示升级UI面板
                            GameUIManager.GameUIInstance.ShowUpgrade(maptowers.transform.position + new Vector3(0, 2f, 0), maptowers.isUpgraded);
                             float removemoney=0;//拆除炮塔需要的金币
                            if (maptowers.isUpgraded)
                            {
                                //点击的是升级过的炮塔  在升级UI面板显示拆除获得的金币数
                               
                                switch (maptowers.Paota.GetComponent<TowMag>().Mytowertype)
                                {
                                    case Towtype.tow1: {removemoney = tow1TurrentData.costUpgrade - 35;} break;
                                    case Towtype.tow2: { removemoney = tow2TurrentData.costUpgrade - 35; } break;
                                    case Towtype.tow3: { removemoney = tow3TurrentData.costUpgrade - 35; } break;
                                    case Towtype.tow4: { removemoney = tow4TurrentData.costUpgrade - 35; } break;
                                    case Towtype.tow5: { removemoney = tow5TurrentData.costUpgrade - 35; } break;
                                    case Towtype.tow6: { removemoney = tow6TurrentData.costUpgrade - 35; } break;
                                    default:
                                        break;
                                }


                            }
                            else
                            {
                                //点击的是未升级过的炮塔  在升级UI面板显示拆除获得的金币数 
                                switch (maptowers.Paota.GetComponent<TowMag>().Mytowertype)
                                {
                                    case Towtype.tow1: { removemoney = tow1TurrentData.cost - 25; }
                                    break;
                                    case Towtype.tow2: { removemoney = tow2TurrentData.cost - 25; }
                                    break;
                                    case Towtype.tow3: { removemoney = tow3TurrentData.cost - 25; }
                                    break;
                                    case Towtype.tow4: { removemoney = tow4TurrentData.cost - 25; }
                                        break;
                                    case Towtype.tow5: { removemoney = tow5TurrentData.cost - 25; }
                                        break;
                                    case Towtype.tow6: { removemoney = tow6TurrentData.cost - 25; }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            GameUIManager.GameUIInstance.UpgradetowCanvas.transform.FindChild("DeleteButton/MoneyText").GetComponent<Text>().text= "拆除获得¥："+ removemoney;//更改拆除需要的金币

                        }

                        upgradeselectPaotai = maptowers;//选中的炮塔就把它保存起来
                        GameUIManager.GameUIInstance.towpoint = upgradeselectPaotai;
                    }

                }
              
            }
        }
        
    }


    /// <summary>
    /// 选择炮塔的bool函数储存当前选择炮塔
    /// </summary>
    /// <param name="isOn"></param>
    public void OnTow1Selected(bool isOn)
    {
        if (isOn)
        {
            selectedTurrentData = tow1TurrentData;
        }
       
    }
    public void OnTow2Selected(bool isOn)
    {

        if (isOn)
        {
            selectedTurrentData = tow2TurrentData;
        }
    }
    public void OnTow3Selected(bool isOn)
    {

        if (isOn)
        {
            selectedTurrentData = tow3TurrentData;
        }
    }
    public void OnTow4Selected(bool isOn)
    {

        if (isOn)
        {
            selectedTurrentData = tow4TurrentData;
        }
    }
    public void OnTow5Selected(bool isOn)
    {

        if (isOn)
        {
            selectedTurrentData = tow5TurrentData;
        }
    }
    public void OnTow6Selected(bool isOn)
    {

        if (isOn)
        {
            selectedTurrentData = tow6TurrentData;
        }
    }
}

