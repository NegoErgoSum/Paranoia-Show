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

    public void PlayCassete()
    {

    }
   
}
public class ConversationTape
    {

    ConversationTape(Conversation_Showman showman, Conversation_House house, Conversation_Candidates candidates)
    {
        ShowmanTape = showman;
        HouseTape = house;

    }
    #region HouseTape
    private Conversation_House HouseTape;
    private Dialogue HouseDialogue;
    private Dialogue HouseGreeting;

    private HousePresentation ShowmanGreetsHouse;
    #endregion

    #region ShowmanTape
    public Conversation_Showman ShowmanTape;

    private Reactions[] _ShowmanReactions;
    private ShowPresentations _ShowmanPresentation;
    private CandidatesPresentation _ShowmanGreetsCandidates;

    public Reactions[] ShowmanReactions
    {
        get
        {
            return _ShowmanReactions;
        }
        set
        {
            value = _ShowmanReactions;
        }
    }
    public ShowPresentations ShowmanPresentation
    {
        get
        {
            return _ShowmanPresentation;
        }
        set
        {
            value = _ShowmanPresentation;
        }
    }
    public CandidatesPresentation ShowmanGreetsCandidates
    {
        get
        {
            return _ShowmanGreetsCandidates;
        }
        set
        {
            value = _ShowmanGreetsCandidates;
        }
    }
    #endregion

    public Conversation_Candidates CandidatesTape;           
    private string BestCandidatePresentation;
    private string NeutralCandidatePresentation;
    private string WorstCandidatePresentation;

    void Start()
    {
        ShowmanPresentation = ShowmanTape.PossibleShowPresentations[Random.Range(0, ShowmanTape.PossibleShowPresentations.Length)];

        ShowmanGreetsHouse = ShowmanTape.PossibleHousePresentations[Random.Range(0, ShowmanTape.PossibleHousePresentations.Length)];
        HouseGreeting = HouseTape.dialogues[Random.Range(0, HouseTape.dialogues.Length)];

        ShowmanGreetsCandidates = ShowmanTape.ShowmanGreetsCandidates[Random.Range(0, ShowmanTape.ShowmanGreetsCandidates.Length)];

        BestCandidatePresentation = CandidatesTape.BestCandidateDialogues;


        ShowmanReactions = ShowmanTape.ShowmanReactions;


    }


}
    

