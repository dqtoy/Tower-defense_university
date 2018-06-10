using UnityEngine;
using System.Collections;

/// <summary>
/// 子弹管理
/// </summary>
public class Bullet : MonoBehaviour {
    public float damage = 50;//伤害
    public float speed = 35; //子弹速度
    private Transform target;//追踪目标
    public GameObject ExplosionEffectPrefab;//子弹爆炸特效
    private float Distoenemy=1f;//距离敌人的距离
    float dis;//子弹与敌人的距离
    //不同炮塔的不同子弹对应的攻击加成
    float enemy1bulletdemagemoney = 3;
    float enemy1upgradebulletdemagemoney = 8;
    float enemy2bulletdemagemoney = 6;

    float bullet3demagemoney = 9;
    float bullet3upgradeoney = 16;
    float bullet4demagemoney = 12;
    float bullet4upgradedemagemoney = 20;
    float bullet5demagemoney = 15;
    float bullet5upgradeoney = 24;
   
    void Start()
    {
        Destroy(this.gameObject, 10f);    
    }
    /// <summary>
    /// 记录跟踪的目标
    /// </summary>
    /// <param name="_target"></param>
    public void SetTarget(Transform _target)
    {
        
        this.target = _target;


    }

    void FixedUpdate()
    {
       
        //目标不为空时  没有被销毁掉    
        if (target==null)
        {
            Die();
            return;
        }
       
        transform.LookAt(target.position);//子弹跟踪
        transform.Translate(Vector3.forward * speed * Time.deltaTime);//子移移动
        //求绝对值dis.magnitude<=0.8f判断
        // Vector3 dis = distarget.position - transform.position;

        dis = Vector3.Distance(transform.position, target.position);//子弹与敌人距离判断
        if (dis <= Distoenemy)
        {
            //敌人减少血量 同时加金币数量
            target.gameObject.GetComponent<Enemy>().TakeDamadge(damage);
            if (gameObject.name== "enemy1bullet(Clone)")
            {
                DataManager.DataInstance.datamoney += enemy1bulletdemagemoney;
            }
            else if (gameObject.name == "enemy1upgradebullet(Clone)")
            {
                DataManager.DataInstance.datamoney += enemy1upgradebulletdemagemoney;
            }
            else if (gameObject.name == "enemy2bullet(Clone)")
            {
                DataManager.DataInstance.datamoney += enemy2bulletdemagemoney;
            }
            else if (gameObject.name == "bullet4(Clone)")
            {//炮塔4的子弹
                DataManager.DataInstance.datamoney += bullet4demagemoney;
            }
            else if (gameObject.name == "bullet4_upgrade(Clone)")
            {//炮塔4的升级子弹
                DataManager.DataInstance.datamoney += bullet4upgradedemagemoney;
            }
            else if (gameObject.name == "Bullet5(Clone)")
            {//炮塔5的子弹
                DataManager.DataInstance.datamoney += bullet5demagemoney;
            }
            else if (gameObject.name == "Bullet5_upgrade(Clone)")
            {//炮塔5的升级子弹
                DataManager.DataInstance.datamoney += bullet5upgradeoney;
            }
            else if (gameObject.name == "Bullet3(Clone)")
            {//炮塔3的子弹
                DataManager.DataInstance.datamoney += bullet3demagemoney;
            }
            else if (gameObject.name == "Bullet3_upgrade(Clone)")
            {//炮塔3的升级子弹
                DataManager.DataInstance.datamoney += bullet3upgradeoney;
            }

            GameUIManager.GameUIInstance.UIshow(DataManager.DataInstance.datamoney);
            Die();
        }

    }

    /// <summary>
    /// 销毁子弹
    /// </summary>
    void Die()
    {
        GameObject explosioneffect = (GameObject)GameObject.Instantiate(ExplosionEffectPrefab, transform.position, transform.rotation);//产生特效
        Destroy(explosioneffect, 1);//销毁特效
        Destroy(this.gameObject);//销毁子弹


    }

  

}
