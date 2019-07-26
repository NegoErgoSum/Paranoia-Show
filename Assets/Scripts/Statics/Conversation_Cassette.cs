using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName ="NewCassete", menuName ="ConverationSystem/Cassete", order =1)]
[System.Serializable]
public class Conversation_Cassette : ScriptableObject 
{
    public Conversation_Showman Showman;
    public Conversation_House House;
    public Conversation_Candidates Candidates;

  void Start ()
    {
        ConversationManager.Instance.CurrentTape = new ConversationTape(Showman, House, Candidates);
    }
}