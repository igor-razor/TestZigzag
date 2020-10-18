using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private float _rot_speed = 5.0f;
    private float speedUP = 10.0f;
    private bool flagUP = false;

    public void Fupper()
    {
        //_rot_speed *= _rot_speed;
        flagUP = true;
        Destroy(gameObject, 3.0f);
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    //transform.rotation = Quaternion.AngleAxis(10.0f,new Vector3(0.0f,1.0f,0.0f));
    //}

    // Update is called once per frame
    void Update()
    {
        if (!flagUP) { transform.Rotate(0, _rot_speed, 0); }
        if (flagUP) { transform.position = new Vector3(transform.position.x, transform.position.y + speedUP * Time.deltaTime, transform.position.z); }
    }
}//public class Crystal : MonoBehaviour
