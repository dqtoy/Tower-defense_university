using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 敌人控制
/// </summary>
/// 
public enum Enemytype {
enemyone,enemytwo,enemythree,enemyfour,enemyfive,enemysix,enemyseven,enemyeight

}
public class Enemy : MonoBehaviour {
    public Enemytype type;//敌人类型
    float jianbosshp = 0;//减少的城堡boss老窝血量

    private Transform[] positions;//寻点位置信息
    public float HP = 150;//敌人血量
    float enemyallhp;//总血量
    [HideInInspector]//隐藏public的Inspector面板显示
    public  int index = 0;
    public float speed = 30;//小兵速度
    Slider enemyslider;//敌人血条
    public GameObject EnemyExplosionEffect;//敌人爆炸特效
    void Start()
    {
      
        enemyallhp = HP;
        positions = Waypoints.positions;//获取路径点位置
    
       
        enemyslider = transform.FindChild("SliderCanvas(Clone)/Slider").gameObject.GetComponent<Slider>();
    }
  
// Update is called once per frame
    void Update()
    {

        Move();
      

    }

    private void Move()
    {
        //判断在小兵走到最后一个路径点
        if (index <= positions.Length - 1)
        {
            transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);//小兵按照向量轨迹行走
            if (Vector3.Distance(positions[index].position, transform.position) < 1f)
            {
                index++;//到达一个路径点下标加一，开始走下一个路径向量

            }
        }
        else
        {
            ReachDestination();// 小怪到达目的地
        }


    }
    /// <summary>
    /// 小怪到达目的地
    /// </summary>
    void ReachDestination()
    {
        Die();//销毁敌人
       
        switch (type)
        {
            case Enemytype.enemyone:
            case Enemytype.enemyfive:
                jianbosshp = -10;
                break;
            case Enemytype.enemytwo:
            case Enemytype.enemysix:
                jianbosshp = -20;
                break;
            case Enemytype.enemythree:
            case Enemytype.enemyseven:
                jianbosshp = -30;
                break;
            case Enemytype.enemyfour:
            case Enemytype.enemyeight:
                jianbosshp = -40;
                break;
            default:
                break;
        }
        Home_Boss.instance.HPupdate(jianbosshp);

    }

    /// <summary>
    /// 减少血量
    /// </summary>
    /// <param name="damage"></param>
    public  void TakeDamadge(float damage)
    {
       
        HP -= damage;
        if (HP<=0)
        {
            //血量小于0  销毁自身
            Die();
            //OnDestroy();
            return;
        }
        enemyslider.value = (float)HP/ (float)enemyallhp;
       


    }
    /// <summary>
    /// 销毁自身
    /// </summary>
    void Die()
    {
        GameObject effect = (GameObject)GameObject.Instantiate(EnemyExplosionEffect,transform.position,transform.rotation);
        Destroy(effect, 1.5f);
      
        Destroy(this.gameObject);

    }
    /// <summary>
    /// 控制敌人数量 
    /// </summary>
    void OnDestroy()
    {
        Enemyspawner.CountEnemyAlive--;
    }



}
