using UnityEngine;
using DG.Tweening;  // ★ DOTweenを使うのに必要
using UnityEngine.Audio;

public class Ending_SqeeseAnimation : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip clip;
    public Animator animator;
    private Tween shakeTween; // 振動アニメーションを管理用に保持

    void Update()
    {
        if (Serial.ischarge)
        {
            PlaySound();
            animator.SetBool("IsSqeeze", true);

            // すでに振動していなければ開始
            if (shakeTween == null || !shakeTween.IsActive())
            {
                shakeTween = transform.DOShakePosition(
                    duration: 0.1f,   // 1回の振動時間
                    strength: new Vector3(30f, 30f, 0), // 揺れの強さ
                    vibrato: 10,     // 振動の細かさ
                    randomness: 90,  // ランダム度合い
                    snapping: false, // グリッドにスナップしない
                    fadeOut: true    // 時間経過で減衰するか
                ).SetLoops(-1, LoopType.Restart); // 無限ループで繰り返し
            }
        }
        else
        {
            audiosource.Stop();
            animator.SetBool("IsSqeeze", false);

            // 振動を止める
            if (shakeTween != null && shakeTween.IsActive())
            {
                shakeTween.Kill();
                shakeTween = null;
                transform.localPosition = Vector3.zero; // 元の位置に戻す
            }
        }
    }
    public void PlaySound()
    {
        if (!audiosource.isPlaying) // 再生中でなければ
        {
            audiosource.clip = clip;
            audiosource.Play();
        }
    }
}
