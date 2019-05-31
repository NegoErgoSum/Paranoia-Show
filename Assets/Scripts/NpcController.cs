using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public Person Identificator;
    private Animator _Anim;
    private string _Type;
    private Sprite _NpcSprite;
    private Npc_SpecialComponent SpecialComponent;


    public float TalkingDelay;
    private GameObject TextFramework;
    private Sprite[] TextFrameworkSprites;
    private Sprite[] LightBox;
    private GameObject TextBox;

    private Manager ManagerRef;

    private bool TalkingDialogue;
    [TextArea]
    public string[] Dialogue;

    public Sprite NpcSprite { get; set; }

    public string SpecialComponentName;
    public Animator Anim
    {
        get
        {
            return this._Anim;
        }
        set
        {
            _Anim = value;
        }
    }
    public string NpcType
    {
        get
        {
            return this._Type;
        }
        set
        {
            _Type = value;
        }
    }

    void Update()
    {
    }
    public void ShowPapers()
    {
        Debug.Log("Name: " + Identificator.Name + "; Birthyear: " + Identificator.BirthYear);
        Debug.Log("Type: " + Identificator.Type);

    }
    void Start()
    {

        Anim = gameObject.GetComponent<Animator>();

        NpcSprite = gameObject.GetComponent<Image>().sprite;

        SpecialComponent = gameObject.GetComponent<Npc_SpecialComponent>();


        TextBox = GameObject.Find("Brain").GetComponent<Manager>().TextBoxRef;
        TextFramework = GameObject.Find("Brain").GetComponent<Manager>().TextFrameworkRef;
        LightBox = GameObject.Find("Brain").GetComponent<Manager>().TextFrameWorkSpritesRef;
        Npc_SpecialComponent specialComponent= gameObject.AddComponent<Npc_SpecialComponent>();
        //Debug.Log(Identificator.Name);
        gameObject.SetActive(false);        
    }
    public void Refresh(Person npc, int interlocutor, bool shadow)
    {              gameObject.GetComponent<Animator>().runtimeAnimatorController = npc.Appearance.GetComponent<Animator>().runtimeAnimatorController;

        gameObject.GetComponent<Animator>().SetBool("Shadow", shadow);
NpcSprite = npc.Appearance.GetComponent<Image>().sprite;
        NpcType = npc.Type;

        GameObject.Find("Brain").GetComponent<Manager>().SpecialInterlocutorsComponent[interlocutor].gameObject.name = "Item";

        
        gameObject.GetComponent<Npc_SpecialComponent>().SwitchType(this.NpcType);
        //gameObject.GetComponent<Npc_SpecialComponent>().NpcItem=



    }

    public void Reset()
    {                        
        gameObject.GetComponent<Image>().sprite= null;
        gameObject.GetComponent<NpcController>().NpcType= null;
        gameObject.GetComponent<Animator>().runtimeAnimatorController= null;
        gameObject.SetActive(false); 
    }
    public void Talk(string[] dialogue)
    {
        StartCoroutine(TalkDialogue(Dialogue));
    }

    IEnumerator WaitingInput()
    {
        while (!Input.GetKeyDown(KeyCode.KeypadEnter) || TalkingDialogue)
        {

            yield return null;


        }
    }
    public IEnumerator TalkDialogue(string[] dialogue)
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            StartCoroutine(Talking(dialogue[i]));
            yield return StartCoroutine(WaitingInput());

        }
    }

    IEnumerator Talking(string line)
    {
        Anim.SetBool("Talk", true);
        TalkingDialogue = true;
        TextFramework.GetComponent<Image>().sprite = LightBox[1];
        for (int i = 0; i <= line.Length; i++)
        {
            string currentText = line.Substring(0, i);
            TextBox.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(TalkingDelay);
        }
        TextFramework.GetComponent<Image>().sprite = LightBox[0];
        Anim.SetBool("Talk", false);
        TalkingDialogue = false;

        if (Random.Range(0, 101) > 50)
        {
            Anim.SetTrigger("Action1");
        }
        StopCoroutine("Talking");
    }
}
