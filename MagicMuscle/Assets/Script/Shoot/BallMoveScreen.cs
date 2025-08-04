using UnityEngine;
using System.Collections;
using UnityEditor.Rendering.LookDev;

public class BallMoveScreen : MonoBehaviour
{
    ScreenToWorldShot sts;
    private Vector3 HD = new Vector3(1920, 1080, 0);
    public GameObject death_effect;
    public Vector3 input ;
    public float powerscale = 0;
    public int scalechange = 200;
    private Vector3 screenObj;
    public float speed = 0.2f;
    public bool turanuki = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Serial.strong = Mathf.Abs(Serial.strong);
        if ((powerscale / scalechange) < 20)
        {
            this.transform.localScale *= powerscale / scalechange;
        }
        else {
            this.transform.localScale *= 10;
        }
        
        if (powerscale > 400)
        {
            turanuki = true;
        }
        screenObj = Camera.main.ScreenToWorldPoint(input);
    }

    // Update is called once per frame
    void Update()
    {
        input.z+=speed;
        screenObj = Camera.main.ScreenToWorldPoint(input);
        this.transform.position = screenObj;
    }
    //void OnDisable()
    //{

    //    StartCoroutine(SpawnNextFrame());
    //    //削除された時にエフェクトを
    //    //Instantiate(death_effect, transform.position, Quaternion.identity);
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Bullet")
    //    {
    //        Instantiate(death_effect, transform.position, Quaternion.identity);
    //        Destroy(other.gameObject);
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(death_effect, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
    IEnumerator SpawnNextFrame()
    {
        yield return null; // 1フレーム待つ
        Instantiate(death_effect, transform.position, Quaternion.identity);
    }
}
