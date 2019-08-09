using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// - El Conversation Manager será el encargado de gestionar una conversación dada, ejecutarla de manera correcta y mostrarla de modo coherente al jugador
/// - Hereda de "Singleton" par que solo pueda haber una instancia de la clase a la vez
/// </summary>
public class ConversationManager : Singleton<ConversationManager>
{
    //    protected ConversationManager() { }


    [Header("Cassette Slot")]
    [Header("_______________")]
    public Conversation_Cassette InsertCassette;
    public ConversationTape CurrentCassette { get; set; }
    [Header("_______________")]



    public ShowPresentation P1_ShowPresentation;

    public string[] P2_ShowmanPresentHouse;
    public ShowmanQuestion P2_ShowmanQuestion;

    public string[] P3_ShowmanPresentCandidates;
    public string P3_BestCandidatePresentation;
    public string P3_NeutralCandidatePresentation;
    public string P3_WorstCandidatePresentation;


    public string[] NextCheckedText { get; set; }


    void Start()
    {
        CurrentCassette = new ConversationTape(InsertCassette);
    }


    //    //public Conversation_Showman ShowmanScript;
    //    public Conversation_House HouseScript;

    //    public Queue<Dialogue> dialogues;
    //    public Dialogue current;
    //    public string[] Current;


    //    private bool WaitingAnswer;

    //    void Update()
    //    {
    //        //       if (Input.GetKeyDown(KeyCode.Keypad1))
    //        //{
    //        //    SelectOption(1);
    //        //}
    //        //else if (Input.GetKeyDown(KeyCode.Keypad2))
    //        //{
    //        //    SelectOption(2);
    //        //}
    //    }
    //    void Start()
    //    {
    //        dialogues = new Queue<Dialogue>();
    //        //ShowmanScript.PlayTape();
    //    }

    //    public void StartConversation(Conversation_House conversation)
    //    {
    //        dialogues.Clear();   //Lo primero es resetear la cola de díálogos

    //        foreach (Dialogue dialogue in conversation.dialogues)//el bucle pasa una conversación dada (conversation.dialogues) a la Queue "dialogues" que se utilizará para ejecutar el protocolo
    //        {
    //            dialogues.Enqueue(dialogue);
    //        }  

    //        NextDialogue();
    //    }

    //    public void NextDialogue()
    //    {
    //        if (!WaitingAnswer)
    //        {
    //            if (dialogues.Count == 0)
    //            {
    //                Debug.Log("End conversation");
    //                return;

    //            }


    //            /*Al definir una variable como un "Dequeue" se asigna el primer valor en cola de la Queue a la variable a la vez que esta se quita de la cola, 
    //             * eliminando la necesidad de hacer un bucle que asigne valores y luego los quite de la lista para evitar que vuelvan a salir*/
    //            current = dialogues.Dequeue();


    //            DisplayDialogue();
    //        }
    //    }

    //    public void DisplayDialogue()
    //    {   if (current.options.Length!=0)
    //        {
    //            WaitingAnswer = true;
    //            foreach   (DialogueOption option in current.options)
    //            {
    //                Debug.Log(option.optionNumber + " - " + option.text);

    //            }
    //        }



    //    }
    //    public void SelectOption(int selected)
    //    {
    //        foreach(DialogueOption option in current.options)
    //        {
    //            if (option.optionNumber == selected)
    //            {
    //                EndConversation();
    //                if (option.response)
    //                {
    //                    StartConversation(option.response);
    //                }
    //            }
    //        }
    //    }
    //    public void EndConversation()
    //    {
    //        WaitingAnswer = false;

    //    }


    public IEnumerator ReplaceDialogueKeywords(string[] dialogue)
    {
        List<string> dialogueLines = new List<string>();

        foreach (string line in dialogue)
        {

            if (line.Contains("/houseName"))
            {
                string lineReplacement = line.Replace("/houseName", GameObject.Find("Brain").GetComponent<Manager>().HousesOnStage[0].GetComponent<HousePersonality>().Name);

                dialogueLines.Add(lineReplacement);


            }
            else
            {
                dialogueLines.Add(line);
                yield return null;

            }


            yield return null;

        }
        ConversationManager.Instance.NextCheckedText = dialogueLines.ToArray();

        yield return dialogueLines.ToArray();

}

    public class ConversationTape 
    {

        private void SetCassettte(Conversation_Cassette cassette, MonoBehaviour mono)
        {

            ConversationManager.Instance.P1_ShowPresentation = cassette.ShowPresentation.PossiblePresentations[Random.Range(0, cassette.ShowPresentation.PossiblePresentations.Length)];

            mono.StartCoroutine(TapeShuffle_ShowmanGreetsHouse(cassette));

            mono.StartCoroutine(TapeShuffle_ShowmanAskHouse(cassette));        

            ConversationManager.Instance.P3_ShowmanPresentCandidates = new string[] { cassette.ShowmanGreetsCandidates.PresentantionDialogue[Random.Range(0, cassette.ShowmanGreetsCandidates.PresentantionDialogue.Length)] };

            mono.StartCoroutine(TapeShuffle_CandidatePresentation(cassette));






        }
        public ConversationTape(Conversation_Cassette cassette)
        {

            MonoBehaviour mono = GameObject.Find("Brain").GetComponent<Manager>();

            SetCassettte(cassette, mono);

        }

       public  IEnumerator TapeShuffle_ShowmanGreetsHouse(Conversation_Cassette cassette)
        {

            foreach (HouseType type in cassette.ShowmanGreetsHouse.ShowmanGreetsHouse)
            {
                if (type.Type == GameObject.Find("Brain").GetComponent<Manager>().HousesOnStage[0].GetComponent<HousePersonality>().HouseType)
                {
                    ConversationManager.Instance.P2_ShowmanPresentHouse = type.ShowmanGreeting[Random.Range(0, type.ShowmanGreeting.Length)].HousePresentations;
                }
                yield return null;
            }
        }
        public  IEnumerator TapeShuffle_CandidatePresentation(Conversation_Cassette cassette)
        {
            
            foreach (Crushing type in cassette.CandidatesPresentation.Feeling)
            {
                if (type.HouseType == GameObject.Find("Brain").GetComponent<Manager>().HousesOnStage[0].GetComponent<HousePersonality>().HouseType)
                {
                    ConversationManager.Instance.P3_BestCandidatePresentation = type.BestCandidateDialogues[Random.Range(0,type.BestCandidateDialogues.Length)].text;

                    ConversationManager.Instance.P3_NeutralCandidatePresentation = type.NeutralCandidateDialogues[Random.Range(0, type.NeutralCandidateDialogues.Length)].text;


                    ConversationManager.Instance.P3_WorstCandidatePresentation = type.WorstCandidateDialogues[Random.Range(0, type.WorstCandidateDialogues.Length)].text;

                    yield return null;

                }
                yield return null;
               
            }
        }

      public  IEnumerator TapeShuffle_ShowmanAskHouse(Conversation_Cassette cassette)
        {

            foreach (HouseType type in cassette.ShowmanGreetsHouse.ShowmanGreetsHouse)
            {
                if (type.Type == GameObject.Find("Brain").GetComponent<Manager>().HousesOnStage[0].GetComponent<HousePersonality>().HouseType)
                {
                    ConversationManager.Instance.P2_ShowmanQuestion = type.ShowmanAskHouse[Random.Range(0, type.ShowmanAskHouse.Length)];
                }
                yield return null;
            }
        } 
        //public ConversationTape(Conversation_Showman showman, Conversation_House house, CandidatesTape_CandidatesPresentation candidates)
        //{
        //    ShowmanTape = showman;
        //    HouseTape = house;
        //    CandidatesTape = candidates;

        //}
        //#region HouseTape
        //private Conversation_House HouseTape;
        //private Dialogue HouseDialogue;
        //private Dialogue HouseGreeting;

        //public ShowmanTape_ShowmanGreetsHouse ShowmanGreetsHouse;
        //#endregion

        #region ShowmanTape
        public ShowmanTape_ShowmanGreetsHouse ShowmanTape;

        private ShowmanTape_ShowmanReactions[] _ShowmanReactions;
        private ShowmanTape_ShowPresentation _ShowmanPresentation;
        private ShowmanTape_ShowmanGreetsCandidates _ShowmanGreetsCandidates;

        public ShowmanTape_ShowmanReactions[] ShowmanReactions
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
        public ShowmanTape_ShowPresentation ShowmanPresentation
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
        public ShowmanTape_ShowmanGreetsCandidates ShowmanGreetsCandidates
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

        public CandidatesTape_CandidatesPresentation CandidatesTape;
        private string BestCandidatePresentation;
        private string NeutralCandidatePresentation;
        private string WorstCandidatePresentation;

        //void Start()
        //{
        //    ShowmanPresentation = ShowmanTape.PossibleShowPresentations[Random.Range(0, ShowmanTape.PossibleShowPresentations.Length)];

        //    ShowmanGreetsHouse = ShowmanTape.PossibleHousePresentations[Random.Range(0, ShowmanTape.PossibleHousePresentations.Length)];
        //    HouseGreeting = HouseTape.dialogues[Random.Range(0, HouseTape.dialogues.Length)];

        //    ShowmanGreetsCandidates = ShowmanTape.ShowmanGreetsCandidates[Random.Range(0, ShowmanTape.ShowmanGreetsCandidates.Length)];


        //    BestCandidatePresentation = CandidatesTape.BestCandidateDialogues[Random.Range(0, CandidatesTape.BestCandidateDialogues.Length)].text;
        //    NeutralCandidatePresentation = CandidatesTape.NeutralCandidateDialogues[Random.Range(0, CandidatesTape.NeutralCandidateDialogues.Length)].text;
        //    WorstCandidatePresentation = CandidatesTape.WorstCandidateDialogues[Random.Range(0, CandidatesTape.WorstCandidateDialogues.Length)].text;


        //    ShowmanReactions = ShowmanTape.ShowmanReactions;


        //}


    }
}




[System.Serializable]
public class KeyWords
{

    [Tooltip("HouseName => /houseName \nHouseWish => /houseWish \nHouePersonality => /housePersonality")]
    public KeyWordsT Tutorial;
}
[System.Serializable]
public class KeyWordsT
{
}



