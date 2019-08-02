using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewShowmanTape[ShowPresentation]", menuName = "ConversationSystem/ShowmanTape/1.[ShowPresentation]", order = 1)]
[System.Serializable]
public class ShowmanTape_ShowPresentation : ScriptableObject
{
    [Tooltip("HouseName => /houseName \nHouseWish => /houseWish \nHouePersonality => /housePersonality")]
    public KeyWords Tutorial;


    [Tooltip("Cada elemento de la array contiene una posible presentación para el show, que puede contener una o varias líneas de texto")]   
    public ShowPresentation[] PossiblePresentations = new ShowPresentation[1];
}

[System.Serializable] 
public class ShowPresentation 
{

    [TextArea(3, 10)]
    [Tooltip("Cada elemento de la array es un ´enter´ que tiene que hacer el jugador para pasar a la siguiente línea de texto")]
    public string[] PresentantionDialogue = new string[1];
}