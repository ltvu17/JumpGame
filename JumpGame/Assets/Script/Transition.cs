using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public GameObject player;
    public Transform Transitioner;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cam1.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerExit2D(Collider2D other){
        Debug.Log("Enter");
        if(other.tag == "Player"  && rb.velocity.y > 0) {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }else   
        if(other.tag == "Player" && rb.velocity.y < 0) {
            cam2.SetActive(false);
            cam1.SetActive(true);
        }
    }
}
