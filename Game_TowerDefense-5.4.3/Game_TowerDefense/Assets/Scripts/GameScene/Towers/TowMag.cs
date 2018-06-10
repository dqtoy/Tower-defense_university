using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Towtype {
tow1,tow2, tow3, tow4, tow5, tow6

}

/// <summary>
/// 对每一个生成的炮塔进行控制管理
/// </summary>
public class TowMag : MonoBehaviour {

    public Towtype Mytowertype;
    [HideInInspector]
    public    List<GameObject> enemys = new List<GameObject>();//存储检测到的敌人
     float attackRateTime = 1;//每一秒发射子弹
     float addtime=0;//累加时间
     public GameObject   bulletprefab;//子弹预设物
     Transform Firepos;//发射点
    Transform Turret_head;//可旋转的炮管
    LineRenderer Tow3linebullet;//炮塔3激光

    //激光武器伤害值
    float DemageTow3 = 70;//普通激光
    float DemageTow3money = 8;//普通激光伤害加成
    float DemageTow3upgrade = 100;//升级激光
    float DemageTow3upgrademoney = 10;//升级激光 伤害加成
    GameObject effectenemy3;//激光特效预设物
    GameObject explosioneffect;//产生的激光特效
    void Start()
     {
         Firepos = transform.FindChild("Base/Turret/Firepos");//获取发射点
        Turret_head = transform.FindChild("Base/Turret"); ;//获取可旋转炮管
        if (Mytowertype== Towtype.tow6)
        {
            Tow3linebullet = transform.FindChild("Base/Turret/Firepos/Line").GetComponent<LineRenderer>();
            effectenemy3 = Resources.Load<GameObject>("Prefabs/Effects/BuildEffect");//产生激光特效
        }
        addtime = attackRateTime;
       
      
    }
     void OnTriggerEnter(Collider col)
     {

        if (col.gameObject.tag=="Enemy")
        {
            enemys.Add(col.gameObject);
        }
     }
 
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    void FixedUpdate()
    {//发射子弹
        switch (Mytowertype)
        {
            case Towtype.tow1:
            case Towtype.tow2:
            case Towtype.tow3:
            case Towtype.tow4:
            case Towtype.tow5:
           
            
                {
                    addtime += Time.deltaTime;
                    if (enemys.Count > 0 && addtime >= attackRateTime)
                    {
                        addtime = 0;
                        Attack();//发射炮弹
                    }
                    //旋转炮管
                    if (enemys.Count>0&&enemys[0]!=null)
                    {
                        /*不改变高度的旋转
                         * Vector3 targetposition = enemys[0].transform.position;
                         * targetposition.y = Turret_head.position.y;
                         * Turret_head.LookAt(targetposition);*/
                         Turret_head.LookAt(enemys[0].transform);
                    }


                    break;
            }
            case Towtype.tow6:
                {
                    //第三种炮塔就发射激光武器  //旋转炮管
                    if (enemys.Count > 0)
                    {
                        if (enemys[0] == null)
                        {

                            UpdateEnemy();
                        }
                        if (enemys.Count>0)
                        {
                          

                            Tow3linebullet.enabled = true;
                            Firepos.LookAt(enemys[0].transform);
                            Tow3linebullet.SetPositions(new Vector3[]{Firepos.position, enemys[0].transform.position} );//设置激光初始位置和结束位置
                          
                            explosioneffect = (GameObject)GameObject.Instantiate(effectenemy3, enemys[0].transform.position, transform.rotation);//产生激光特效
                            Destroy(explosioneffect,0.01f);
                            if (gameObject.name== "Tow6(Clone)")
                            {
                                enemys[0].GetComponent<Enemy>().TakeDamadge(DemageTow3 * Time.deltaTime);//敌人减少血量
                                DataManager.DataInstance.datamoney += (DemageTow3money * Time.deltaTime);//增加金币
                            }
                            else
                            {
                                enemys[0].GetComponent<Enemy>().TakeDamadge(DemageTow3upgrade * Time.deltaTime);//敌人减少血量
                                DataManager.DataInstance.datamoney += (DemageTow3upgrademoney * Time.deltaTime);//增加金币
                            }
                          
                            GameUIManager.GameUIInstance.UIshow(DataManager.DataInstance.datamoney);//UI更新
                        }


                    }
                    else
                    {
                        //没有敌人不攻击

                        Tow3linebullet.enabled = false;
                        //销毁特效
                        Destroy(explosioneffect);
                    }
                    break;
                }
            default:
                break;
        }
      

    }
    void Attack()
    {
        if (enemys[0]==null)
        {
            UpdateEnemy();
        }
        if (enemys.Count>0)
        {
            if (Mytowertype==Towtype.tow2&&gameObject.name== "Tow2_upgrade(Clone)")
            {
                //是炮塔2升级版时要发射多枚炮弹
                GameObject bullet2 = (GameObject)GameObject.Instantiate(bulletprefab, Firepos.position + new Vector3(-0.25f, 0, 0), Firepos.rotation);
                bullet2.GetComponent<Bullet>().SetTarget(enemys[0].transform);
                GameObject bullet22 = (GameObject)GameObject.Instantiate(bulletprefab, Firepos.position + new Vector3(0.25f, 0, 0), Firepos.rotation);
                bullet22.GetComponent<Bullet>().SetTarget(enemys[0].transform);
            }
            else
            {
                GameObject bullet = (GameObject)GameObject.Instantiate(bulletprefab, Firepos.position, Firepos.rotation);
                bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
            }
         

        }
        else
        {
            addtime = attackRateTime;
        }

    }

    /// <summary>
    /// 数组为空清除空对象
    /// </summary>
    void UpdateEnemy()
    {
        List<int> emptyIndex = new List<int>();
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i]==null)
            {
                emptyIndex.Add(i);
            }
        }
        for (int j = 0; j < emptyIndex.Count; j++)
        {
            enemys.RemoveAt(emptyIndex[j] - j);
        }
    }

}
