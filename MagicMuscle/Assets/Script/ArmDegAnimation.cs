using DG.Tweening;
using UnityEngine;

public class ArmControl : MonoBehaviour
{
    public Animator animator;
    public string stateName = "ArmMove"; // アニメーションステート名
    public float sensorValue;            // 0〜90度のセンサー値をここに入れる

    void Update()
    {
        
        // センサー値を0〜1に正規化
        float normalized = Mathf.Clamp01(Serial.deg / 90f);

        // 特定のアニメーションを時間制御で再生
        animator.Play(stateName, 0, normalized);

        // Animatorを動かさない（その時点で固定する）
        animator.speed = 0;
        
       
    }

    public void Shake()
    {
        transform.DOShakePosition(10f, 0.1f);
    }
}