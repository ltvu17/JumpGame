using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Header("choice UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    public bool answer;

    private static DialogueManager instance;
    private Story currentStory;
    private int correctAnswer;

    private bool flag;

    public bool dialogueIsPlaying { get; private set; }
    public bool trueAnswer { get; set; }
    private void Awake()
    {
        if (!instance == null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }
    public static DialogueManager GetInstance()
    {
        if(instance == null)
        {
            instance = new DialogueManager();
        }
        return instance;
    }
    private void Start()
    {
        dialogueIsPlaying = false;
        trueAnswer = false; 
        dialoguePanel.SetActive(false);

        //get all of the choice text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }
    public void EnterDialogueMode(TextAsset inkJSON, Action onDialogueFinished)
    {
        currentStory = new Story(inkJSON.text);

        // Đọc giá trị của correctAnswer từ file ink
        object correctAnswerObject = currentStory.variablesState["correctAnswer"];
        if (correctAnswerObject != null && correctAnswerObject is int)
        {
            correctAnswer = (int)correctAnswerObject;
            Debug.Log("Correct answer: " + correctAnswer);
        }
        else
        {
            Debug.LogError("Correct answer is not defined or is not an integer.");
        }

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        // Tạo một hàm callback mới để gọi khi câu chuyện hoàn thành
        Action onComplete = () =>
        {
            dialogueIsPlaying = false; // Đặt biến cờ khi câu chuyện kết thúc
            dialoguePanel.SetActive(false); // Ẩn dialogue panel khi kết thúc câu chuyện
            onDialogueFinished?.Invoke(); // Gọi callback để thông báo rằng câu chuyện đã kết thúc
        };

        // Tiếp tục câu chuyện với hàm callback mới
        ContinueStory(onComplete);
    }
    private void Update()
    {
        if(flag)
        {
            trueAnswer = true;
        }
        if(flag== false)
        {
            trueAnswer=false;
        }
        //return right away if dialogue isn't playing
        if(!dialogueIsPlaying)
        {
            return;
        }
        //handle continuing to the next line in the dialogue when submit is pressed
        if (Input.GetKeyDown(KeyCode.Space)){
            ContinueStory();
        }
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f); 
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    /* private void ContinueStory()
     {
         if (currentStory.canContinue)
         {
             dialogueText.text = currentStory.Continue();
         }
         else
         {
             StartCoroutine(ExitDialogueMode());
         }
     }*/
    private void ContinueStory(Action onDialogueFinished = null)
    {
        if (currentStory.canContinue)
        {
            //set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            //display choices, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            // Nếu không còn câu chuyện nào để tiếp tục, kiểm tra xem có callback không
            if (onDialogueFinished != null)
            {
                onDialogueFinished.Invoke(); // Gọi callback nếu có
            }
            else
            {
                StartCoroutine(ExitDialogueMode()); // Nếu không, tiến hành thoát khỏi chế độ câu chuyện mặc định
            }
        }
    }
    public void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        // defensive check make  sure our UI can support  the number of choice coming in
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given that UI can support. Number of given: " + currentChoices.Count);
        }
        int index = 0;
        //enable  and initilize  the choice up to the amount of choice for this line of dialogue
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //go through the remaining choices the UI supports and make sure that's are hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFisrtChoice());
    }
    private IEnumerator SelectFisrtChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    /*    public void MakeChoice(int choiceIndex)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);

            // Lấy đáp án được chọn từ câu chuyện
            string chosenAnswer = currentStory.currentChoices[choiceIndex].text;
            Debug.Log(chosenAnswer);
    *//*        // Hiển thị câu trả lời đã chọn và kiểm tra xem có đúng không
            if (chosenAnswer == correctAnswer.ToString())
            {
                Debug.Log("Bạn đã chọn đúng!");
            }
            else
            {
                Debug.Log("Bạn đã chọn sai! Đáp án đúng là: " + correctAnswer);
            }*//*
        }*/
    public void MakeChoice(int choiceIndex)
    {

        Debug.Log(choiceIndex);
        string chosenAnswer = currentStory.currentChoices[choiceIndex].text;


        Debug.Log(chosenAnswer);
        // Hiển thị câu trả lời đã chọn và kiểm tra xem có đúng không
        if ((choiceIndex + 1).ToString() == correctAnswer.ToString())
        {
            flag = true;
            trueAnswer = true;
            currentStory.ChooseChoiceIndex(choiceIndex);
            Debug.Log("Bạn đã chọn đúng!");
        }
        else
        {
            flag = false;
            trueAnswer = false;
            currentStory.ChooseChoiceIndex(choiceIndex);
            Debug.Log("Bạn đã chọn sai! Đáp án đúng là: " + correctAnswer);
        }
    }
}
