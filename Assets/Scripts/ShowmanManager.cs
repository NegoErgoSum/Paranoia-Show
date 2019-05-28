using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowmanManager:MonoBehaviour
{ 

    NpcController Npc;

    [TextArea]
    public string[] DialogueS;


      void Start()
    {
        Npc = gameObject.GetComponent<NpcController>();
        
    }
    public void MicUp()
    {
        Npc.Anim = gameObject.GetComponent<Animator>();
        Npc.Anim.SetBool("CanTalk", true);
        base.StartCoroutine(Npc.TalkDialogue(DialogueS));
    }
  

}
