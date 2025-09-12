
using UnityEngine;
using static ArmControl;

public class ArmBulletManage : MonoBehaviour
{
    public GameObject bullet,bulletMidium,bulletLarge,bulletMax;
    public DomtRotate CurrentObject;
    public string childname;
    public SliderCharge slider;
    public BallSizeType ballType;

    public bool isChange = false;
    //ボールをセットする
    public void SetBall() {
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
                CurrentObject = newChild.GetComponent<DomtRotate>();

            }
        }
    }

    //オブジェクトを変更する
    public void ChangeObject()
    {
       
        GameObject curObject;
        switch (ballType)
        {
            case BallSizeType.Small:
                curObject = bullet;
                break;
            case BallSizeType.Medium:
                curObject = bulletMidium;
                break;
            case BallSizeType.Large:
                curObject = bulletLarge;
                break;
            case BallSizeType.Max:
                curObject = bulletMax;
                break;
            default:
                curObject = null;
                break;
        }
        //現在のオブジェクトと、握力に応じたオブジェクトが同じ場合
        if (CurrentObject != curObject) {
            //子オブジェクトを全て削除
            DeleteAllChildren();
            // オブジェクトを変更
            GameObject newChild = Instantiate(curObject, transform);

            // 名前を揃える（任意）
            newChild.name = childname;
            CurrentObject = newChild.GetComponent<DomtRotate>();
        }

       
    }

    // このオブジェクトの子オブジェクトを全削除
    public void DeleteAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public GameObject getCurrentBallType()
    {
        switch (ballType){
            case BallSizeType.Small:

                break;
            case BallSizeType.Medium:

                break;
            case BallSizeType.Large:
                
                break;
        }

        return null;
    }

    // 発射時にサイズからタイプを決定
    public void DecideBallType()
    {
        
        float sliderval = slider.sliderPersent;
        BallSizeType curtent = ballType;

        if (sliderval < 0.3f) {
            ballType = BallSizeType.Small;
        }
        else if (sliderval < 0.6f)
        {
            ballType = BallSizeType.Medium;
        }
        else if (sliderval < 0.9f)
        {
            ballType = BallSizeType.Large;
        }
        else ballType = BallSizeType.Max;

        //ボールタイプに変化があった場合呼び出される
        if (curtent != ballType)
        {
            //待機状態の場合
            if (!Serial.isDeg)
            {
                ChangeObject();
            }
        }
 
    }

}
