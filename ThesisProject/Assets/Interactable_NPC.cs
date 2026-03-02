using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A class to hold on to and handle dialogue of a given character.
/// NOTE: THE GAMEOBJECT REQUIRES A CAPSULE COLLIDER (OR OTHER COLLIDER) TO FUNCTION.
/// </summary>
public class Interactable_NPC : MonoBehaviour, F_IInteractable
{
    //  The class holds either a singular dialogue asset, or a set of dialogues to cycle through.

    [SerializeField] Dialogue_Asset dialogue;
    [SerializeField] DialogueSet dialogueSet;
    [SerializeField] bool singularDialogue = true;

    public void Interact()
    {
        Debug.Log("Interacted with NPC");

        if (singularDialogue)
        {
            DialogueManager.Instance.QueueDialogue(dialogue);

        }
        else
        {
            Dialogue_Asset nextDialogue = dialogueSet.GetNextDialogue();
            if (nextDialogue != null)
            {
                DialogueManager.Instance.QueueDialogue(nextDialogue);
            }
        }
    }
}
