using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public enum E_PlayerState
{
    Normal,
    Reload,
    GetHit,
    Die
}

//[DefaultExecutionOrder(200)]
public class PlayerController : SingletonMono<PlayerController>
{
    
    private E_PlayerState playerState;
    public E_PlayerState PlayerState
    {
        get
        {
            return playerState;
        }
        set
        {
            playerState = value;
            switch (playerState)
            {
                case E_PlayerState.Normal:
                    break;
                case E_PlayerState.Reload:
                    StartCoroutine(DoReload());
                    break;
                case E_PlayerState.GetHit:
                    break;
                case E_PlayerState.Die:
                    break;
                        
            }
        }
    }

    private CharacterController characterController;
    private Animator animator;
    LayerMask groundMask;
    private bool canShoot; //cd好了则置为true,即一个标志结合协程实现CD功能
    private Transform firePoint;
    #region 参数
    public float moveSpeed;
    public int bulletNum;
    public int maxBulletNum;
    public float shootInternal;
    public int bulletMoveForce;
    public int bulletAtk;
    #endregion

    protected override  void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
        groundMask = LayerMask.GetMask("Ground");
        animator = transform.Find("famaleModel").GetComponent<Animator>();
        canShoot = true;
        firePoint = transform.Find("firePoint").transform;
    }

    private void Start()
    {
        //设置数值，后续会从配置中读取，这里只是测试
        moveSpeed = 4;
        maxBulletNum = 60;
        bulletNum = maxBulletNum;
        shootInternal = 0.2f;
        bulletMoveForce = 1000;
        bulletAtk = 10;
    }
    private void Update()
    {
        StateOnUpdate();
    }

    //不同状态的update
    private void StateOnUpdate()
    {
        switch (playerState)
        {
            case E_PlayerState.Normal:  //正常状态下可以移动，射击，装弹
                Move();
                Shoot();
                Reload();
                break;
            case E_PlayerState.Reload: //装弹状态下可以移动
                Move();
                break;
            case E_PlayerState.GetHit:
                break;
            case E_PlayerState.Die:
                break;
        }
    }
    private void Move()
    {
        //移动
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        characterController.Move(new Vector3(x, -1, z) * Time.deltaTime * moveSpeed);

        //旋转
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hitInfo,1000,groundMask))
        {
            Vector3 targetPoint = hitInfo.point;
            Vector3 targetVector = targetPoint - transform.position;
            targetVector.y = 0; //同一平面上旋转

            targetVector = targetVector.normalized; //目标方向


            Quaternion q = Quaternion.LookRotation(targetVector);
            //Quaternion q = Quaternion.FromToRotation(transform.forward,targetVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime*20);
            if (targetVector.z < 0) //这里只是为了重置动画，不会影响到移动
            {
                x = -x;
                z = -z;
            }
        }

        //移动动画 (blend tree)
        animator.SetFloat("MoveX",x);
        animator.SetFloat("MoveZ", z);


    }

    private void Shoot()
    {
        if(Input.GetMouseButton(0) &&canShoot && bulletNum > 0)
        {
            StartCoroutine(DoShoot());
        }
    }
    
    private void Reload()
    {
        if (bulletNum < maxBulletNum && Input.GetKeyDown(KeyCode.R))
            PlayerState = E_PlayerState.Reload;
    }
    private IEnumerator DoShoot()
    {
        bulletNum -= 1;
        animator.SetTrigger("Shoot");
        AudioManager.Instance.PlayOneShot("Audio/Shoot/laser_01", transform);
        canShoot = false;
        BulletController bullet = ResManager.Instance.Load<BulletController>("Game/Bullet/Bullet");
        bullet.Init(firePoint.position, firePoint.forward, bulletMoveForce, bulletAtk);


        yield return new WaitForSeconds(shootInternal);
        canShoot = true;

        if(bulletNum <= 0) //没子弹了则自动装弹
        {
            PlayerState = E_PlayerState.Reload;
        }

    }
    
    private IEnumerator DoReload()
    {
        animator.SetBool("Reload",true);
        AudioManager.Instance.PlayOneShot("Audio/Shoot/Reload",transform);
        yield return new WaitForSeconds(1.9f);
        animator.SetBool("Reload", false);
        bulletNum = maxBulletNum;
        PlayerState = E_PlayerState.Normal;
    }

}
