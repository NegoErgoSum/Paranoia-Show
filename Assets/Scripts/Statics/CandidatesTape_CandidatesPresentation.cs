using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewCandidatesTape[CandidatesPresentation]", menuName ="ConversationSystem/CandidatesTape/3.2[CandidatesPresentation]", order =4)]
[System.Serializable]
public class CandidatesTape_CandidatesPresentation : ScriptableObject 
{
    [Tooltip("HouseName => /houseName \nHouseWish => /houseWish \nHouePersonality => /housePersonality")]
    public KeyWords Tutorial;


    [Tooltip("Cada elemento de la array representa a un tipo de casa (ej: medievalHouse) y contiene 3 arrays de posibles presentaciones (mejores, neutros y peores). Es interesantes que haya varias de cada para darle variedad")]
    public Crushing[] Feeling = new Crushing[1]; 
    
}

[System.Serializable]
/// <summary>
/// Con esta clase creamos un nuevo "tipo" de variable que utilizaremos para almacenar los datos de los diálogos y las posibles respuestas
/// </summary>
public class PossibleDialogues
{
    [TextArea(3, 10)]
    public string text;
    public ShowmanTape_ShowmanGreetsHouse response;


}

[System.Serializable]
/// <summary>
/// Con esta clase creamos un nuevo "tipo" de variable que utilizaremos para almacenar los datos de los diálogos y las posibles respuestas
/// </summary>
public class Crushing
{
    public ShowCensus._HouseType HouseType;
    public PossibleDialogues[] BestCandidateDialogues = new PossibleDialogues[1];
    public PossibleDialogues[] NeutralCandidateDialogues = new PossibleDialogues[1];
    public PossibleDialogues[] WorstCandidateDialogues = new PossibleDialogues[1];

}

public class PresentationDialogue
{
    [TextArea(3, 10)]   
    public string text;
    
               
    //Creando una variable de tipo "Conversation", generamos una nueva conversación especificada mediante código para adecuarse al texto del diálogo expuesto anteriormente; es decir, una respuesta
    //public Conversation_House response;
}
