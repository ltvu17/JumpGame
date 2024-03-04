using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [SerializeField] private GameObject trigger;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;


    private bool playerInrange;
    public Rigidbody2D rb;

    private bool choiceMade = false;


    private void Awake()
    {
        playerInrange = false;
        visualCue.SetActive(false);
    }
    private void Update()
    {
        if (playerInrange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            rb.velocity = Vector3.zero;
            // Pass a callback function to EnterDialogueMode
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON, OnDialogueFinished);
            trigger.SetActive(false);

            /*            if (DialogueManager.GetInstance().trueAnswer == true)
                        {
                            trigger.SetActive(false);
                        }
                        else
                        {
                            trigger.SetActive(true);
                        }*/
        }
/*        else
        {
            visualCue.SetActive(false);
        }*/
    }

    // Callback function to be called when dialogue is finished
    private void OnDialogueFinished()
    {
        // Set visualCue to false after dialogue finishes
        visualCue.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerInrange=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerInrange=false;
        }
    }


    /*    private void Update()
    {
        if (playerInrange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
        //    if(Input.GetKeyDown(KeyCode.E)) {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                visualCue.SetActive(false);
        //    }

        }
        else
        {
            visualCue.SetActive(false);
        }
    }*/
}
