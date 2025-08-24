using System;
using DG.Tweening;
using UnityEngine;

public class ArmAnimation : MonoBehaviour
{
    //アニメーションたち
    //アニメーションをさせる
    //private Animator animator;
    public string[] animationNames = {"isShoot"};
    //
    public ScreenToWorldShot scw;
    public float amplitude = 0.1f;  // 振幅（揺れの大きさ）
    public float frequency = 10f;   // 周波数（揺れる速さ）
    public Vector3 startPos; // Canvas の RectTransform
    public float fixX;

    //このオブジェクト
    public RectTransform target;

    //マッスルモード
     //振るスピード
    public float speed = 50f;
    //振る角度
    public float Angle = 90;

    //UIの位置を取得
    public Vector3 muscleTimepos;
    public RectTransform canvasRectTransform; // Canvas の RectTransform
    [HideInInspector]public static bool isRotating = false;
    private bool muscleTimeInit = false;
    void Start()
    {
        isRotating = false;
        //animator = GetComponent<Animator>();
        RectTransform rectTransform = (RectTransform)transform;
        
        //startPos = rectTransform.localPosition;
        
    }

    void Update()
    {


        if (!GameManager.muscleTime)
        {
            if (muscleTimeInit)
            {
                // ピボットを左上にした例 (0,1)
                target.pivot = new Vector2(0.5f, 0.5f);
                RectTransform RTransform = GetComponent<RectTransform>();
                RTransform.anchoredPosition = new Vector2(muscleTimepos.x, muscleTimepos.y);
                target.localEulerAngles = new Vector3(0, 0, 0);
                muscleTimeInit = false;
            }
            Vector2 localPoint;

            // スクリーン座標（マウス位置）を Canvas 内のローカル座標に変換
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,
                Input.mousePosition,
                null, // ← Overlay モードではカメラは null
                out localPoint
            );

            // 現在の Y/Z を維持し、X のみをマウスに合わせて更新
            RectTransform rectTransform = (RectTransform)transform;
            Vector3 currentPos = rectTransform.localPosition;
            //高さ
            float visualHeight = (rectTransform.rect.height * rectTransform.lossyScale.y) / 2;
            //太さ
            float visualWidth = ((rectTransform.rect.width * rectTransform.lossyScale.x) / 2) + fixX;

            rectTransform.localPosition = new Vector3(localPoint.x + visualWidth, startPos.y - visualHeight, startPos.z);

            if (startPos.y > localPoint.y)
            {
                rectTransform.localPosition = new Vector3(localPoint.x + visualWidth, localPoint.y - visualHeight, currentPos.z);
            }
        }
        else
        {
            muscleTime();
        }


        //if (scw.charge)
        //{
        //    float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        //    transform.localPosition = startPos + new Vector3(offset, 0, 0);
        //}

    }
    public void StartAnime() {
        if (!GameManager.muscleTime) {
            RectTransform rt = GetComponent<RectTransform>(); // 自分のRectTransformを取得
            rt.DOLocalMove(rt.localPosition + new Vector3(0, 50, 0), 0.1f)    // 現在位置にoffsetを加算した位置へ0.3秒で移動
              ;                          // 移動にイージングを適用（滑らかに減速）
        }



    }
    
    public void muscleTime() {



        muscleTimeInit = true;

        // ピボットを左上にした例 (0,1)
        target.pivot = new Vector2(0.5f, 0f);
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(muscleTimepos.x, muscleTimepos.y);

        // 現在の角度
        float currentAngle = target.localEulerAngles.z;

        // 角度を -180〜180 に正規化
        if (currentAngle > 180) currentAngle -= 360;

        // 補間して回転
        float newAngle = Mathf.MoveTowards(currentAngle, Angle, speed * Time.deltaTime);

        target.localEulerAngles = new Vector3(0, 0, newAngle);

        // 目標角度に到達したら停止
        if (Mathf.Approximately(newAngle,Angle))
        {
            isRotating = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SwingLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            SwingRight();
        }
    }

    public void SwingLeft()
    {
        Angle = 90f; // 左に90度
        isRotating = true;
    }

    public void SwingRight()
    {
        Angle = -90f; // 右に90度
        isRotating = true;
    }


}
