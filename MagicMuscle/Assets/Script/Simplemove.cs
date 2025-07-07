using UnityEngine;

public class Simplemove : MonoBehaviour
{
    public Vector3 moveVec = new Vector3(0, 0, 1);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += moveVec;
    }
}
