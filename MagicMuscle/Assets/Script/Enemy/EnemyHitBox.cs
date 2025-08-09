using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    private EnemyStatus status;
    //カメラを振動させる
    private CameraShake camerashake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        status = GetComponentInParent<EnemyStatus>();
        
        camerashake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            status.TakeDamage(10);
        }
        if (collision.gameObject.tag == "Player")
        {
            camerashake.Shake();
            Destroy(transform.parent.gameObject);
        }
    }
}
