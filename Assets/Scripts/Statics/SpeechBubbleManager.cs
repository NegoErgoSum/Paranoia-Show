using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleManager : MonoBehaviour
{

    private GameObject _ComicDialogue;
    private List<GameObject> _HousesOnStage;
    private float TalkingDelay;

    private Person Candidate;
    private GameObject House;
    private string Line;


    public enum _Speaker
    {
      HOUSE, CANDIDATE
    };
    public _Speaker Speaker;

    private GameObject ComicDialogue
    {
        get
        {
            return GameObject.Find("Brain").GetComponent<Manager>().ComicDialogue; 
        }
        set
        {
            ComicDialogue = GameObject.Find("Brain").GetComponent<Manager>().ComicDialogue;
        }
    }

    private List<GameObject> HousesOnStage
    {
        get
        {
            return GameObject.Find("Brain").GetComponent<Manager>().HousesOnStage; 
        }
        set
        {
            HousesOnStage = GameObject.Find("Brain").GetComponent<Manager>().HousesOnStage;
        }
    }
   public void Initialize()
    {

        switch (Speaker)
        {
            case _Speaker.CANDIDATE:
            {
                    gameObject.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 830);
                    gameObject.GetComponent<RectTransform>().transform.localEulerAngles = Vector3.zero;

                    StartCoroutine(Speak(Candidate, Line));
                break;
            }
            case _Speaker.HOUSE:
            {
                    gameObject.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 830);
                    gameObject.GetComponent<RectTransform>().transform.localEulerAngles = Vector3.zero;

                    StartCoroutine(Speak(Candidate, Line));
                break;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CloneParameters(SpeechBubbleManager speechBubbleManager)
    {
        speechBubbleManager.Candidate = Candidate;
        speechBubbleManager.Speaker = Speaker;
        speechBubbleManager.Line = Line;
        speechBubbleManager.TalkingDelay = TalkingDelay;
    }


    public SpeechBubbleManager(Person candidate, string line)
    {
        Speaker = _Speaker.CANDIDATE;
        Candidate = candidate;
        Line = line;

        //Autoassigned parameters
        TalkingDelay = GameObject.Find("Brain").GetComponent<Manager>().TalkingDelay;
        
    }
    public SpeechBubbleManager(Person candidate, string line, float talkingDelay)
    {
        Speaker = _Speaker.CANDIDATE;
        Candidate = candidate;
        Line = line;
        TalkingDelay = talkingDelay;
        
    }
    public SpeechBubbleManager(GameObject house, string line, float talkingDelay)
    {
        Speaker = _Speaker.HOUSE;
        House = house;
        Line = line;
        TalkingDelay = talkingDelay;
        
    }

    public SpeechBubbleManager(GameObject house, string line)
    {
        Speaker = _Speaker.HOUSE;
        House = house;
        Line = line;


        TalkingDelay = 0.15f;
        
    }


    public IEnumerator Speak(Person candidate,string line)
    {
        for (int i = 0; i <= line.Length; i++)
        {


            string currentText = line.Substring(0, i);
            gameObject.GetComponentInChildren<Text>().text = currentText;
            yield return new WaitForSeconds(TalkingDelay);

        } 
        
    }

}
