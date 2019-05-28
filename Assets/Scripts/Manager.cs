using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Manager : MonoBehaviour
{   


    [Header("Cameras")]
    public GameObject[] Cams;
    public GameObject OverlapShot;
    public Vector3[] OverlapShotCoord;
    public Vector3[] OverlapShotScale;
    public static GameObject CurrentCam;
    public GameObject Interlocutor1, Interlocutor2;

    [Header ("HUD")]
    public GameObject TextFrameworkRef;
    public GameObject TextBoxRef;
    public Sprite[] TextFrameWorkSpritesRef;




    private List<Person> _Census;

   
    
     [Header ("Hosts")]
    public GameObject Showman;
    public GameObject[] Houses;
    public Vector2 HouseOnStage;
    public float HouseEntryTime;
    public int[] CensusID;
    public GameObject Curtain;
    public static int IDAssign;
    public GameObject TextBoxCanvas;
    public GameObject[] SpecialInterlocutorsComponent;
    



    public List<Person> Census
    {
        get
        {
            return this._Census;
        }
        set
        {
            this._Census = value;
        }

    }

    public GameObject[] CharacterCase;
    public string[] AvailableNames;

    [Header("Adds")]
    public GameObject AddBackground;
    public GameObject[] Adds;
    public GameObject HUDAdd;
    public float TimeShowingAdd;
    [Range(0,1)]
    public float PlayAddDelay;
    
    

    void Start()
    {
        
        CensusID = new int[0];
        _Census = new List<Person>();
        //StartCoroutine(AddsLibrary(Adds)) ;

        //AdvertisingTime pack = new AdvertisingTime(Adds, 2, 2);

        //GoToAdvertising(pack);


    }

    

    void OverlapCurrentShot(bool overlap)
    {

        if(!overlap)
        {
            OverlapShot.SetActive(false);
            return;
        }
      for(int i=0; i<Cams.Length;i++)
        {
          if(Cams[i]==CurrentCam)
            {
                OverlapShot.transform.position = OverlapShotCoord[i];
                OverlapShot.transform.localScale = OverlapShotScale[i];
            }
        }
        OverlapShot.SetActive(true);
    }
    void UpdateCensus ()
    {
        if (Census.Count == 0) return;

        CensusID = new int[Census.Count];
        for ( int i =0; i<CensusID.Length; i++)
        {
            CensusID[i] = Census[i].ID;
        }
    }
    void Update()
    {
        UpdateCensus();
        if (Input.GetKey(KeyCode.C)                         )
            {
            if (Input.GetKey(KeyCode.Backspace)      )
                {
                if (Input.GetKey(KeyCode.Alpha1)  )
                    {
                    Interlocutor1.GetComponent<NpcController>().Reset();

                }
                else if (Input.GetKey(KeyCode.Alpha2))
                {
                    Interlocutor2.GetComponent<NpcController>().Reset();

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Interlocutor1.GetComponentInChildren<NpcController>().Talk(Interlocutor1.GetComponentInChildren<NpcController>().Dialogue);
            

        }       if (Input.GetKeyDown(KeyCode.N))
        {
            Interlocutor2.GetComponentInChildren<NpcController>().Talk(Interlocutor2.GetComponentInChildren<NpcController>().Dialogue);
            Interlocutor2.GetComponentInChildren<AudioSource>().Play();
            Interlocutor2.GetComponentInChildren<Animator>().SetBool("Talk", true);

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            NpcToFirstPlane(_Census[0]);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            NpcToFirstPlane(_Census[1]);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(OverlapShot.activeSelf)
            {
                               OverlapCurrentShot(false) ;

            }
            else if (!OverlapShot.activeSelf)
            {
                               OverlapCurrentShot(true) ;

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapCamera(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapCamera(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapCamera(2);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EntryOnStage(Houses[0]);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            int random = Random.Range(0, CharacterCase.Length);
            CreateCharacter(CharacterCase[random], AvailableNames[Random.Range(0, AvailableNames.Length)], Random.Range(1990,2001), new Vector2(10,-0.3f), CharacterCase[random].name);
        }
        if (Input.GetKeyDown(KeyCode.E)&&_Census.Capacity>0)
        {
            Destroy(_Census[0].Appearance.gameObject);
            _Census.RemoveAt(0);
            _Census.Capacity = (_Census.Capacity<0)?0:_Census.Capacity-1;
        }           
    }




    public void CreateCharacter(GameObject characterCase, string name, int year, Vector3 spawnPos, string type)
    {
        Census.Add(new Person(characterCase, name, year, spawnPos,type));

    }

    void GoToAdvertising(AdvertisingTime setUp)
    {
        setUp.InitAddTime(this);
            }
    void EntryOnStage(GameObject house)
    {
        GameObject houseCandidate = Instantiate(house) as GameObject;

        SwapCamera(1);           

        StartCoroutine(HouseEntryStage(houseCandidate));
    }

    void AdjustInterlocutorSpawnScale()
    {

    }
    public void NpcToFirstPlane(Person npc)
    {

        TextBoxCanvas.SetActive(true);

        //GameObject character = Instantiate(npc) as GameObject;
        if (Interlocutor1.activeSelf)
        {
            if (Interlocutor2.activeSelf)
            {
                if (Interlocutor1.GetComponent<NpcController>().NpcType=="Showman")
                {
                    Interlocutor2.SetActive(true);
                    Interlocutor2.GetComponent<NpcController>().Refresh(npc, 0);
                }
                else
                {

                    Interlocutor1.SetActive(true);
                    Interlocutor1.GetComponent<NpcController>().Refresh(npc, 0);
                }
            }
            else
            {
              Interlocutor2.SetActive(true);
            Interlocutor2.GetComponent<NpcController>().Refresh(npc, 1);
            }  
        }
        else
        {

            Interlocutor1.SetActive(true);
            Interlocutor1.GetComponent<NpcController>().Refresh(npc, 0);

        }
        //character.transform.localPosition = new Vector3(0,0,-69);
    }
    void SwapCamera(int shot)
    {
        foreach(GameObject cam in Cams)
        {
            if (cam == Cams[shot])
            {
                cam.GetComponent<CinemachineVirtualCamera>().Priority = 2;
                CurrentCam = cam;
            }
            else
            {
                cam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
            }
        }


    }
    IEnumerator HouseEntryStage(GameObject house)
    {
        Vector3 pos = house.transform.position;
        pos.x = -20;
        pos.z = 20;
        house.transform.position = pos;
        while (Mathf.Abs(house.transform.position.x-HouseOnStage.x)>float.Epsilon)
        {
            house.transform.position = Vector2.Lerp(house.transform.position, HouseOnStage, Time.deltaTime/HouseEntryTime);
            yield return null;
        }
    }
}
