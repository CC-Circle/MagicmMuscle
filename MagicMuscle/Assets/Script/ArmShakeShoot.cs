
using UnityEngine;
using DG.Tweening;

public class ArmShakeShoot : MonoBehaviour
{
    [Header("振動設定")]
    public float strength = 0.1f;   // 振動の大きさ

    public float interval = 0.05f;  // 1ステップの間隔（秒）
    public int loops = 20;          // 繰り返す回数

    public SliderCharge slider;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
        //DoJaggyShake();
    }

    private void Update()
    {
        
        if (Serial.ischarge) {
            DoJaggyShake();
        }

    }

    public void DoJaggyShake()
    {
        float shakeval = strength * slider.sliderPersent;
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < loops; i++)
        {
            // ランダムに飛ばす
            Vector3 offset = new Vector3(
                Random.Range(-shakeval, shakeval),
                Random.Range(-shakeval, shakeval),
                0f
            );

            seq.AppendCallback(() =>
            {
                transform.localPosition = startPos + offset;
            });
            seq.AppendInterval(interval);
        }

        // 最後に元の位置に戻す
        seq.AppendCallback(() =>
        {
            transform.localPosition = startPos;
        });
    }
}

