using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePersonality : MonoBehaviour
{
    public string WishedFamily;
    //public SpeechBubbleControl._Protocols Protocol;
    public string Name;
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
                    Actitude = BehaviourLibrary.Behaviour.ELEGANT;
                    break;
                }
            case 2:
                {
                    Actitude = BehaviourLibrary.Behaviour.ELEGANT;
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
