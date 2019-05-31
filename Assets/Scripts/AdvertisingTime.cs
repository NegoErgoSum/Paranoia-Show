using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvertisingTime
{        
    private float _TimeShowingAdd;
    private float _Fade;
    private GameObject[] _AddsLibrary;

    private GameObject AddBackground;
    private GameObject HUDAdd;
    private GameObject Curtain; 

    public float TimeShowingAdd
    {
        get
        {
            return this._TimeShowingAdd;
        }
        set
        {
            this._TimeShowingAdd = value;
        }

    }
    public float Fade
    {
        get
        {
            return this._Fade;
        }
        set
        {
            this._Fade = value;
        }

    }
    public GameObject[] AddsLibraryRef
    {
        get
        {
            return this._AddsLibrary;
        }
        set
        {
            this._AddsLibrary = value;
        }
    }  

     
      //Llamada completa: Array de anuncios, tiempo por anuncio, fade entre anuncios
    public AdvertisingTime(GameObject[] addsToPlay, float timePerAdd, float fade)
    {
        AddsLibraryRef = addsToPlay;
        TimeShowingAdd = timePerAdd;
        Fade = fade;


        HUDAdd = GameObject.Find("Add");
        AddBackground = GameObject.Find("AddBackground");
        Curtain = GameObject.Find("BigCurtain");
    }
    public AdvertisingTime(GameObject[] addsToPlay, float timePerAdd)
    {
        AddsLibraryRef = addsToPlay;
        TimeShowingAdd = timePerAdd;
       Fade = 1;

        HUDAdd = GameObject.Find("Add");
        AddBackground = GameObject.Find("AddBackground");
        Curtain = GameObject.Find("BigCurtain");
    }
    public AdvertisingTime(GameObject[] addsToPlay)
    {
        AddsLibraryRef = addsToPlay;
        TimeShowingAdd = 3;
        Fade = 2;
        

        HUDAdd = GameObject.Find("Add");
        AddBackground = GameObject.Find("AddBackground");
        Curtain = GameObject.Find("BigCurtain");
    }

   

        public void InitAddTime(MonoBehaviour mono)
        {
            mono.StartCoroutine(AddsLibrary(mono, AddsLibraryRef, Fade, TimeShowingAdd));
        }

        IEnumerator AddsLibrary(MonoBehaviour monobehaviour, GameObject[] adds, float fade, float timeShowingAdd)
        {
            AddBackground.SetActive(true);

            foreach (GameObject add in adds)
            {
                yield return monobehaviour.StartCoroutine(PlayAdd(add, fade, timeShowingAdd));
            }
            AddBackground.SetActive(false);
        
        GameObject.Find("Brain").GetComponent<Manager>().Spotlights.SetActive(true);
        GameObject.Find("Brain").GetComponent<Manager>().Spotlights.GetComponent<SpotLightController>().ShowPresentation();

        }     //library of adds to play
        IEnumerator PlayAdd(GameObject add, float playAddDelay, float timeShowingAdd)
        {
            Color transparent = HUDAdd.GetComponent<Image>().color;
            transparent.a = 0;
            HUDAdd.GetComponent<Image>().color = transparent;

            HUDAdd.GetComponent<Image>().sprite = add.GetComponent<SpriteRenderer>().sprite;

            //transparencia 0 -> 1
            for (float i = 0; i < 1; i += Time.deltaTime * playAddDelay)
            {

                Color fade = HUDAdd.GetComponent<Image>().color;
                fade.a += Time.deltaTime * playAddDelay;
                HUDAdd.GetComponent<Image>().color = fade;
                yield return null;
            }
            yield return new WaitForSeconds(timeShowingAdd);
            //transparenciea 1->0
            for (float i = 1; i > 0; i -= Time.deltaTime * playAddDelay)
            {

                Color fade = HUDAdd.GetComponent<Image>().color;
                fade.a -= Time.deltaTime * playAddDelay;
                HUDAdd.GetComponent<Image>().color = fade;
                yield return null;
            }
            yield return null;

        }    //fade and show time of an add
    }

