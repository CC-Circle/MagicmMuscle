using UnityEngine;
using System.Collections;
public class PotatoMove : MonoBehaviour
{
    //プレイヤーに向かって移動する
    //移動スピード
    public float speed = 0.5f;
    public int RandomeLnegth = 40;
    private int randomInt;
    public int waitTime;
    private bool move = false;
    private void Start()
    {
        randomInt = Random.Range(-RandomeLnegth, RandomeLnegth); // 0〜9 の整数
        StartCoroutine(WaitShoot());
    }
    void FixedUpdate()
    {
        Vector3 Pvec = new Vector3(randomInt, 0, -10);
        Vector3 vec = Pvec - this.transform.position;//プレイヤーの位置から敵の位置を引く
        vec = vec.normalized;//正規化
        this.transform.position += vec * speed;//スピードをかける
        if (move)
        {
           
        }
    }
    IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(waitTime);
        move = true;
    }
}
