using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public float move;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            move = -1;

        }else if (Input.GetKey(KeyCode.RightArrow))
        {
            move = 1;
        }else move = 0;
        transform.Translate(Vector3.right * speed * move * Time.deltaTime);
    }
}
