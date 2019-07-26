using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewCandidatesTape", menuName ="ConverationSystem/CandidatesTape", order =3)]
[System.Serializable]
public class Conversation_Candidates : ScriptableObject 
{
    //Variable tipo "Dialogue" (tipo creado más abajo)
    public CandidateTape[] BestCandidateDialogues; 
    public CandidateTape[] NeutralCandidateDialogues; 
    public CandidateTape[] WorstCandidateDialogues; 
}

[System.Serializable]
/// <summary>
/// Con esta clase creamos un nuevo "tipo" de variable que utilizaremos para almacenar los datos de los diálogos y las posibles respuestas
/// </summary>
public class CandidateTape
{
    [TextArea(3, 10)]
    public string text;
    public Conversation_House response;


}

[System.Serializable]
/// <summary>
/// Clase que genera el tipo de variable "DialogueOption"
/// </summary>
public class NeutralCandidate
{
     public CandidateTape[] BestCandidateDialogues; 
    public CandidateTape[] NeutralCandidateDialogues; 
    public CandidateTape[] WorrstCandidateDialogues; 
}
[System.Serializable]
/// <summary>
/// Clase que genera el tipo de variable "DialogueOption"
/// </summary>
public class WorstCandidate
{
    [TextArea(3, 10)]   
    public PresentationDialogue PresentationLine;
               
    //Creando una variable de tipo "Conversation", generamos una nueva conversación especificada mediante código para adecuarse al texto del diálogo expuesto anteriormente; es decir, una respuesta
    public Conversation_House response;
}
    [System.Serializable]
/// <summary>
/// Clase que genera el tipo de variable "DialogueOption"
/// </summary>
public class PresentationDialogue
{
    [TextArea(3, 10)]   
    public string text;
    
               
    //Creando una variable de tipo "Conversation", generamos una nueva conversación especificada mediante código para adecuarse al texto del diálogo expuesto anteriormente; es decir, una respuesta
    public Conversation_House response;
}
