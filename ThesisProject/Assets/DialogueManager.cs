using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private bool inDialogue;    //  Is the player currently in a dialogue?
    private bool textIsTyping;  //  Is text currently being typed out?

    private Queue<Dialogue_Asset.Info> dialogueQueue; //   A queue of the dialogue strings
    private string fullText; //  The full text of the current dialogue line

    [SerializeField] private float textDelay = 0.1f; //  The delay between each letter being typed out.

    [SerializeField] TMP_Text dialogueText; //  The TextMeshPro text component that displays the dialogue text.
    [SerializeField] GameObject dialogueBox;    //  The dialogue box GameObject that contains the dialogue UI.

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        dialogueQueue = new Queue<Dialogue_Asset.Info>();


    }

    private void OnInteract()
    {
        if (inDialogue)
        {
            DequeueDialogue();
        }
    }

    public void QueueDialogue(Dialogue_Asset dialogue)
    {
        if (inDialogue)
        {
            return;
        }

        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = false; // Disable player input during dialogue
        inDialogue = true;
        dialogueBox.SetActive(true); // Show the dialogue box
        dialogueQueue.Clear(); // Clear any existing dialogue in the queue
        foreach (Dialogue_Asset.Info line in dialogue.dialogueInfo)
        {
            dialogueQueue.Enqueue(line); // Enqueue each line of dialogue from the SO_Dialogue scriptable object

        }
        DequeueDialogue(); // Start the dialogue by dequeuing the first line
    }

    public void DequeueDialogue()
    {
        if (textIsTyping)
        {
            CompleteText(); // If text is currently being typed, complete it immediately
            StopAllCoroutines(); // Stop the typing coroutine
            textIsTyping = false; // Set the typing flag to false
            return;
        }

        if (dialogueQueue.Count == 0)
        {
            EndDialogue(); // If there are no more lines in the queue, end the dialogue
            return;
        }

        Dialogue_Asset.Info info = dialogueQueue.Dequeue(); // Dequeue the next line of dialogue
        fullText = info.dialogue; // Set the full text to the dialogue string from the SO_Dialogue.Info
        dialogueText.text = ""; // Clear the dialogue text component
        StartCoroutine(TypeText(info)); // Start the coroutine to type out the text
    }

    private void CompleteText()
    {
        dialogueText.text = fullText; // Set the dialogue text to the full text immediately
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box
        inDialogue = false; // Set the inDialogue flag to false
        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = true; // Re-enable player input
    }

    private IEnumerator TypeText(Dialogue_Asset.Info info)
    {
        textIsTyping = true;    // Set the typing flag to true
        foreach (char c in info.dialogue.ToCharArray())
        {
            yield return new WaitForSeconds(textDelay); // Wait for the specified delay between each letter
            dialogueText.text += c; // Add the next character to the dialogue text
        }
        textIsTyping = false;   // Set the typing flag to false once the full text has been typed out
    }

}