using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //プレイヤーに向かって移動する
    //移動スピード
    public float speed = 0.5f;
    void FixedUpdate()
    {
        Vector3 Pvec = new Vector3(0, 0, -10);
        Vector3 vec = Pvec - this.transform.position;//プレイヤーの位置から敵の位置を引く
        vec = vec.normalized;//正規化
        this.transform.position += vec * speed;//スピードをかける

    }
}
