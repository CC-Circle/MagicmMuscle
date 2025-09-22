using UnityEngine;

public class PlayerHItBox : MonoBehaviour
{
    CameraShake camerashake;
    Score score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camerashake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        score = GameObject.Find("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Hit();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Hit();
        }
    }
    private void Hit()
    {
        camerashake.Shake();
        score.ScoreRed(100);
        
    }
}
