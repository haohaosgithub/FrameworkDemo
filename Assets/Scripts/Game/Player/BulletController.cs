using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Pool]
public class BulletController : MonoBehaviour
{
    Rigidbody rb;
    int attack;
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
        //rb.velocity = Vector3.zero;
        rb.AddForce(moveDir * moveForce);
        attack = atk;
        
        Invoke(nameof(Destory), 10); //发射10s后自动销毁
    }

    private void OnTriggerEnter(Collider other) //碰到物体时销毁
    {
        if (other.CompareTag("Player")) //忽略与Player的碰撞检测
        {
            return;
        }
        
        CancelInvoke(nameof(Destory)); //先取消自动销毁的延迟函数
        Destory();


        if (other.CompareTag("Monster"))
        {
            MonsterController mc = other.GetComponent<MonsterController>();
            mc.GetHit(attack);
        }
    }
    public void Destory()
    {
        rb.velocity = Vector3.zero; //放回对象池前先清理子弹的数据，让其重新加载出来后能够正确的赋值
        PoolManager.Instance.PushGameObj(gameObject);
    }
    

   

}
