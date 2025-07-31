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

    //UIの位置を取得
    public RectTransform canvasRectTransform; // Canvas の RectTransform
    void Start()
    {
        //animator = GetComponent<Animator>();
        RectTransform rectTransform = (RectTransform)transform;
        
        startPos = rectTransform.localPosition;
        
    }

    void Update()
    {
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
        float visualWidth = ((rectTransform.rect.width * rectTransform.lossyScale.x) / 2)+fixX;

        rectTransform.localPosition = new Vector3(localPoint.x+visualWidth, startPos.y,startPos.z);

        if (startPos.y> localPoint.y - visualHeight)
        {
            rectTransform.localPosition = new Vector3(localPoint.x + visualWidth, localPoint.y - visualHeight, currentPos.z);
        }

        //if (scw.charge)
        //{
        //    float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        //    transform.localPosition = startPos + new Vector3(offset, 0, 0);
        //}
      
    }
    public void StartAnime() {
        //RectTransform rt = GetComponent<RectTransform>(); // 自分のRectTransformを取得
        //rt.DOLocalMove(rt.localPosition + new Vector3(0, 50, 0), 0.1f)    // 現在位置にoffsetを加算した位置へ0.3秒で移動
        //  ;                          // 移動にイージングを適用（滑らかに減速）
       
    }
    public void EndAnime() {
       
    }


}
