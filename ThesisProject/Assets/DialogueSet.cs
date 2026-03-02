using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueSet", menuName = "Dialogue Set")]

public class DialogueSet : ScriptableObject
{

    [SerializeField] List<Dialogue_Asset> dialogueList;

    [SerializeField] private int currentDialogueIndex = 0;
    private int previousIndex = 0;

    [SerializeField] private bool isRandom = false;

    public Dialogue_Asset GetNextDialogue()
    {
        if(dialogueList.Count == 0)
        {
            Debug.LogWarning("DialogueSet is empty!");
            return null;
        }

        if(isRandom)
        {
            currentDialogueIndex = Random.Range(0, dialogueList.Count);
        }
        else
        {
            previousIndex = currentDialogueIndex;
            currentDialogueIndex = (currentDialogueIndex + 1) % dialogueList.Count;
        }
        return dialogueList[previousIndex];
    }
    
}