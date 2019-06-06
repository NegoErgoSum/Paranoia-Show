using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;


public class Hacks : MonoBehaviour
{
    public GameObject cinematic;
    void Start()
    {
        
    }

    void Update()
    {
       if (Input.GetKey(KeyCode.C))
        {

           if( Input.GetKey(KeyCode.V))
                {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    GameObject.Find("Brain").GetComponent<Manager>().SwapCamera(0, CinemachineBlendDefinition.Style.Cut);
                }
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    GameObject.Find("Brain").GetComponent<Manager>().SwapCamera(1, CinemachineBlendDefinition.Style.Cut);
                }
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    GameObject.Find("Brain").GetComponent<Manager>().SwapCamera(2, CinemachineBlendDefinition.Style.Cut);
                }  
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (GameObject.Find("Brain").GetComponent<Manager>().OverlapShot.activeSelf)
                {
                    GameObject.Find("Brain").GetComponent<Manager>().OverlapCurrentShot(false);

                }
                else if (!GameObject.Find("Brain").GetComponent<Manager>().OverlapShot.activeSelf)
                {
                    GameObject.Find("Brain").GetComponent<Manager>().OverlapCurrentShot(true);

                }
            }
        }      //_C_amera  hacks

        if (Input.GetKey(KeyCode.G))
        {

            if (Input.GetKeyDown(KeyCode.O))
            {

                StartCoroutine(GameObject.Find("Brain").GetComponent<Manager>().GameOver());
            }
        }     //Game hacks
        if (Input.GetKey(KeyCode.S))       
        {
             if (Input.GetKeyDown(KeyCode.A))
            {
                AdvertisingTime pack = new AdvertisingTime(GameObject.Find("Brain").GetComponent<Manager>().Adds, 2, 2);

                GameObject.Find("Brain").GetComponent<Manager>().GoToAdvertising(pack);
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                AdvertisingTime pack = new AdvertisingTime(GameObject.Find("Brain").GetComponent<Manager>().Adds, 2, 2);
                pack.FirstAdds = true;

                GameObject.Find("Brain").GetComponent<Manager>().GoToAdvertising(pack);
            }
        }     // _S_how  hacks

            if  (Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKey(KeyCode.W))
                {
                cinematic.GetComponent<PlayableDirector>().Play();
                cinematic.GetComponent<AudioSource>().Play();

            }
        }


    }
}
