using UnityEngine;


// moves the sun and moon light

public class MBSSun : MonoBehaviour
{

    [SerializeField] float vRotSp;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,vRotSp * Time.deltaTime,0));
    }
}
