using UnityEngine;

public class ArmBulletManage : MonoBehaviour
{
    public GameObject bullet;
    public string childname;
    private Vector3 initialLocalPos;
    private Quaternion initialLocalRot;
    private Vector3 initialLocalScale;

     void Awake()
    {
        // 最初の子オブジェクトを探して、座標・回転・スケールを保存
        Transform child = transform.Find(childname);
        {
            initialLocalPos = child.localPosition;
            initialLocalRot = child.localRotation;
            initialLocalScale = child.localScale;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        bool hasChild = transform.Find(childname) != null;

        if (hasChild)
        {
            Debug.Log("子オブジェクトが存在する");
        }
        else
        {
            Debug.Log("子オブジェクトはいない");
            if (!Serial.isDeg)
            {
                // 新しいオブジェクトを生成
                GameObject newChild = Instantiate(bullet, transform);

                // 名前を揃える（任意）
                newChild.name = childname;

                //// 保存しておいた最初の座標・回転・スケールを適用
                //newChild.transform.localPosition = initialLocalPos;
                //newChild.transform.localRotation = initialLocalRot;
                //newChild.transform.localScale = initialLocalScale;

            }
        }
        
    }
}
