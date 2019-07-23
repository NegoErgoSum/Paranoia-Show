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
