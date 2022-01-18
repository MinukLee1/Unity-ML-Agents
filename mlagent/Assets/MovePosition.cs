using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha5) == true)
        {
            gameObject.transform.position = new Vector3(3, 1, 8);
        }

        if (Input.GetKey(KeyCode.Alpha6) == true)
        {
            gameObject.transform.position = new Vector3(-6,1, 8);
        }

        if (Input.GetKey(KeyCode.Alpha7) == true)
        {
            gameObject.transform.position = new Vector3(2, 1, -2);
        }

        if (Input.GetKey(KeyCode.Alpha8) == true)
        {
            gameObject.transform.position = new Vector3(-2, 1, -2);
        }
         
    }
}