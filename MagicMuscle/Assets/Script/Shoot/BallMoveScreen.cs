//using UnityEngine;

//public class BallMoveScreen : MonoBehaviour
//{
//    public AudioClip audioClip;
//    ScreenToWorldShot sts;
//    private Vector3 HD = new Vector3(1920, 1080, 0);
//    public GameObject death_effect;
//    public Vector3 input ;
//    public float powerscale = 0;
//    public int scalechange = 200;
//    private Vector3 screenObj;
//    public float speed = 0.2f;
//    public bool turanuki = false;

//    public bool isSuper = false;
//    public bool isChild = false;

//    public bool ishoming;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        if (!isChild)
//        {
//            screenObj = Camera.main.ScreenToWorldPoint(input);
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!isChild)
//        {
//            input.z += speed;
//            screenObj = Camera.main.ScreenToWorldPoint(input);
//            this.transform.position = screenObj;
//        }

//    }

//    private void OnCollisionEnter(Collision collision)
//    {

//        if (collision.gameObject.tag == "Enemy")
//        {
//            AudioSource.PlayClipAtPoint(audioClip ,new Vector3(0,1,-10));
//            Instantiate(death_effect, transform.position, Quaternion.identity);
//            if (!isSuper) {
//                this.gameObject.SetActive(false);
//            }
//        }
//        if (collision.gameObject.tag == "Ground")
//        {
//            Debug.Log("Ground"+this.transform.position);
//            Instantiate(death_effect, transform.position, Quaternion.identity);
//            this.gameObject.SetActive(false);
//        }
//    }
//}


using UnityEngine;

public class BallMoveScreen : MonoBehaviour
{
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

    void Start()
    {
        if (!isChild)
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
    }

    void Update()
    {
        if (!isChild)
        {
            if (shootAtNearestEnemy)
            {
                // 敵方向へ直進
                transform.position += moveDirection * speed * Time.deltaTime;
            }
            else
            {
                // 従来通りスクリーン座標から前進
                zOffset += speed*Time.deltaTime;
                Vector3 inputWithZ = new Vector3(input.x, input.y, input.z + zOffset);
                screenObj = Camera.main.ScreenToWorldPoint(inputWithZ);
                transform.position = screenObj;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(audioClip, new Vector3(0, 1, -10));
            Instantiate(death_effect, transform.position, Quaternion.identity);
            if (!isSuper)
            {
                gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground" + transform.position);
            Instantiate(death_effect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
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
            if (enemystatus != null) {
                if (distance < minDistance && !enemystatus.dieing)
                {
                    minDistance = distance;
                    nearest = enemy;
                }
            }

           
        }

        return nearest;
    }
}
