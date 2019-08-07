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


  

    public void SetName()
    {
        HousePresentation1 = " Hoy tenemos con nosotros a " + gameObject.GetComponent<Manager>().HousesOnStage[0].GetComponent<HousePersonality>().Name+" que viene a buscar a su pareja ideal";
        HousePresentations.Add(HousePresentation1);

    }
}
