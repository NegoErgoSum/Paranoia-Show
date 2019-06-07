//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Cinemachine;


public class Manager : MonoBehaviour
{

    [Header("Phase2Commands")]
    public Vector2 CandidatePresentationPos;
    public Vector2 BubbleSpawnPos;
    public Vector2 CompileShadowsPos;
    public int CandidatePresentationTime;
    public float SpeechBubbleZoomMultiplier;
    public float TalkingDelay ;
    public float BubbleMaxScale;      
    public GameObject ComicDialogue;
    public GameObject[] CandidateShadows;
    private List<GameObject> ShadowsChecked;
    private bool TalkingDialogue;
    private Text TextBox;
    private GameObject CurrentShadow;

    [Header("Cameras")]
    public GameObject[] Cams;
    public GameObject OverlapShot;
    public Vector3[] OverlapShotCoord;
    public Vector3[] OverlapShotScale;
    public static GameObject CurrentCam;
    public GameObject Interlocutor1, Interlocutor2;
    public GameObject Spotlights;

    [Header ("HUD")]
    public GameObject TextFrameworkRef;
    public GameObject TextBoxRef;
    public Sprite[] TextFrameWorkSpritesRef;
    public Vector3 GameOverPos;
    private GameObject GameplayCam;




    private List<Person> _Census;

   
    
     [Header ("Hosts")]
    public GameObject ShowmanPrefab;
    private GameObject Showman;
    public GameObject[] Houses;
    public Vector2 HouseOnStage;
    public float HouseEntryTime;
    public int[] CensusID;
    public GameObject Curtain;
    public static int IDAssign;
    public GameObject TextBoxCanvas;
    public GameObject[] SpecialInterlocutorsComponent;


    [Header("AudioSettings")]
    public AudioClip Sounds;
    public AudioSource SpotLightAudiosource;

    [TextArea]
    public string[] FirstDialogue;

    public enum ShowStatus
    {
        ADVERTISING, PRESENTATION, SHOW, PHASE1, PHASE2
    };
    

    public ShowStatus ShowPhase;

   
    public AudioSource NewAudiosource(AudioClip audioClip, bool awake, bool loop)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = audioClip;
        newAudio.playOnAwake = awake;
        newAudio.loop = loop;

        return newAudio;
    }

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
    private List<GameObject> LevelCandidates;
    private List <GameObject> AvailableCandidates;
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
        ShowmanSignIn();

        ShadowsChecked = new List<GameObject>();
        TextBox = gameObject.GetComponentInChildren<Text>();

        SpotLightAudiosource = NewAudiosource(Sounds, false, false);

        Spotlights.SetActive(false);
        //StartCoroutine(GameOver());

        LevelCandidates = new List<GameObject>();
        LevelCandidates = CharacterCase.OfType<GameObject>().ToList();
        AvailableCandidates = new List<GameObject>();
        GameplayCam = GameObject.Find("GameplayCamera");
    CensusID = new int[0];
        _Census = new List<Person>();
        //StartCoroutine(AddsLibrary(Adds)) ;

        


    }

    

    public void OverlapCurrentShot(bool overlap)
    {

        if(!overlap)
        {
            OverlapShot.SetActive(false);
            return;
        }
        UpdateOverlap();
        OverlapShot.SetActive(true);
    }
    void UpdateOverlap()
    {
        if (ShowPhase!= ShowStatus.SHOW)
        {
            OverlapShot.SetActive(false);
            return;
        }
        for (int i = 0; i < Cams.Length; i++)
        {
            if (Cams[i] == CurrentCam)
            {
                OverlapShot.transform.position = OverlapShotCoord[i];
                OverlapShot.transform.localScale = OverlapShotScale[i];
            }
        }
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
        //Debug.Log(ShadowsChecked.Count);

        Debug.Log(ShowPhase);
        UpdateCensus();
        UpdateOverlap();


        if (Input.GetKey(KeyCode.C))
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                if (Input.GetKey(KeyCode.Alpha1))
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


        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Interlocutor2.GetComponentInChildren<NpcController>().Talk(Interlocutor2.GetComponentInChildren<NpcController>().Dialogue);
            Interlocutor2.GetComponentInChildren<AudioSource>().Play();
            Interlocutor2.GetComponentInChildren<Animator>().SetBool("Talk", true);

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            NpcToFirstPlane(_Census[0], false);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            NpcToFirstPlane(_Census[1], false);
        }
        
        if (Input.GetKey(KeyCode.C))
        {
              if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapCamera(0, CinemachineBlendDefinition.Style.Cut);
        }
        }
      
        if (Input.GetKey(KeyCode.F))
        {
            {
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {

                    EnterShowman(false);
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {

                    StartCoroutine( InstantiateCandidates(3));                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwapCamera(1, CinemachineBlendDefinition.Style.Cut);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwapCamera(2, CinemachineBlendDefinition.Style.Cut);
            }
           
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Phase1_HousePresentation(Houses[0]);
            }
           
            if (Input.GetKeyDown(KeyCode.E) && _Census.Capacity > 0)
            {

                Destroy(_Census[0].Appearance.gameObject);
                _Census.RemoveAt(0);
                _Census.Capacity = (_Census.Capacity < 0) ? 0 : _Census.Capacity - 1;
            }
        }
    }

     IEnumerator CheckingPoblation(int id)
    {
        foreach (Person d in Census)
            {
            if(d.ID== id)
            {

            }
            yield return null;
        }
    }


    public void CreateCharacter(GameObject characterCase, string name, int year, Vector3 spawnPos, string type)
    {
        //character.Appearance.GetComponent<NpcController>().Identificator = new Person(characterCase, name, year, spawnPos, type);
        GameObject citizen = Instantiate(characterCase) as GameObject;
        citizen.transform.localPosition = spawnPos;
        citizen.AddComponent<NpcController>();
        citizen.GetComponent<NpcController>().Identificator = new Person(characterCase, name, year); 
        citizen.AddComponent<Npc_SpecialComponent>();
        citizen.GetComponent<NpcController>().ShowPapers();

      citizen.SetActive(false);


        if (citizen.CompareTag("Showman"))
        {
            Showman = citizen.gameObject;
            return;
        }
                 Census.Add(citizen.GetComponent<NpcController>().Identificator);

    }

    public void GoToAdvertising(AdvertisingTime setUp)
    {
        ShowPhase = ShowStatus.ADVERTISING;
        setUp.InitAddTime(this);
            }
    public void EndOfAdvertising()
    {
        ShowPhase = ShowStatus.SHOW;
    }



    public void Phase1_HousePresentation(GameObject house)
    {
        ShowPhase = ShowStatus.PHASE1;

        GameObject houseCandidate = Instantiate(house) as GameObject;


        StartCoroutine(HouseEntryStage(houseCandidate));
    }
    public void Phase2_CandidatesPresentation()
    {

        ShowPhase = ShowStatus.PHASE2;
        StartCoroutine(P2_CandidatesPresentation());
    }

    void AdjustInterlocutorSpawnScale()
    {

    }
    public  void NpcToFirstPlane(Person npc, bool shadow)
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
                    Interlocutor2.GetComponent<NpcController>().Refresh(npc, 0, shadow);
                }
                else
                {

                    Interlocutor1.SetActive(true);
                    Interlocutor1.GetComponent<NpcController>().Refresh(npc, 0, shadow);
                }
            }
            else
            {
              Interlocutor2.SetActive(true);
            Interlocutor2.GetComponent<NpcController>().Refresh(npc, 1, shadow);
            }  
        }
        else  if (ShowPhase== ShowStatus.PHASE2)
        {
            Interlocutor2.SetActive(true);
            Interlocutor2.GetComponent<NpcController>().Refresh(npc, 0, shadow);          
        }
        else 
        {
            Interlocutor1.SetActive(true);
            Interlocutor1.GetComponent<NpcController>().Refresh(npc, 0, shadow);
        }
        //character.transform.localPosition = new Vector3(0,0,-69);
    }
    public  void SwapCamera(int shot, CinemachineBlendDefinition.Style blend)
    {
        Interlocutor1.GetComponent<NpcController>().Reset();
        Interlocutor2.GetComponent<NpcController>().Reset();

        GameObject.Find("Brain").GetComponent<CinemachineBrain>().m_DefaultBlend.m_Style = blend;

        foreach (GameObject cam in Cams)
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
    public void FormalizePerson()
    {

    }
    public void ShowmanSignIn()
    {
        if(Showman!=null)
        {
            return;
        }

          CreateCharacter(ShowmanPrefab, "Mr.Bug", 2000, new Vector2(10, -0.3f), ShowmanPrefab.name);
       

    }
    void Presentation()
    {
        EnterShowman(false);
        Interlocutor1.GetComponent<NpcController>().Dialogue = new string[FirstDialogue.Length];
        System.Array.Copy(FirstDialogue, Interlocutor1.GetComponent<NpcController>().Dialogue ,FirstDialogue.Length);
        ResumeShow();

    }
    public void ResumeShow()
    {
        ShowPhase = ShowStatus.SHOW;
    }
    public void EnterShowman(bool shadow)
    {
        if (Interlocutor1.GetComponent<Animator>().runtimeAnimatorController!=null)
        {
                                  Interlocutor1.GetComponent<NpcController>().Reset();

        }
        if (shadow)
        {
            Invoke("Presentation", 2);
        }
       if(shadow==false)
        {
            Curtain.GetComponent<Animator>().SetTrigger("PlayCurtain");
            Spotlights.SetActive(false);
            SpotLightAudiosource.Play();
            StartCoroutine(InstantiateCandidates(3));

        }
        NpcToFirstPlane(Showman.GetComponent<NpcController>().Identificator, shadow);
        //Interlocutor1.GetComponent<Animator>().SetBool("Shadow", true);
    }
    IEnumerator HouseEntryStage(GameObject house)
    {

        float firstDistance = Vector2.Distance(new Vector2(house.transform.position.x, house.transform.position.y), new Vector2(HouseOnStage.x, HouseOnStage.y));
        float distance = Mathf.Abs(HouseOnStage.x - house.transform.position.x);
        Vector3 pos = house.transform.position;
        pos.x = -20;
        pos.z = 20;
        house.transform.position = pos;
        SwapCamera(1, CinemachineBlendDefinition.Style.EaseInOut);

        while ( distance>0.1f )
        {
          house.transform.position = Vector2.Lerp(house.transform.position, HouseOnStage, (Time.deltaTime*distance)/distance);
            distance = Mathf.Abs(HouseOnStage.x - house.transform.position.x);
                                  Debug.Log(distance);

            yield return null;
        }
        NpcToFirstPlane(Showman.GetComponent<NpcController>().Identificator, false);
    }
    public IEnumerator GameOver()
    {


        while (Vector3.Distance(GameplayCam.transform.position, GameOverPos) > float.Epsilon)
        {
            Debug.Log("algo");
            GameplayCam.transform.position = Vector3.Slerp(GameplayCam.transform.position, GameOverPos, Time.deltaTime*0.5f);

            yield return null;

        }

    }    public IEnumerator SpectatorCam()
    {


        while (Vector3.Distance(GameplayCam.transform.position, GameOverPos) > float.Epsilon)
        {
            Debug.Log("algo");
            GameplayCam.transform.position = Vector3.Slerp(GameplayCam.transform.position, GameOverPos, Time.deltaTime*0.5f);

            yield return null;

        }

    }

   public IEnumerator InstantiateCandidates(int number)
    {
        Debug.Log("hellow");

        AvailableCandidates.Clear();
        AvailableCandidates = CharacterCase.OfType<GameObject>().ToList();

        for (int i =1; i<=number; i++)
        {               
            int random = Random.Range(0, AvailableCandidates.Count);
            yield return StartCoroutine(CheckCandidateRepeated(AvailableCandidates[random]));

        }

    }
   IEnumerator CheckCandidateRepeated(GameObject candidate)
    {

        bool complete = false;

        foreach (GameObject person in AvailableCandidates)
        {
            if (!complete)
            {
               if (person.name == candidate.name)
                {
                    CreateCharacter(candidate, AvailableNames[Random.Range(0, AvailableNames.Length)], Random.Range(1990, 2001), new Vector2(10, -0.3f), candidate.name);

                    complete = true;

                yield return null;
                }
                //AvailableCandidates.Add(candidate);
                //complete = true;

            }

        }
                               AvailableCandidates.Remove(candidate);
                           yield return null;

        }

    IEnumerator P2_CandidatesPresentation()
    {
        int candidateNumber = -1;
        foreach (Person candidate in Census)
        {

            candidateNumber++;
            yield return StartCoroutine(P2_CandidatePresentationTime(candidate, candidateNumber));

        }
        StartCoroutine(P2_CompileShadows());
    }
   
    IEnumerator P2_CandidatePresentationTime(Person candidate, int candidateNumb)
    {
           foreach(GameObject possibleShadow in CandidateShadows)
        {
            //Debug.Log(possibleShadow.name + ";" + candidate.ShadowRef);
            if (possibleShadow.name==candidate.ShadowRef)
            {
                GameObject shadow = Instantiate(possibleShadow) as GameObject;
                shadow.name = candidate.ShadowRef;
                shadow.transform.position = CandidatePresentationPos;
                ShadowsChecked.Add(shadow.gameObject);
                CurrentShadow = shadow;
                

            }                   yield return null;

        }
        SwapCamera(2, CinemachineBlendDefinition.Style.EaseIn);
        yield return new WaitForSeconds(gameObject.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time);


        OverlapCurrentShot(true);

        yield return StartCoroutine(TalkDialogue(CurrentShadow.GetComponent<ShadowCommands>().PresentationDialogue));
        OverlapCurrentShot(false);

        SwapCamera(1, CinemachineBlendDefinition.Style.Cut);
                            GameObject.Find("Brain").GetComponent<Manager>().OverlapCurrentShot(false);

        ShadowsChecked[candidateNumb].GetComponent<SpriteRenderer>().enabled=false;
        yield return new WaitForSeconds(gameObject.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time);


    }
    IEnumerator WaitingInput()
    {
        while (!Input.GetKeyDown(KeyCode.KeypadEnter) || TalkingDialogue)
        {

            yield return null;


        }
    }
    public IEnumerator TalkDialogue(string[] dialogue)
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            StartCoroutine(Talking((dialogue[i])));
            yield return StartCoroutine(WaitingInput());

        }
    }

 
    IEnumerator Talking(string line )
    {

        GameObject bocadilloDialogo = Instantiate(ComicDialogue) as GameObject;
                                                          
        bocadilloDialogo.transform.SetParent(GameObject.Find("Canvas").transform);
        bocadilloDialogo.transform.localPosition = BubbleSpawnPos;

        

        //Anim.SetBool("Talk", true);
        TalkingDialogue = true;
        //TextFramework.GetComponent<Image>().sprite = LightBox[1];

        //StartCoroutine(TextBoxTransform(bocadilloDialogo));
        for (int i = 0; i <= line.Length; i++)
        {


            string currentText = line.Substring(0, i);
            bocadilloDialogo.GetComponentInChildren<Text>().text = currentText;
            yield return new WaitForSeconds(TalkingDelay);

        }
        //TextFramework.GetComponent<Image>().sprite = LightBox[0];
        //Anim.SetBool("Talk", false);
        TalkingDialogue = false;   
      
        StopCoroutine("Talking");
    }
    IEnumerator P2_CompileShadows()
    {
        SwapCamera(2, CinemachineBlendDefinition.Style.Cut);
         for(int i =0; i<ShadowsChecked.Count;i++)
        {
            if (i==0)
            {
                ShadowsChecked[i].GetComponent<SpriteRenderer>().enabled = true;

                ShadowsChecked[i].transform.position = CompileShadowsPos ;
                yield return null;
            }
            else
            {
               ShadowsChecked[i].GetComponent<SpriteRenderer>().enabled = true;

            ShadowsChecked[i].transform.position = new Vector2(ShadowsChecked[i - 1].GetComponent<BoxCollider2D>().bounds.max.x + Mathf.Abs(ShadowsChecked[i].transform.position.x-ShadowsChecked[i].GetComponent<BoxCollider2D>().bounds.min.x), ShadowsChecked[i - 1].transform.position.y);
            yield return null;
            }            
        }
        Debug.Log("o");
        NpcToFirstPlane(Showman.GetComponent<NpcController>().Identificator, false);
        yield return StartCoroutine(WaitingInput()); 

        StartCoroutine(SpectatorCam());
            yield return StartCoroutine(WaitingInput());
    }
    public void hack ()
    {
        ShowPhase = ShowStatus.SHOW;
    }
}
