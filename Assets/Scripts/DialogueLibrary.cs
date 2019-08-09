using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLibrary : MonoBehaviour
{
    [TextArea]
    public string[] ShowmanPresentation;
    private string HousePresentation1;
    public List<string> HousePresentations;

    void Start()
    {
        HousePresentations = new List<string>();
            }


  

}
