using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShowmanTape_ShowmanReactions", menuName = "ConversationSystem/ShowmanTape/[ShowmanReactions]", order = 4)]   
[System.Serializable]
public class ShowmanTape_ShowmanReactions : ScriptableObject
{
    [TextArea(3, 10)]
    public ShowmanReactions[] ReactionsPackage;
}

[System.Serializable]
public class ShowmanReactions 
{
    [TextArea(3, 10)]
    public string[] Funny, Surprised, Angry, Sad;
}
