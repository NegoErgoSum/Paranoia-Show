using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHouseTape[HouseQuestion]", menuName = "ConversationSystem/HouseTape/4.[HouseQuestion]", order = 5)]
[System.Serializable]
public class HouseTape_HouseQuestion : ScriptableObject
{
    public KeyWords TextCommand;

    [Tooltip("Cada elemento de la array representa un tipo de casa (ej: medievalHouse), y las posibles preguntas que puede hacer. Solo ha de haber un elmento con el mismo tipo de casa en la array")]
    public HouseQuestions[] HouseQuestion = new HouseQuestions[1];

}

[System.Serializable]
public class   HouseQuestions
{
    public ShowCensus._HouseType HouseType;
    [Tooltip("Cada elemento de la array contiene una posible pregunta con sus posibles respuestas, correspondientes a los tres posibles candidatos (bueno, neutro y malo)")]
    public PossibleQuestions[] PossibleQuestions;
}

[System.Serializable]
public class PossibleQuestions
{
    [TextArea(3, 10)]
    public string HouseAsking;
    [Header("CandidatesResponses")]
    [TextArea(3, 10)]
    public string BestCandidateResponse;
    [TextArea(3, 10)]
    public string NeutralCandidateResponse;
    [TextArea(3, 10)]
    public string WorstCandidateResponse;             


}

