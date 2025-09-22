using UnityEngine;

public class LetusColision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OilControle.isOil = true;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OilControle.isOil = true;
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
