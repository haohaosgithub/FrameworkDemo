using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[DefaultExecutionOrder(201)]
public class CameraController : MonoBehaviour
{
    private PlayerController playerController;
    private Vector3 offset;
    
    public void Start()
    {
        playerController = PlayerController.Instance;
        offset = new Vector3(0.8f, 10, -6);
    }
    public void LateUpdate()
    {
        //transform.position = playerController.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, playerController.transform.position + offset,Time.deltaTime) ;
    }
}
