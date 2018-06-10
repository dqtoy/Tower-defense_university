using UnityEngine;
using System.Collections;
/// <summary>
/// 相机控制
/// </summary>
public class CameraControl : MonoBehaviour {
    float MoveSpeed = 35f;//键盘控制前后左右速度
    float MouseSpeed = 100f;//滑轮速度


    void LateUpdate()
    {
        MoveCtrl();//控制  相机移动
    }
    

    /// <summary>
    /// 控制  相机移动
    /// </summary>
    private void MoveCtrl()
    {
        //使用滑轮  控制相机拉近拖远效果
        float mouse = -Input.GetAxis("Mouse ScrollWheel");//鼠标滑轮值
        //控制前后左右相机移动
        //x轴的移动
        float h = Input.GetAxis("Horizontal");
        //z轴的移动 
        float v = Input.GetAxis("Vertical");
        //使用世界坐标避免被旋转的物体移动时不是前后左右移动而是移动自身造成拉近远离的错误效果
        transform.Translate(new Vector3(h * MoveSpeed, mouse * MouseSpeed, v * MoveSpeed) * Time.deltaTime, Space.World);
        //控制视野移动范围
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30f, 20f), Mathf.Clamp(transform.position.y, 5f, 25f), Mathf.Clamp(transform.position.z, -20f, 35f));


    }

}
