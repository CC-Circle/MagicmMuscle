
using UnityEngine;
using DG.Tweening;

using static ArmControl;

public class DomtRotate : MonoBehaviour
{
    private Quaternion initialRotation;
    public Quaternion startRotate;
    public Vector3 Startpos;

    public SliderCharge slider;

    private Vector3 baseScale;   // スライダーで決まる基準の大きさ
    private Vector3 animOffset;  // DOTweenのアニメーションによる補正値

    //ボールの大きさ
    public BallSizeType ballType;

    public AudioClip audioClip;
    public GameObject death_effect;
    public float speed = 10f;
    public bool isSuper = false;
    public bool isChild = false;

    [Header("発射モード切替")]
    public bool shootAtNearestEnemy = false; // trueなら敵方向、falseなら従来の直進

    private Vector3 moveDirection;
    private Vector3 screenObj;
    public Vector3 input;   // 従来の入力用
    private float zOffset = 0f;

    [HideInInspector]public bool isShoot = false; // 玉が発射されたかどうか
    private Vector3 shootPos;

    public Vector3 StartBig;

    void Start()
    {
        this.transform.localPosition = Startpos;
        slider = GameObject.Find("barsmaster").GetComponent<SliderCharge>();
        initialRotation = startRotate;
        baseScale = StartBig * 0;
        transform.localScale = baseScale;
        //// DOTweenでスケールアニメーション設定
        //DOTween.Sequence()
        //    .AppendCallback(() =>
        //    {
        //        animOffset = Vector3.zero;
        //        DOTween.To(() => animOffset, x => animOffset = x,
        //                   new Vector3(0.1f, 0.1f, 0f), 0.1f)
        //               .SetEase(Ease.InOutSine)
        //               .SetLoops(-1, LoopType.Yoyo);
        //    });

        SetNearObject();
    }

    private void Update()
    {
        // スライダーの値を基準スケールに反映

        if (slider.IsSingleMode)
        {
            if (!isShoot)
            {
                baseScale = StartBig * slider.sliderPersent;
            }
            else
            {
                baseScale = StartBig;
            }
        }
        else {
            baseScale = StartBig;
        }

       

        // 最終スケール = 基準サイズ + アニメーション分
        transform.localScale = baseScale + animOffset;
        if (baseScale.x == 0)
        {
            transform.localScale = Vector3.zero;
        }



        // 発射後の移動処理
        if (isShoot)
        {

            if (shootAtNearestEnemy)
            {
                // 敵方向へ直進
                transform.position += moveDirection * speed * Time.deltaTime;
            }
            else
            {
                float pixelX = 1920 / 2;

                float pixelY = 1080 / 2;

                // スクリーン座標から前進
                zOffset += speed * Time.deltaTime;
                Vector3 inputWithZ = new Vector3(pixelX,pixelY,zOffset);
                screenObj = Camera.main.ScreenToWorldPoint(inputWithZ)-new Vector3(0,0,shootPos.z);
                transform.position += Vector3.forward *speed*Time.deltaTime;
            }
        }
    }

    public void IsShoot() {
        // 発射処理（親を外すのはこの瞬間のみ）
        if (!isShoot)
        {
            shootPos = this.transform.position;
            slider.InitAllSliderValue();

            // 親を外す前のワールドスケールを記録
            Vector3 worldScale = transform.lossyScale;

            // 親を解除
            transform.SetParent(null);

            // 親が外れた後の localScale を worldScale に合わせる
            transform.localScale = worldScale;

            // 解除後のスケールを基準スケールに設定
            StartBig = transform.localScale;

            isShoot = true;
            SetNearObject();
        }
    }


    void LateUpdate()
    {
        transform.rotation = initialRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(audioClip, new Vector3(0, 1, -10));
            Instantiate(death_effect, transform.position, Quaternion.identity);
            if (!isSuper)
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(death_effect, transform.position, Quaternion.identity);
            if (!isSuper)
            {
                this.gameObject.SetActive(false);
            }
            //gameObject.SetActive(false);
        }
    }

    public void SetNearObject()
    {
        if (shootAtNearestEnemy)
        {
            // --- 敵方向に発射 ---
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                moveDirection = (nearestEnemy.transform.position - transform.position).normalized;
            }
            else
            {
                moveDirection = transform.forward; // 敵がいなければ前へ
            }
        }
        else
        {
            // --- 従来のスクリーン座標ベース ---
            screenObj = Camera.main.ScreenToWorldPoint(input);
        }
    }

    // 最寄りのEnemyを探す関数
    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPos, enemy.transform.position);
            EnemyStatus enemystatus = enemy.GetComponent<EnemyStatus>();
            if (enemystatus != null && !enemystatus.dieing)
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = enemy;
                }
            }
        }
        return nearest;
    }

}
