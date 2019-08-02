using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePersonality : MonoBehaviour
{

    public ShowCensus._HouseType HouseType;
    public ShowCensus._HouseType AntiHouseType;
    public ShowmanTape_ShowmanGreetsHouse Dialogues;
    public string WishedFamily;
    //public SpeechBubbleControl._Protocols Protocol;
    public string Name;
    public string[] FirstInterview;
    public string[] HouseFirstDialogue;
    
    //public enum _Personality
    //{
    //  NORMALITA, HIPERACTIVA
    //};
    public BehaviourLibrary.Behaviour Actitude;

    //public _Personality Personality;

    void Start()
    {
        int random = Random.Range(1, 4);
        switch (random)
        {
            case 1:
                {
                    Actitude = BehaviourLibrary.Behaviour.NORMAL;
                    break;
                }
            case 2:
                {
                    Actitude = BehaviourLibrary.Behaviour.NERVOUS;
                    break;
                }
            case 3:
                {
                    Actitude = BehaviourLibrary.Behaviour.ELEGANT;
                    break;
                }
        }
    }
}
