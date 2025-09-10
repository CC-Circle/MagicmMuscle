using UnityEngine;
using DG.Tweening;

public class ArmShakeShoot : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip shootClip;
    public float duration = 0.5f;     // 振動時間
    public float strength = 1.0f;     // 振動の強さ
    public int vibrato = 10;          // 振動回数
    public bool canShake = true;      // 振動できる条件フラグ

    private Tween shakeTween;

    void Update()
    {
        // スペースキーで振動を発生させる
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TryShake();
        //}
        if (Serial.isDegShake) { TryShake(); }

        if (Serial.isDegShake)
        {
            audiosource.PlayOneShot(shootClip);
        }

    }

    public void TryShake()
    {
        if (!canShake) return; // 条件チェック

        // 既存のTweenが残っていれば停止
        if (shakeTween != null && shakeTween.IsActive())
        {
            shakeTween.Kill();
        }

        // 振動アニメーションを開始
        shakeTween = transform.DOShakePosition(duration, strength, vibrato)
                              .SetEase(Ease.OutQuad);
    }

}
//using UnityEngine;
//using DG.Tweening;

//public class ArmShakeShoot : MonoBehaviour
//{
//    public float strength = 1f;    // 振動の強さ
//    public int vibrato = 10;       // 振動回数
//    public float duration = 0.5f;  // 1回の振動の時間

//    public bool isShaking = false; // 振動条件

//    private Tween shakeTween;

//    void Update()
//    {
//        if (Serial.isDegShake)
//        {
//            StartShaking();
//        }
//        else
//        {
//            StopShaking();
//        }
//    }

//    void StartShaking()
//    {
//        if (shakeTween == null || !shakeTween.IsActive())
//        {
//            shakeTween = transform.DOShakePosition(duration, strength, vibrato)
//                                  .SetLoops(-1, LoopType.Restart) // 無限ループ
//                                  .SetEase(Ease.Linear);
//        }
//    }

//    void StopShaking()
//    {
//        if (shakeTween != null && shakeTween.IsActive())
//        {
//            shakeTween.Kill(); // Tween を止める
//            shakeTween = null;
//            transform.localPosition = Vector3.zero; // 元の位置に戻す
//        }
//    }
//}
