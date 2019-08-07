using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleControl : MonoBehaviour
{
    private float SpeechBubbleZoomMultiplier;
    private Vector3 SpawnPos;
    private List<GameObject> Childs;
    private bool Ready;
    private float MaxScale;
    private Vector3 MaxPos;

    public enum _Protocols
    {
      NERVOUS, REGULAR, ELEGANT
    };

    public BehaviourLibrary.Behaviour Protocol;
    public void RandomPersonality()
    {
        Protocol = (BehaviourLibrary.Behaviour)Random.Range(0, 2);
        
    }

    void Start()
    {
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        Ready = false;
        Childs = new List<GameObject>();
        SpeechBubbleZoomMultiplier = GameObject.Find("Brain").GetComponent<Manager>().SpeechBubbleZoomMultiplier;
        MaxScale = GameObject.Find("Brain").GetComponent<Manager>().BubbleMaxScale;
        //SpawnPos = new Vector3(GameObject.Find("Brain").GetComponent<Manager>().CurrentShadow.transform.localPosition.x, GameObject.Find("Brain").GetComponent<Manager>().CurrentShadow.transform.localPosition.y, GameObject.Find("Brain").GetComponent<Manager>().CurrentShadow.transform.localPosition.z);
        //transform.localPosition = Vector3.zero;
        //transform.localEulerAngles = Vector3.zero;
        StartCoroutine(AddChilds());


    }

    void Update()
    {
        if (gameObject.GetComponentInChildren<Text>().color.a <float.Epsilon)
        {             
            Destroy(gameObject);   
        }  
        if(!Ready)
        {
            return;
        }
        switch(Protocol)
        {
            case BehaviourLibrary.Behaviour.NERVOUS:
                {
                    Nervous();
                    break;
                }
            case BehaviourLibrary.Behaviour.NORMAL:
                {
                    Regular();
                    break;
                }
            case BehaviourLibrary.Behaviour.ELEGANT:
                {
                    Elegant();
                    break;
                }

        }
     
    }

     private void Nervous()
    {

           if (gameObject.transform.localScale.x > MaxScale / 2)
        {

            StartCoroutine(Fading());
        }
      
        
        if (gameObject.transform.localScale.x < MaxScale)
        {

            StartCoroutine(IncraseScale());
        }


        #region ChangePos
        Vector3 currentTextBoxPos = gameObject.transform.position;
        //currentTextBoxPos.y += Time.deltaTime*2;
        //currentTextBoxPos.x -= Time.deltaTime*2;
        currentTextBoxPos.y -= Random.Range(-2, 4) * Time.deltaTime;
        currentTextBoxPos.x -= Random.Range(-2, 5) * Time.deltaTime;
        gameObject.transform.position = currentTextBoxPos;
        #endregion  //Gradual movement
    }

    private void Regular()
    {

           if (gameObject.transform.localScale.x > MaxScale / 2)
        {

            StartCoroutine(Fading());
        }
      
        
        if (gameObject.transform.localScale.x < MaxScale)
        {

            StartCoroutine(IncraseScale());
        }


        #region ChangePos
        Vector3 currentTextBoxPos = gameObject.transform.position;
        currentTextBoxPos.y -= Time.deltaTime * 0.3f;
        currentTextBoxPos.x -= Time.deltaTime * 0.3f;

        gameObject.GetComponent<RectTransform>().transform.position = currentTextBoxPos;
        #endregion  //Gradual movement
    }
    private void Elegant()
    {

           if (gameObject.transform.localScale.x > MaxScale / 2)
        {

            StartCoroutine(Fading());
        }
      
        
        if (gameObject.transform.localScale.x < MaxScale)
        {

            StartCoroutine(IncraseScale());
        }


        StartCoroutine(LeafFall());
    }


     IEnumerator LeafFall()
    {
        Vector3 currentPos = transform.position;
        StartCoroutine(LeafFallY(currentPos));
        StartCoroutine(LeafFallX(currentPos));
        yield return null;


    }
    IEnumerator LeafFallY(Vector3 goal)
    {
        Vector3 nextPos = goal - new Vector3(0, -10, 0);

        while (Mathf.Abs(gameObject.GetComponent<RectTransform>().transform.position.y -goal.y)>0)
        {
            gameObject.GetComponent<RectTransform>().transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime);
            yield return null;
        }


    }
    IEnumerator LeafFallX(Vector3 goal)
    {
        Vector3 nextPos = goal - new Vector3(-10, 0, 0);

        
        while (Mathf.Abs(gameObject.GetComponent<RectTransform>().transform.position.x -goal.x)>0)
        {
            gameObject.GetComponent<RectTransform>().transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime);
            yield return null;
        }


    }



    IEnumerator AddChilds()
    {
        foreach (Transform child in gameObject.transform)
        {             
            Childs.Add(child.gameObject);
            yield return null;
        }
        Ready = true;

    }

    IEnumerator Fading()
    {


       
          foreach (GameObject child in Childs)
        {
              if (child.gameObject.name == "TextBox")
                {               


                            Color currentTransparencyFont = child.gameObject.GetComponent<Text>().color;
                            currentTransparencyFont.a -= Time.deltaTime;
                            child.gameObject.GetComponent<Text>().color = currentTransparencyFont;
              
            }
              else
                { 
               Color currentTransparency = child.GetComponent<Image>().color;
                currentTransparency.a -= Time.deltaTime;
                child.gameObject.GetComponent<Image>().color = currentTransparency;
            }
          
                             yield return null;                     
        }
        

        
        

        

    }
    IEnumerator IncraseScale()
    {
        for (float i = SpeechBubbleZoomMultiplier; i >0;i-=Time.deltaTime)
        {
            #region ChangeScale
            Vector3 currentTextBoxScale = gameObject.transform.localScale;
            currentTextBoxScale.x +=   i;
            currentTextBoxScale.y +=  i;
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, currentTextBoxScale, Time.deltaTime);
            #endregion  //Gradual scale increase
            yield return null;
        }
    }
}
