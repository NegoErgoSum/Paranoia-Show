using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShowmanTape[ShowmanGreetsHouse]", menuName = "ConversationSystem/ShowmanTape/2.[ShowmanGreetsHouse]", order = 2)]
[System.Serializable]
public class ShowmanTape_ShowmanGreetsHouse : ScriptableObject
{
    [Tooltip("HouseName => /houseName \nHouseWish => /houseWish \nHouePersonality => /housePersonality")]
    public KeyWords Tutorial;

    [Tooltip ("Cada elemento de la array hace referencia a un tipo de casa (ej: medieval), solo debe existir un elemento de cada tipo en la array")]
    public HouseType[] ShowmanGreetsHouse = new HouseType[1];
}

[System.Serializable]
public class ShowmanQuestion 
{

    [TextArea(3, 10)]
    public string ShowmanAsk; 
    public HouseFirstSpeak HouseReponse;
}

[System.Serializable]
public class HouseType 
{
   public  ShowCensus._HouseType Type; 



    [Tooltip("Presentación de la casa (ej: Hoy tenemos con nosotros a Julia, una casa con mucha personalidad...)")]
    public PossibleHousePresentations[] ShowmanGreeting;

    [Tooltip ("Cada elemento de la array contiene una pregunta del showman y una respuesta de la casa")]
    public ShowmanQuestion[] ShowmanAskHouse = new ShowmanQuestion[1];


}

[System.Serializable]
public class PossibleHousePresentations 
{

    [TextArea(3, 10)]
    public string[] HousePresentations; 


}

[System.Serializable]
public class HouseFirstSpeak 
{

    [TextArea(3, 10)]
    public string HouseResponse;
}