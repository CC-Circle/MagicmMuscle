using UnityEngine;
using DG.Tweening;
using static DomtRotate;

public class ArmShakeShoot : MonoBehaviour
{
    public float duration = 0.5f;     // 振動時間
    public float strength = 1.0f;     // 振動の強さ
    public int vibrato = 10;          // 振動回数
    public bool canShake = true;      // 振動できる条件フラグ

    private Tween shakeTween;

    void Update()
    {
        
        if (Serial.isDegShake) { TryShake(); }
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

