using DG.Tweening;
using UnityEngine;

public class ArmControl : MonoBehaviour
{

    //public Animator animator;
    //public string stateName = "ArmMove"; // アニメーションステート名
    //public float sensorValue;            // 0〜90度のセンサー値をここに入れる

    //void Update()
    //{

    //    // センサー値を0〜1に正規化
    //    float normalized = Mathf.Clamp01(Serial.deg / 90f);

    //    // 特定のアニメーションを時間制御で再生
    //    animator.Play(stateName, 0, normalized);

    //    // Animatorを動かさない（その時点で固定する）
    //    animator.speed = 0;

    //}

    public Animator animator;
    public string stateName = "ArmMove"; // アニメーションステート名
    public float sensorValue;            // 0〜90度のセンサー値をここに入れる

    private float currentNormalized;     // 現在の正規化値（0〜1）
    private float velocity;              // SmoothDamp用の速度

    void Update()
    {
        // センサー値を0〜1に正規化
        float targetNormalized = Mathf.Clamp01(Serial.deg);

        // 補間して滑らかに近づける
        currentNormalized = Mathf.SmoothDamp(currentNormalized, targetNormalized, ref velocity, 0.05f);
        // 第4引数 (0.1f) = 平滑化の時間。小さくすると素早く反応、大きくするとゆっくり追従

        // 特定のアニメーションを時間制御で再生
        animator.Play(stateName, 0, currentNormalized);

        // Animatorを動かさない（その時点で固定する）
        animator.speed = 0;
    }

    public void Shake()
    {
        transform.DOShakePosition(10f, 0.1f);
    }
}