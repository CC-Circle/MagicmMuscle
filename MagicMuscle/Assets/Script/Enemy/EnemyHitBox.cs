using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    //カメラを振動させる
    private CameraShake camerashake;
    private Animator animator;
    public int scoreadd = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = transform.parent.gameObject.GetComponent<Animator>();
        camerashake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //アニメーションをする
            animator.SetBool("isDeath", true);
            //スコアを追加
            Score.score += scoreadd;
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "Player")
        {
            camerashake.Shake();
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {

    }
    void Ondeath()
    {
        gameObject.SetActive(false);
    }
}
