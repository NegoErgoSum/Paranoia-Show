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
    public Vector3 FocusPoint1, FocusPoint2;

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
        LimitMaxX = Coll.bounds.max.x ;
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
        float randomX2 = Random.Range(LimitMinX, LimitMaxX);
        float randomY = Random.Range(LimitMinY, LimitMaxY);
        float randomY2 = Random.Range(LimitMinY, LimitMaxY);
        float time = 1;


        while (!Focus)
        { Vector2 randomPoint = new Vector2(randomX, randomY);
             Vector2 randomPoint2 = new Vector2(randomX2, randomY2);

                //Debug.Log(randomPoint);
                Spot1.transform.position = Vector3.Lerp(Spot1.transform.position, randomPoint, Time.deltaTime / time);
                Spot2.transform.position = Vector3.Lerp(Spot2.transform.position, randomPoint2, Time.deltaTime / time);



            Vector3 spawnZ1 = Spot1.transform.position;
            Vector3 spawnZ2 = Spot2.transform.position;
            spawnZ1.z = 8;
            spawnZ2.z = 8;
            Spot1.transform.localPosition = spawnZ1;
            Spot2.transform.localPosition = spawnZ2;
            //Debug.Log(LimitMinX + "," + LimitMaxX);

            //Vector2 max = Spot2.transform.position;
            //max.x = Mathf.Max(LimitMinX, Spot2.transform.position.x);
            //max.x = Mathf.Min(LimitMaxX, Spot2.transform.position.x);
            //max.y = Mathf.Min(LimitMaxY, Spot2.transform.position.y);
            //max.x = Mathf.Max(LimitMinY, Spot2.transform.position.y);
            //Spot2.transform.position = max;

            if (Vector2.Distance(Spot1.transform.position, randomPoint) < 1)
                {
                    randomX = Random.Range(LimitMinX, LimitMaxX);
                    randomY = Random.Range(LimitMinY, LimitMaxY);
                    time = 1;
                }   if (Vector2.Distance(Spot2.transform.position, randomPoint2) < 1)
                {
                    randomX2 = Random.Range(LimitMinX, LimitMaxX);
                    randomY2 = Random.Range(LimitMinY, LimitMaxY);
                    time = 1;
                }




                time -= Time.deltaTime;

                yield return null;
            }
            GameObject.Find("Brain").GetComponent<Manager>().ShowmanSignIn();

            while (Vector2.Distance(Spot1.transform.position, FocusPoint1) > 1 || Vector2.Distance(Spot2.transform.position, FocusPoint2) > 1)
            {
                time -= Time.deltaTime;

                Spot1.transform.position = Vector3.Lerp(Spot1.transform.position, FocusPoint1, Time.deltaTime / time);
                Spot2.transform.position = Vector3.Lerp(Spot2.transform.position, FocusPoint2, Time.deltaTime / time);





                yield return null;
            }

            GameObject.Find("Brain").GetComponent<Manager>().SP1_EnterShowman(true);




        }
    }

