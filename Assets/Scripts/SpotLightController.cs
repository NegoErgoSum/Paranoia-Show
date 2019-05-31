using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    private BoxCollider2D Coll;
    private float _PointX, _PointY;
    private GameObject Curtain;

    public float LimitMinX, LimitMaxX, LimitMinY, LimitMaxY;
    public bool Focus;
    public Vector2 FocusPoint1, FocusPoint2;

    public GameObject Spot1, Spot2;
   
    public float PointX
    {
        get
        {
            return _PointX;
        }
        set
        {
            if (value > LimitMaxX)
            {
                _PointX = LimitMaxX;
            }
            else if (value < LimitMinX)
            {
                _PointX = LimitMinX;
            }
            else
            {
                _PointX = value;
            }

        }
    }
    public float PointY
    {
        get
        {
            return _PointY;
        }
        set
        {
            if (value > LimitMaxY)
            {
                _PointY = LimitMaxY;
            }
            else if (value < LimitMinY)
            {
                _PointY = LimitMinY;
            }
            else
            {
                _PointY = value;
            }

        }
    }
    void Start()
    {
        
        Curtain = GameObject.Find("BigCurtain");
        Focus = false;
        Coll = gameObject.GetComponent<BoxCollider2D>();
        LimitMaxX = Coll.bounds.max.x;
        LimitMaxY = Coll.bounds.max.y;
        LimitMinX = Coll.bounds.min.x;
        LimitMinY = Coll.bounds.min.y;
    }
    private void LightFocus()
    {
        Focus = true;
    }
    public void ShowPresentation()
    {
        StartCoroutine(Moving());
        Invoke("LightFocus", 5);





    }
    void Update()
    {

    }

    IEnumerator Moving()
    {
        float randomX = Random.Range(LimitMinX, LimitMaxX);
        float randomY = Random.Range(LimitMinY, LimitMaxY);
        float time = 1;


        while (!Focus)
        {
            time -= Time.deltaTime;
            Vector2 randomPoint = new Vector2(randomX, randomY);
            Spot1.transform.position = Vector2.Lerp(Spot1.transform.position, randomPoint, Time.deltaTime / time);
            Spot2.transform.position = Vector2.Lerp(Spot2.transform.position, -randomPoint, Time.deltaTime / time);
            if (Vector2.Distance(Spot1.transform.position, randomPoint) < 1)
            {
                randomX = Random.Range(LimitMinX, LimitMaxX);
                randomY = Random.Range(LimitMinY, LimitMaxY);
                time = 1;
            }
            yield return null;
        }
        GameObject.Find("Brain").GetComponent<Manager>().ShowmanSignIn();

        while (Vector2.Distance(Spot1.transform.position, FocusPoint1) > 1 || Vector2.Distance(Spot2.transform.position, FocusPoint2) > 1)
        {
            time -= Time.deltaTime;

            Spot1.transform.position = Vector2.Lerp(Spot1.transform.position, FocusPoint1,  Time.deltaTime / time);
            Spot2.transform.position = Vector2.Lerp(Spot2.transform.position, FocusPoint2,  Time.deltaTime / time);





            yield return null;
        }

        GameObject.Find("Brain").GetComponent<Manager>().EnterShowman(true);




    }
}
