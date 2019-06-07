using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleControl : MonoBehaviour
{
    private float SpeechBubbleZoomMultiplier;
    private Vector2 SpawnPos;
    private List<GameObject> Childs;
    private bool Ready;
    private float MaxScale;
    private Vector2 MaxPos;
    void Start()
    {
      
        gameObject.transform.localScale = new Vector2(0.1f, 0.1f);
        Ready = false;
        Childs = new List<GameObject>();
        SpeechBubbleZoomMultiplier = GameObject.Find("Brain").GetComponent<Manager>().SpeechBubbleZoomMultiplier;
        MaxScale = GameObject.Find("Brain").GetComponent<Manager>().BubbleMaxScale;
        SpawnPos = GameObject.Find("Brain").GetComponent<Manager>().BubbleSpawnPos;
        StartCoroutine(AddChilds());
    }

    void Update()
    {     if (gameObject.GetComponentInChildren<Text>().color.a <float.Epsilon)
        {
            //Debug.Log(gameObject.GetComponentInChildren<Text>().color.a);

            Destroy(gameObject);

        }  
        if(!Ready)
        {
            return;
        }
        if (gameObject.transform.localScale.x > MaxScale / 2)
        {

            StartCoroutine(Fading());
        }
       
        if (gameObject.transform.localScale.x < MaxScale)
            {

            StartCoroutine(IncraseScale());
           




           


            }
        #region ChangePos
        Vector2 currentTextBoxPos = gameObject.transform.position;
        currentTextBoxPos.y += Time.deltaTime*2;
        currentTextBoxPos.x -= Time.deltaTime*2;
        //currentTextBoxPos.y += Random.Range(-2, 4) * Time.deltaTime;
        //currentTextBoxPos.x -= Random.Range(-2, 5) * Time.deltaTime;
        gameObject.transform.position = currentTextBoxPos;
        #endregion  //Gradual movement
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
            Vector2 currentTextBoxScale = gameObject.transform.localScale;
            currentTextBoxScale.x +=   i;
            currentTextBoxScale.y +=  i;
            gameObject.transform.localScale = Vector2.Lerp(gameObject.transform.localScale, currentTextBoxScale, Time.deltaTime);
            #endregion  //Gradual scale increase
            yield return null;
        }
    }
}
