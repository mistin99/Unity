using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0,2.0f,-3.0f);
    public Vector3 rotation = new Vector3(15, 0, 0);

    public bool IsMoving { set; get; }
  

    void LateUpdate()
    {
        if (!IsMoving)
        {
            return;
        }
        Vector3 targetPos = player.position + offset;
        targetPos.x = 0;
       // targetPos.y = 2;
        transform.position = Vector3.Lerp(transform.position,targetPos,0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(rotation),0.1f);
    }
}
