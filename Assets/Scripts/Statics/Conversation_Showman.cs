using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="NewShowmanTape", menuName ="ConverationSystem/ShowmanTape", order =4)]
[System.Serializable]
public class Conversation_Showman : ScriptableObject 
{
    [Tooltip("/houseName => nombre de la casa")]
    public SetNote Tutorial;

    //[TextArea(3,10)]
    public ShowPresentations[] PossibleShowPresentations = new ShowPresentations[1];
    public HousePresentation[] PossibleHousePresentations = new HousePresentation[1];

    public CandidatesPresentation[] ShowmanGreetsCandidates;
    public Reactions[] ShowmanReactions;     
}
[System.Serializable]      
public class ShowPresentations
{

    [TextArea(3, 10)]
    public string[] PresentantionDialogue = new string[1];
}
[System.Serializable]      
public class CandidatesPresentation
{

    [TextArea(3, 10)]
    public string[] PresentantionDialogue = new string[1];
}
[System.Serializable]      
public class HousePresentation
{

    [TextArea(3, 10)]
    public string[] HousePresentationDialogue = new string[1];
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
    private Reactions[] PossibleReactions;
    public SetTape(string[] showPresentations, Reactions[] reactions)
    {
        ShowPresentations = showPresentations;
    }

    private Reactions _ShowmanReaction;
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
    public Reactions ShowmanReaction
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
public class Reactions
{
    [TextArea(3, 10)]
    public string[] Funny, Surprised, Angry, Sad;
}       

[System.Serializable]
[CustomPropertyDrawer(typeof(SetNote))]
public class SetNote
{    
  
}