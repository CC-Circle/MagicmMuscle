using UnityEngine;

public class EmptySponner : MonoBehaviour
{
    //public GameObject gameObject;

    public int timemax;
    private int time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time++;
        if (timemax == time)
        {
            Instantiate(gameObject, this.transform.position, Quaternion.identity);
            time = 0;
        }
    }
}
