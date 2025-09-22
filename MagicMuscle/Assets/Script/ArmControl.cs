using DG.Tweening;
using UnityEngine;
using static DomtRotate;
using UnityEngine.Audio;

public class ArmControl : MonoBehaviour
{
    public SpriteFlash spriteflash;

    public SliderCharge slider;
    public AudioSource audiosource;
    //腕の球管理
    public ArmBulletManage armbulletmanage;

    //球の情報
    public DomtRotate domtrotate;

    public AudioClip shootClip, suka;
    
    public Animator animator;
    public string stateName = "ArmMove"; // アニメーションステート名
    public float sensorValue;            // 0〜90度のセンサー値をここに入れる

    private float currentNormalized;     // 現在の正規化値（0〜1）
    private float velocity;              // SmoothDamp用の速度

    BallSizeType ballType;

    public enum BallSizeType
    {
        Small,
        Medium,
        Large,
        Max
    }

    private void Start()
    {

        armbulletmanage.ChangeObject();
    }

    void Update()
    {
        //腕をふるアニメーション
        ArmRotateAnimation();
        //振った時の動作
        ArmShoot();
        StanBay();
        //ボールの大きさを更新
        armbulletmanage.DecideBallType();

        if (!Serial.isDeg) {
            //armbulletmanage.ChangeObject();
            //新しい球の情報を獲得
            SetCurrentBullet();
        }

        if (Serial.isDegShakeEnd) {
            
            armbulletmanage.ChangeObject();
        }

    }


    //ステッキのアニメーション
    public void ArmRotateAnimation() {
        if (Serial.isDegShake)
        {
            if (armbulletmanage.ballType == BallSizeType.Small) {
                audiosource.PlayOneShot(suka);
            }
            else
            {
                audiosource.PlayOneShot(shootClip);
            }
            


        }
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


    public void ArmShoot()
    {
        if (Serial.isDegShake)
        {
            try
            {
                Debug.Log("SHoot");
                DomtRotate domt =  transform.Find(armbulletmanage.childname).GetComponent<DomtRotate>();
                domt.IsShoot();
            }
            catch
            {
                Debug.Log("CantSHoot");
            }
        }
    }


    //ステッキを傾けていない場合
    public void StanBay()
    {

        if (!Serial.isDeg)
        {
            //armbulletmanage.SetBall();
        }
    }


    public void SetCurrentBullet() {
        try
        {
            domtrotate = transform.Find(armbulletmanage.childname).GetComponent<DomtRotate>();
        }
        catch {

        }
    }

    // 発射時にサイズからタイプを決定
    private void DecideBallType()
    {
        float sliderval = slider.sliderPersent;
        if (sliderval < 0.3f)
            ballType = BallSizeType.Small;
        else if (sliderval < 0.5f)
            ballType = BallSizeType.Medium;
        else if (sliderval < 0.7f)
            ballType = BallSizeType.Large;
        else ballType = BallSizeType.Max;
    }
}