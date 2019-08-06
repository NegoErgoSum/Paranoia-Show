using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCensus : MonoBehaviour
{
   public enum _HouseType
    {
      MedievalHouse, StoneHouse, ImpressionistHouse, SurrealistHouse, WoodHouse
    };
    public _HouseType HouseType;


    public enum _CandidatesFeeling
    {
        BEST, NEUTRAL, WORST
    };
    public _HouseType CandidatesFeeling;
}
