using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc_SpecialComponent : MonoBehaviour
{
    public string NpcType;
    public NpcController Npc;
    public Sprite ShowmanItem;



    public enum Character
    {
        SHOWMAN, GOLAZO

    };
    public Character CurrentNpc;

    //public void RefreshSpecialNpcComponent()
    //{
    //    NpcType = gameObject.GetComponent<NpcController>().NpcType;
    //    SwitchType(NpcType);
    //}

    void Start()
    {
        Npc = gameObject.GetComponent<NpcController>();
        NpcType = gameObject.GetComponent<NpcController>().NpcType;
        //SwitchType(NpcType);

        


    }
         public void SwitchType(string type)
    {
        switch (type)
        {
            case "Showman":
                {
                   
                    CurrentNpc = Character.SHOWMAN;
                    gameObject.GetComponent<Animator>().SetTrigger("FirstDialogue");

                    break;
                }
            case "Contestant_Golazo":
                {
                    CurrentNpc = Character.GOLAZO;
                    gameObject.GetComponent<Animator>().SetTrigger("FirstDialogue");


                    break;
                }
            default:
                {
                    CurrentNpc = Character.GOLAZO;
                    gameObject.GetComponent<Animator>().SetTrigger("FirstDialogue");


                    break;
                }
        }
    }
    public void MicUp()
    {
        if (CurrentNpc!= Character.SHOWMAN)
        {
            return;
        }
        Npc.Anim = gameObject.GetComponent<Animator>();
        Npc.Anim.SetBool("CanTalk", true);
        Npc.StartCoroutine(Npc.TalkDialogue(Npc.Dialogue));
    }
}
