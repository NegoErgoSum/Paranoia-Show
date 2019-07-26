using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   /// <summary>
   /// - El Conversation Manager será el encargado de gestionar una conversación dada, ejecutarla de manera correcta y mostrarla de modo coherente al jugador
   /// - Hereda de "Singleton" par que solo pueda haber una instancia de la clase a la vez
   /// </summary>
public class ConversationManager : Singleton<ConversationManager>
{
    protected ConversationManager() { }


    [Header("Cassette Slot")]
    [Header("##############")]
    public Conversation_Cassette CurrentCassete;
    public ConversationTape CurrentTape { get; set; }
    [Header("##############")]




    public Conversation_Showman ShowmanScript;
    public Conversation_House HouseScript;

    public Queue<Dialogue> dialogues;
    public Dialogue current;
    public string[] Current;


    private bool WaitingAnswer;

    void Update()
    {
        //       if (Input.GetKeyDown(KeyCode.Keypad1))
        //{
        //    SelectOption(1);
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad2))
        //{
        //    SelectOption(2);
        //}
    }
    void Start()
    {
        dialogues = new Queue<Dialogue>();
        //ShowmanScript.PlayTape();
    }

    public void StartConversation(Conversation_House conversation)
    {
        dialogues.Clear();   //Lo primero es resetear la cola de díálogos

        foreach (Dialogue dialogue in conversation.dialogues)//el bucle pasa una conversación dada (conversation.dialogues) a la Queue "dialogues" que se utilizará para ejecutar el protocolo
        {
            dialogues.Enqueue(dialogue);
        }  

        NextDialogue();
    }

    public void NextDialogue()
    {
        if (!WaitingAnswer)
        {
            if (dialogues.Count == 0)
            {
                Debug.Log("End conversation");
                return;

            }


            /*Al definir una variable como un "Dequeue" se asigna el primer valor en cola de la Queue a la variable a la vez que esta se quita de la cola, 
             * eliminando la necesidad de hacer un bucle que asigne valores y luego los quite de la lista para evitar que vuelvan a salir*/
            current = dialogues.Dequeue();


            DisplayDialogue();
        }
    }

    public void DisplayDialogue()
    {   if (current.options.Length!=0)
        {
            WaitingAnswer = true;
            foreach   (DialogueOption option in current.options)
            {
                Debug.Log(option.optionNumber + " - " + option.text);

            }
        }



    }
    public void SelectOption(int selected)
    {
        foreach(DialogueOption option in current.options)
        {
            if (option.optionNumber == selected)
            {
                EndConversation();
                if (option.response)
                {
                    StartConversation(option.response);
                }
            }
        }
    }
    public void EndConversation()
    {
        WaitingAnswer = false;

    }



}

public class ConversationTape
{

    public ConversationTape(Conversation_Showman showman, Conversation_House house, Conversation_Candidates candidates)
    {
        ShowmanTape = showman;
        HouseTape = house;
        CandidatesTape = candidates;

    }
    #region HouseTape
    private Conversation_House HouseTape;
    private Dialogue HouseDialogue;
    private Dialogue HouseGreeting;

    public HousePresentation ShowmanGreetsHouse;
    #endregion

    #region ShowmanTape
    public Conversation_Showman ShowmanTape;

    private Reactions[] _ShowmanReactions;
    private ShowPresentations _ShowmanPresentation;
    private CandidatesPresentation _ShowmanGreetsCandidates;

    public Reactions[] ShowmanReactions
    {
        get
        {
            return _ShowmanReactions;
        }
        set
        {
            value = _ShowmanReactions;
        }
    }
    public ShowPresentations ShowmanPresentation
    {
        get
        {
            return _ShowmanPresentation;
        }
        set
        {
            value = _ShowmanPresentation;
        }
    }
    public CandidatesPresentation ShowmanGreetsCandidates
    {
        get
        {
            return _ShowmanGreetsCandidates;
        }
        set
        {
            value = _ShowmanGreetsCandidates;
        }
    }
    #endregion

    public Conversation_Candidates CandidatesTape;
    private string BestCandidatePresentation;
    private string NeutralCandidatePresentation;
    private string WorstCandidatePresentation;

    void Start()
    {
        ShowmanPresentation = ShowmanTape.PossibleShowPresentations[Random.Range(0, ShowmanTape.PossibleShowPresentations.Length)];

        ShowmanGreetsHouse = ShowmanTape.PossibleHousePresentations[Random.Range(0, ShowmanTape.PossibleHousePresentations.Length)];
        HouseGreeting = HouseTape.dialogues[Random.Range(0, HouseTape.dialogues.Length)];

        ShowmanGreetsCandidates = ShowmanTape.ShowmanGreetsCandidates[Random.Range(0, ShowmanTape.ShowmanGreetsCandidates.Length)];


        BestCandidatePresentation = CandidatesTape.BestCandidateDialogues[Random.Range(0, CandidatesTape.BestCandidateDialogues.Length)].text;
        NeutralCandidatePresentation = CandidatesTape.NeutralCandidateDialogues[Random.Range(0, CandidatesTape.NeutralCandidateDialogues.Length)].text;
        WorstCandidatePresentation = CandidatesTape.WorstCandidateDialogues[Random.Range(0, CandidatesTape.WorstCandidateDialogues.Length)].text;


        ShowmanReactions = ShowmanTape.ShowmanReactions;


    }


}



