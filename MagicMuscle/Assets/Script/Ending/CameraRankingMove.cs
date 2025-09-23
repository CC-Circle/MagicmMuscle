using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CameraRankingMove : MonoBehaviour
{
    public static Vector3 target; // 移動先のターゲット位置
    public float speed = 5f; // 移動速度
    public float sppedup = 0.1f;

    public Vector3 zoomPos;
    public float zoomspeed;
    

    public bool isMoveEnd = false;
    public static bool finaldomino = false;
    void Start()
    {
        finaldomino = false;
        

    }

    public void MoveCamera()
    {
        if (target != null)
        {
            //Debug.Log("transform:"+transform.position.x);
            //Debug.Log("target:" + target.position.x);

            Vector3 direction = (target - transform.position).normalized;
            if (transform.position.x < target.x)
            {
                speed+= sppedup;
            }
            else
            {
                speed-= sppedup;
            }

            if (transform.position.x >= target.x - 0.01f )
            {
                speed = 0;
                isMoveEnd = true;

            }
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0);
        }
        if (isMoveEnd)
        {

            Debug.Log("Zoom");
            Vector3 target_zoom = target + zoomPos + new Vector3 (0,this.transform.position.y,0);
            transform.position = Vector3.Lerp(
                transform.position,
                target_zoom,
                Time.deltaTime * zoomspeed
            );
        }
    }
    void Update()
    {
        Debug.Log(speed);
        // ターゲット位置に向かって移動
        //MoveCamera();


    }
}