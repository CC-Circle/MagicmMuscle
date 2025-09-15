
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
            GetDamageFromBullet(collision.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            camerashake.Shake();
            Destroy(transform.parent.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GetDamageFromBullet(other.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            camerashake.Shake();
            Destroy(transform.parent.gameObject);
        }
    }

    public void GetDamageFromBullet(GameObject DamageObject)
    {
        BulletState bsm = DamageObject.GetComponent<BulletState>();
        if (bsm != null)
        {
            status.TakeDamage(bsm.Attack);
        }
    }
}
