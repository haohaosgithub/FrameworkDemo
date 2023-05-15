using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Pool]
public class BulletController : MonoBehaviour
{
    Rigidbody rb;

    /// <summary>
    /// 子弹初始化
    /// </summary>
    /// <param name="bornPoint">产生位置</param>
    /// <param name="moveDir">移动方向</param>
    /// <param name="moveForce">移动力</param>
    /// <param name="atk">攻击力</param>
    public void Init(Vector3 bornPoint,Vector3 moveDir,int moveForce,int atk)
    {

        transform.position = bornPoint;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(moveDir * moveForce);
        Invoke(nameof(Destory), 10); //发射10s后销毁
    }

    private void OnTriggerEnter(Collider other) //碰到物体时销毁
    {
        //print(other.name + "  " + other.tag);
        if (other.CompareTag("Player")) //忽略与Player的碰撞检测
        {
            return;
        }
        //print("trigger");
        Destory();


        if (other.CompareTag("Enemy"))
        {

        }
    }
    public void Destory()
    {
        rb.velocity = Vector3.zero;
        PoolManager.Instance.PushGameObj(gameObject);
    }
    

   

}
