
using UnityEngine;

public class Potato : MonoBehaviour
{
    private Animator animator;
    public float speed = 0.5f;
    private float sindeg = 0;
    public float sindegspeed = 2;
    public int scoreadd = 100;
    //カメラを振動させる
    private CameraShake camerashake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        animator = GetComponent<Animator>();
        camerashake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            animator.SetBool("isDeath", true);
           
            Destroy(collision.gameObject);
            //Destroy(this.gameObject);

        }
        if (collision.gameObject.tag == "Player")
        {

            camerashake.Shake();
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            camerashake.Shake();
            Destroy(transform.parent.gameObject);
        }
    }
    void Ondeath()
    {
        gameObject.SetActive(false);
    }
}
