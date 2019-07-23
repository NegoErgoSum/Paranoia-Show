using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName ="NewHouseTape", menuName ="ConverationSystem/HouseTape", order =2)]
[System.Serializable]
public class Conversation_House: ScriptableObject 
{

    //GUIContent icon = new GUIContent(ConversationManager.Instance.TapeIcon);
   



    //Variable tipo "Dialogue" (tipo creado más abajo)
    public Dialogue[] dialogues; 
}


[System.Serializable]
/// <summary>
/// Con esta clase creamos un nuevo "tipo" de variable que utilizaremos para almacenar los datos de los diálogos y las posibles respuestas
/// </summary>
public class Dialogue
{
    [TextArea(3,10)]
    public string Sentence;
    public DialogueOption[] options; 
        }

[System.Serializable]
/// <summary>
/// Clase que genera el tipo de variable "DialogueOption"
/// </summary>
public class DialogueOption
{
    [TextArea(3, 10)]   
    public string text;
    public int optionNumber;
               
    //Creando una variable de tipo "Conversation", generamos una nueva conversación especificada mediante código para adecuarse al texto del diálogo expuesto anteriormente; es decir, una respuesta
    public Conversation_House response;
}
