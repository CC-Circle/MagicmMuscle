
using UnityEngine;
using DG.Tweening;

using static ArmControl;
using System.Collections.Generic;

public class SuperBulletChild : MonoBehaviour
{
    public DomtRotate domtrotate;
    private Quaternion initialRotation;

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

    private bool isShoot = false; // 玉が発射されたかどうか

    public Vector3 StartBig;


    void Start()
    {
        SetNearObject();

    }

    private void Update()
    {

        if (domtrotate.isShoot) {
            IsShoot();
        }

        // 発射後の移動処理
        if (isShoot)
        {
            // すでにRigidbodyがアタッチされていなければ追加
            if (GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();

                // 初期設定（必要に応じて調整）
                rb.mass = 0f;                 // 質量
                rb.linearDamping = 0f;                 // 空気抵抗
                rb.angularDamping = 0f;       // 回転の抵抗
                rb.useGravity = false;         // 重力を使うか
                rb.isKinematic = false;       // 物理演算を適用するか
                                              // 位置を固定（すべての軸）
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }

            if (shootAtNearestEnemy)
            {
                // 敵方向へ直進
                transform.position += moveDirection * speed * Time.deltaTime;
            }
            else
            {

                // スクリーン座標から前進
                zOffset += speed * Time.deltaTime;
                Vector3 inputWithZ = new Vector3(input.x, input.y, input.z + zOffset);
                screenObj = Camera.main.ScreenToWorldPoint(inputWithZ);
                transform.position = screenObj;
            }
        }
    }

    public void IsShoot() {
        // 発射処理（親を外すのはこの瞬間のみ）
        if (!isShoot)
        {

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
            GameObject nearestEnemy = FindRandomEnemy();
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
    // ランダムなEnemyを探す関数
    GameObject FindRandomEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> validEnemies = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            EnemyStatus enemystatus = enemy.GetComponent<EnemyStatus>();
            if (enemystatus != null && !enemystatus.dieing)
            {
                validEnemies.Add(enemy);
            }
        }

        if (validEnemies.Count == 0)
        {
            return null; // 敵がいなければ null を返す
        }

        int randomIndex = Random.Range(0, validEnemies.Count);
        return validEnemies[randomIndex];
    }


}
