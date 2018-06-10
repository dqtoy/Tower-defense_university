using UnityEngine;
using System.Collections;

public class Bossdata : MonoBehaviour {

    public static Bossdata Instance;
    //部位的名字，部位对应的skm
    public static string[,] girlStr= { { "eyes", "1" }, { "hair", "1" }, { "top", "1" }, { "pants", "1" }, { "shoes", "1" }, { "face", "1" } } ;
    public  static string[,] boyStr = { { "eyes", "1" }, { "hair", "1" }, { "top", "1" }, { "pants", "1" }, { "shoes", "1" }, { "face", "1" } };
    void Awake()
    {
        if (Instance !=null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

    }
   

    public static void Datadelete()
    {//重新赋值
     
         girlStr[0, 0] = "eyes"; girlStr[0, 1] = "1";
         girlStr[1, 0] = "hair"; girlStr[1, 1] = "1";
         girlStr[2, 0] = "top"; girlStr[2, 1] = "1";
         girlStr[3, 0] = "pants"; girlStr[3, 1] = "1";
         girlStr[4, 0] = "shoes"; girlStr[4, 1] = "1";
         girlStr[5, 0] = "face"; girlStr[5, 1] = "1";
         boyStr[0, 0] = "eyes"; boyStr[0, 1] = "1";
         boyStr[1, 0] = "hair"; boyStr[1, 1] = "1";
         boyStr[2, 0] = "top"; boyStr[2, 1] = "1";
         boyStr[3, 0] = "pants"; boyStr[3, 1] = "1";
         boyStr[4, 0] = "shoes"; boyStr[4, 1] = "1";
         boyStr[5, 0] = "face"; boyStr[5, 1] = "1";

    }

}
