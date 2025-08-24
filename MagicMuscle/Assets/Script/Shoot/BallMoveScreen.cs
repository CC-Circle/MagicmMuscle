using UnityEngine;

public class BallMoveScreen : MonoBehaviour
{
    public AudioClip audioClip;
    ScreenToWorldShot sts;
    private Vector3 HD = new Vector3(1920, 1080, 0);
    public GameObject death_effect;
    public Vector3 input ;
    public float powerscale = 0;
    public int scalechange = 200;
    private Vector3 screenObj;
    public float speed = 0.2f;
    public bool turanuki = false;

    public bool isSuper = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(this.transform.position);
        //Serial.strong = Mathf.Abs(Serial.strong);
        //if ((powerscale / scalechange) < 20)
        //{
        //    this.transform.localScale *= powerscale / scalechange;
        //}
        //else {
        //    this.transform.localScale *= 10;
        //}

        //if (powerscale > 400)
        //{
        //    turanuki = true;
        //}
        screenObj = Camera.main.ScreenToWorldPoint(input);
    }

    // Update is called once per frame
    void Update()
    {
        input.z+=speed;
        screenObj = Camera.main.ScreenToWorldPoint(input);
        this.transform.position = screenObj;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            AudioSource.PlayClipAtPoint(audioClip ,new Vector3(0,1,-10));
            Instantiate(death_effect, transform.position, Quaternion.identity);
            if (!isSuper) {
                this.gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Ground"+this.transform.position);
            Instantiate(death_effect, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
