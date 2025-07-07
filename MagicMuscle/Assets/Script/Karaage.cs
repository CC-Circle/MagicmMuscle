using UnityEngine;
public class Karaage : MonoBehaviour
{
    public float speed = 0.5f;
    private float sinMove = 0;
    public float sinSpeed = 1;
    private float sindeg = 0;
    public float sindegspeed = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sindeg+=sindegspeed;
        sinMove = Mathf.Sin(sindeg)*sinSpeed;
        //RB = this.GetComponent<Rigidbody>();
        //PlayerCharaControle MV;//呼ぶスクリプトにあだ名をつける
        //GameObject obj = GameObject.Find("Player");//Circleというゲームオブジェクトを探す
        //if (obj != null)
        //{
            //MV = obj.GetComponent<PlayerCharaControle>();//スクリプトを取得
            //Vector3 Pvec = new Vector3(MV.transform.position.x, transform.position.y, MV.transform.position.z);//プレイヤーの座標を保存
            Vector3 Pvec = new Vector3(0, 1, -10);
            Vector3 vec = Pvec - this.transform.position;//プレイヤーの位置から敵の位置を引く
            vec = vec.normalized;//正規化
            this.transform.position += vec * speed+new Vector3(0,sinMove,0);//スピードをかける
        //}
    }
}
