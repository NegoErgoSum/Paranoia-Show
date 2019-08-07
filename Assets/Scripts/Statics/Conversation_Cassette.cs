using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName ="NewCassete", menuName ="ConversationSystem/Cassete", order =0)]
[System.Serializable]
public class Conversation_Cassette : ScriptableObject
{

    [Header("SH0W PRESENTATION")]
    [Header ("##### PHASE 1 #####")]
    public ShowmanTape_ShowPresentation ShowPresentation;

    [Header("SHOWMAN GREETS HOUSE")]
    [Header("##### PHASE 2 #####")]
    public ShowmanTape_ShowmanGreetsHouse ShowmanGreetsHouse;

    [Header("CANDIDATES PRESENTATION")]
    [Header("##### PHASE 3 #####")]
    public ShowmanTape_ShowmanGreetsCandidates ShowmanGreetsCandidates;
    public CandidatesTape_CandidatesPresentation CandidatesPresentation;

    [Header("HOUSE ASKING CANDIDATES")]
    [Header("##### PHASE 4 #####")]
    public HouseTape_HouseQuestion HouseAskingCandidates;





   

  //void Start ()
  //  {
  //      ConversationManager.Instance.CurrentTape = new ConversationTape(ShowmanTape, HouseTape, Candidates);
  //  }
}