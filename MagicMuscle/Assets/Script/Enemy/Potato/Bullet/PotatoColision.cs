using UnityEngine;

public class PotatoColision : MonoBehaviour
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
