using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 敌人生成管理
/// </summary>
public class Enemyspawner : MonoBehaviour {
    public Wave[] waves;//存储敌人信息的数组
   
    public static int CountEnemyAlive;//生成的敌人数量
    Transform startpos;//生成点
    float waverate = 3f;//每一波怪物的时间间隔
    GameObject SliderCanvas;//血条预设物
    [HideInInspector]
    public static int dijiboenemy = 0; //目前是第几波敌人
    void Start()
    {
        startpos = transform.FindChild("startpoint");//生成点
        SliderCanvas = Resources.Load<GameObject>("Prefabs/SliderCanvas");//获取血条预设物
        StartCoroutine(SpawnEnemy());//开启携程
       
       
    }
     void Update()
    {
       
    }
    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            dijiboenemy = i + 1;//对应第几波怪物
            GameUIManager.GameUIInstance.showenemydijibotext(dijiboenemy);//对应第几波怪物的Text提示框 调用


            for (int j = 0; j < waves[i].count; j++)
            {
              //Instantiate(waves[i].enemyPrefab.transform,startpos.position,Quaternion.identity);//生成 不旋转
                Transform enemytransf = Instantiate(waves[i].enemyPrefab.transform);//生成敌人
                enemytransf.position = startpos.position;
                enemytransf.SetParent(GameObject.Find("EnemyBurn").transform);//设置父物体
                GameObject slidercanvas = Instantiate(SliderCanvas);//生成血条
                slidercanvas.transform.parent = enemytransf;//给血条设置父物体
                slidercanvas.transform.localPosition = new Vector3(0, 2.5f, 0);

                CountEnemyAlive++;//生成的敌人数量加一
                if (j != waves[i].count - 1)
                {
                    yield return new WaitForSeconds(waves[i].rate);//此轮怪物类型中每一个怪物间隔时间生成
                }

            }
            //控制每一波数量被销毁完之后才进行下一波生成  
            while (CountEnemyAlive > 0)
            {
                yield return 0;//当前怪物数量大于0 则停止执行 直到不满足条件跳出循环
            }
            yield return new WaitForSeconds(waverate);//一波一波怪物时间间隔
        }



    }

}
