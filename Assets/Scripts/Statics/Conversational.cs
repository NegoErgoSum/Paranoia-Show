using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversational : MonoBehaviour
{
    public Conversation_House conversation;

          public void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TriggerConversation();
        }
    }
  public void TriggerConversation()
    {
        ConversationManager.Instance.StartConversation(conversation);

    }
}
