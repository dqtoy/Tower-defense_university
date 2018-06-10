using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSys : MonoBehaviour {

    public static AvatarSys _instance;//单例
    /*******小女孩*******/
    private Transform girlSourceTrans;//资源model
    private GameObject girlTarget; //骨架物体，换装的人  
    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> girlData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>(); 
    //小女孩所有的资源信息   //部位的名字，部位编号，部位对应的skm
    Transform[] girlHips; //小女孩骨骼信息
    private Dictionary<string, SkinnedMeshRenderer> girlSmr = new Dictionary<string, SkinnedMeshRenderer>();// 换装骨骼身上的skm信息
   
    //初始化信息$

    /*******小男孩*******/
    private Transform boySourceTrans;//资源model
    private GameObject boyTarget; //骨架物体，换装的人  
    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> boyData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
    //小男孩所有的资源信息   //部位的名字，部位编号，部位对应的skm
    Transform[] boyHips; //小男孩骨骼信息
    private Dictionary<string, SkinnedMeshRenderer> boySmr = new Dictionary<string, SkinnedMeshRenderer>();// 换装骨骼身上的skm信息
    //部位的名字，部位对应的skm
 
    //初始化信息

    [HideInInspector]
    public int nowcount = 0;//0代表此时是女孩  1代表男孩
    //public GameObject girlPanel;//小女孩的UI
    //public GameObject boyPanel;//小男孩的UI

    void Awake() {
        //单例模式
        if (_instance==null)
        {
            _instance = this;
        }
        else
        {
            Destroy(_instance);
        }
      
    }

    void Start() {
        GirlAvatar(new Vector3(-16, -4, -5));//小女孩控制
        BoyAvatar(new Vector3(-16, -4, -5));//小男孩控制

        //绑定旋转查看人物脚本
        boyTarget.AddComponent<SpinWithMouse>();
        girlTarget.AddComponent<SpinWithMouse>();

        //初始状态让小男孩隐藏起来

        boyTarget.SetActive(false);
        //是游戏主场景时  初始就创建界面
       
    }
    

    /// <summary>
    /// 小女孩
    /// </summary>
   public  void GirlAvatar(Vector3 bosspos) {
        InstantiateGirl(bosspos);//生成小女孩
        SaveData(girlSourceTrans,girlData,girlTarget,girlSmr);//存储部位数据  生成对应部位
        InitAvatarGirl();
    }
   /// <summary>
   /// 小男孩
   /// </summary>
   public  void BoyAvatar(Vector3 bosspos) { 
        InstantiateBoy(bosspos);//生成小女孩
        SaveData(boySourceTrans,boyData,boyTarget,boySmr);//存储部位数据  生成对应部位
        InitAvatarBoy();
       
    }
    /// <summary>
    /// 加载女孩
    /// </summary>
    void InstantiateGirl(Vector3 bosspos) {
        GameObject go = Instantiate(Resources.Load("BossSources/Prefabs/FemaleModel")) as GameObject; //加载资源物体
        girlSourceTrans = go.transform;//提前保存资源物体  便于在后面查找
        go.SetActive(false);//把资源人物模型隐藏了
        girlTarget = Instantiate(Resources.Load("BossSources/Prefabs/FemaleTarget")) as GameObject; //生成目标人物    
        girlTarget.transform.position = bosspos;
     
        girlHips = girlTarget.GetComponentsInChildren<Transform>();//获取骨骼信息
    }

    /// <summary>
    /// 加载男孩
    /// </summary>
    void InstantiateBoy(Vector3 bosspos)
    {
        GameObject go = Instantiate(Resources.Load("BossSources/Prefabs/MaleModel")) as GameObject; //加载资源物体
        boySourceTrans = go.transform;//提前保存资源物体  便于在后面查找
        go.SetActive(false);//把资源人物模型隐藏了
        boyTarget = Instantiate(Resources.Load("BossSources/Prefabs/MaleTarget")) as GameObject;//生成目标人物  
        boyTarget.transform.position = bosspos;
     
        boyHips = boyTarget.GetComponentsInChildren<Transform>();//获取骨骼信息
    }

    /// <summary>
    /// 生成部位  并且把模型对应的部位信息存储起来
    /// </summary>
    /// <param name="souceTrans"></param>    资源model
    /// <param name="data"></param>    每个部位的所有数据  存放好几种类型的部位数据 字典存储 
    /// <param name="target"></param>    需要换装的主人公
    /// <param name="smr"></param>  主人公的字典信息
    void SaveData(Transform souceTrans,Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> data,GameObject target,
        Dictionary<string, SkinnedMeshRenderer> smr) {

            data.Clear();
            smr.Clear();

        if (souceTrans == null)
            return;

        SkinnedMeshRenderer[] parts = souceTrans.GetComponentsInChildren<SkinnedMeshRenderer>();// 遍历所有子物体有SkinnedMeshRenderer，进行存储
        foreach (var part in parts) {
            string[] names = part.name.Split('-');//字符串的拆分
            if (!data.ContainsKey(names[0])) { //每次遍历到一个新的部位   生成部位
                //骨骼下边生成对应的skm
                GameObject partGo = new GameObject();
                partGo.name = names[0];
                partGo.transform.parent = target.transform;

                smr.Add(names[0],partGo.AddComponent<SkinnedMeshRenderer>()); //把骨骼target身上的skm信息存储，部位只记录一次
                data.Add(names[0],new Dictionary<string,SkinnedMeshRenderer>());
            }
            data[names[0]].Add(names[1],part); //存储所有的skm信息到数据里边
        }

    }
    /// <summary>
    /// 控制换装函数
    /// </summary>
    /// <param name="part"></param>  部位
    /// <param name="num"></param>   这个部位中第几个
    /// <param name="data"></param>  所有不为数据
    /// <param name="hips"></param>  骨骼信息
    /// <param name="smr"></param>   主角部位数据
    /// <param name="str"></param>   
    void ChangeMesh(string part, string num, Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> data,
        Transform[] hips, Dictionary<string, SkinnedMeshRenderer> smr,string[,] str)
    { //传入部位，编号，从data里边拿取对应的skm 

        SkinnedMeshRenderer skm = data[part][num];//要更换的部位

        List<Transform> bones = new List<Transform>();
        foreach (var trans in skm.bones) { 
            foreach(var bone in hips){
                if (bone.name == trans.name) {
                    bones.Add(bone);
                    break;
                }
            }
        }
        //换装实现
        smr[part].bones = bones.ToArray();//绑定骨骼
        smr[part].materials = skm.materials;//替换材质
        smr[part].sharedMesh = skm.sharedMesh;//更换mesh

        SaveData(part,num,str);//更换后的部位数据存储  方便再次打开不会出错
    }

    void InitAvatarGirl() { //初始化骨架让他有mesh 材质 骨骼信息
        
        int length = Bossdata.girlStr.GetLength(0);//获得行数
        for (int i = 0; i < length; i++) {
            ChangeMesh(Bossdata.girlStr[i,0], Bossdata.girlStr[i,1],girlData,girlHips,girlSmr, Bossdata.girlStr); //穿上衣服
        }
    
    }

    void InitAvatarBoy()
    { //初始化骨架让他有mesh 材质 骨骼信息
        int length = Bossdata.boyStr.GetLength(0);//获得行数
        for (int i = 0; i < length; i++)
        {
            ChangeMesh(Bossdata.boyStr[i, 0], Bossdata.boyStr[i, 1], boyData, boyHips, boySmr, Bossdata.boyStr); //穿上衣服
        }

    }
    /// <summary>
    /// 人物切换
    /// </summary>
    /// <param name="part"></param>
    /// <param name="num"></param>
    public void OnChangePeople(string part,string num){
        if (nowcount == 0)
        { //girl
            ChangeMesh(part, num, girlData, girlHips, girlSmr, Bossdata.girlStr);
        }
        else {
            ChangeMesh(part, num, boyData, boyHips, boySmr, Bossdata.boyStr);
        }
    }

    public void SexChange(int index) { //性别转换，人物隐藏，面板隐藏
        if (index == 1)
        {//显示男孩  隐藏女孩
            nowcount = 1;
            boyTarget.SetActive(true);
            girlTarget.SetActive(false);
           // boyPanel.SetActive(true);
           // girlPanel.SetActive(false);
        }
        else if (index == 0)
        {
            //显示女孩 隐藏男孩
            nowcount = 0;
            boyTarget.SetActive(false);
            girlTarget.SetActive(true);
          //  boyPanel.SetActive(false);
            //girlPanel.SetActive(true);
        }
    }

    void SaveData(string part,string num,string[,] str)  { //更改数据
        int length = str.GetLength(0);//获得行数
        for (int i = 0; i < length; i++)
        {
            if (str[i, 0] == part) {
                str[i, 1] = num;

            }
        }
    }

}
