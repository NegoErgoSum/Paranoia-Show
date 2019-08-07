using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CreateAssetMenu(fileName = "NewShowmanTape[ShowmanGreetsCandidates]", menuName = "ConversationSystem/ShowmanTape/3.1[ShowmanGreetsCandidates]", order = 3)]
[System.Serializable]      
public class ShowmanTape_ShowmanGreetsCandidates: ScriptableObject
{
    public KeyWords TextCommand;


    [Tooltip("Cada elemento de la array contiene una posible presentación de los candidatos (ej: dejemos que los candidatos se presenten...)")]
    [TextArea(3, 10)]
    public string[] PresentantionDialogue = new string[1];
}


//[System.Serializable]

//public class PresentationDialogues
//{
//    [TextArea(3, 10)]

//    public string PresentationDialogue;
//}
[System.Serializable]  
public class SetTape  
{
    private string[] ShowPresentations;
    private ShowmanTape_ShowmanReactions[] PossibleReactions;
    public SetTape(string[] showPresentations, ShowmanTape_ShowmanReactions[] reactions)
    {
        ShowPresentations = showPresentations;
    }

    private ShowmanTape_ShowmanReactions _ShowmanReaction;
    private string _ShowPresentation;

    public string ShowPresentation
    {
        get
        {
            return _ShowPresentation;
        }
        set
        {
            if (_ShowPresentation == null)
            {
                value = ShowPresentations[Random.Range(0, ShowPresentations.Length)];
            }
        }
    }
    public ShowmanTape_ShowmanReactions ShowmanReaction
    {
        get
        {
            return _ShowmanReaction;
        }
        set
        {
            if (_ShowPresentation == null)
            {
                value = PossibleReactions[Random.Range(0, PossibleReactions.Length)];
            }
        }
    }
}


[System.Serializable]
[CustomPropertyDrawer(typeof(SetNote))]
public class SetNote
{    
  
}