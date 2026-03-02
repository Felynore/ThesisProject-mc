using UnityEngine;
[CreateAssetMenu(fileName = "Dialogue_Asset", menuName = "Dialogue Asset")]
public class Dialogue_Asset : ScriptableObject
{
    public Info[] dialogueInfo;

    [System.Serializable]
    public class Info
    {
        [TextArea(4, 8)] public string dialogue;
    }

}