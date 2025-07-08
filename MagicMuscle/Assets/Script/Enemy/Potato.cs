using UnityEngine;

public class Potato : MonoBehaviour
{
    public float speed = 0.5f;
    private float sindeg = 0;
    public float sindegspeed = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    //sin波上に移動してくる
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 Pvec = new Vector3(0, 0, -10);
        Vector3 vec = Pvec - this.transform.position;//プレイヤーの位置から敵の位置を引く
        vec = vec.normalized;//正規化
        this.transform.position += vec * speed;//スピードをかける
    }
}
