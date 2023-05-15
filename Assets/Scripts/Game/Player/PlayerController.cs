using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    Normal,
    Reload,
    GetHit,
    Die
}

//[DefaultExecutionOrder(200)]
public class PlayerController : SingletonMono<PlayerController>
{
    
    private PlayerState playerState;
    public PlayerState PlayerState
    {
        get
        {
            return playerState;
        }
        set
        {
            switch (playerState)
            {
                case PlayerState.Normal:
                    playerState = PlayerState.Normal; 
                    break;
                case PlayerState.Reload:
                    playerState = PlayerState.Reload;
                    break;
                case PlayerState.GetHit:
                    playerState = PlayerState.GetHit;
                    break;
                case PlayerState.Die:
                    playerState = PlayerState.Die; 
                    break;
                        
            }
        }
    }

    private CharacterController characterController;
    private Animator animator;
    LayerMask groundMask;

    public float moveSpeed;
    protected override  void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
        groundMask = LayerMask.GetMask("Ground");
        animator = transform.Find("famaleModel").GetComponent<Animator>();
    }

    private void Start()
    {
        //设置数值，后续会从配置中读取，这里只是测试
        moveSpeed = 4;

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
            case PlayerState.Normal:
                Move();
                break;
            case PlayerState.Reload:
                
                break;
            case PlayerState.GetHit:
                
                break;
            case PlayerState.Die:
                
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
            targetVector.y = 0.5f;  
            
            targetVector = targetVector.normalized; //目标方向
            if(targetVector.z < 0) //这里只是为了重置动画，不会影响到移动
            {
                x = -x;
                z = -z;
            }

            Quaternion q = Quaternion.LookRotation(targetVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime*20);
        }

        //移动动画 (blend tree)
        animator.SetFloat("MoveX",x);
        animator.SetFloat("MoveZ", z);


    }
}
