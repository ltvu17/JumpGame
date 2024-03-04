using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.background, 1, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Jump"))
        {
            AudioManager.instance.PlaySound(AudioManager.instance.jump, 1f);
        }
    }
}
