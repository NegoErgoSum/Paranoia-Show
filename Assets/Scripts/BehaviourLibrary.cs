using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourLibrary : MonoBehaviour
{
    public enum Behaviour
    {
      NORMAL, NERVOUS, ELEGANT
    };
    public Behaviour Actitude;


   public BehaviourLibrary(Behaviour actitude)
    {
        Actitude = actitude;
    }
}
