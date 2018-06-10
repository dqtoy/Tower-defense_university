using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {

    [HideInInspector]//隐藏属性
    public float datamoney = 300;//拥有的金钱



    public static DataManager DataInstance;//单例模式
    void Awake()
    {
        if (DataInstance==null)
        {
            DataInstance = this;
        }
        else
        {
            Destroy(DataInstance);
        }
    }


    void Start () {
	



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
