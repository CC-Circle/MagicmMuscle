using UnityEngine;

public class LocalPosAncker : MonoBehaviour
{
    //
    public Vector3 startpos;
    //設定した値を使用するかどうか
    public bool isStartpos=true;
    [Tooltip("固定したい座標を選択")]
    public bool x;
    public bool y;
    public bool z;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isStartpos) {
            startpos = this.transform.position;
        }
        Vector3 my = this.transform.position;
        if (x)
        {
            this.transform.position = new Vector3(startpos.x, my.y, my.z);
            my = this.transform.position;
        }
        if (y)
        {
            this.transform.position = new Vector3(my.x, startpos.y, my.z);
            my = this.transform.position;
        }
        if (z)
        {
            this.transform.position = new Vector3(my.x, my.y, startpos.z);
            my = this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 my = this.transform.position;
        if (x)
        {
            this.transform.position = new Vector3(startpos.x,my.y,my.z);
            my = this.transform.position;
        }
        if (y)
        {
            this.transform.position = new Vector3(my.x,startpos.y, my.z);
            my = this.transform.position;
        }
        if (z)
        {
            this.transform.position = new Vector3(my.x, my.y, startpos.z);
            my = this.transform.position;
        }
    }
}
