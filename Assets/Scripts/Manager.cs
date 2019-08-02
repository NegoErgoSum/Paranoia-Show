//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Cinemachine;


public class Manager : MonoBehaviour
{

    private Conversation_Cassette CurrentCassette;

    //private Dialogue CurrentMovieScript;
    //public Conversation_House ShowmanPresentationCandidatesScript;
    public Vector3 ShowmanSpawnPos;
    [Header("Phase1Commands")]
    public GameObject SpotsPlane;
    [Range(2,10)]
    public float HouseEntrySpeed;
    public float HouseEntrySeconds;   
    public Vector3 HouseOnStagePos;
    public float ShowmanGreetingHouseTime;
    public Vector3 ShowmanGreetingHousePos;
    private string[] ShowmanGreetingText;


    [Header("Phase2Commands")]
    public Vector3 CandidatePresentationPos;
    public Vector2 BubbleSpawnPos;
    public Vector3 CompileShadowsPos;
    public int CandidatePresentationTime;
    public float SpeechBubbleZoomMultiplier;
    [Range(0.05f,0.5f)]
    public float TalkingDelay ;
    public float BubbleMaxScale;
    public float DistanceBetweenShadows;
    public GameObject ComicDialogue;
    public GameObject[] CandidateShadows;
    public Vector3[] BubbleSpeechCoord;
    public Vector3[] BubbleSpeechRot;
    private List<GameObject> ShadowsChecked;
    private bool TalkingDialogue;
    private Text TextBox;
    public GameObject CurrentShadow;


    [Header("Cameras")]
    public GameObject[] Cams;
    public GameObject OverlapCamPanel;
    public GameObject OverlapCam;
    public float[] OverlapCamScale;
    public Vector3[] OverlapCamCoord;
    public Vector3[] OverlapCamRot;
    public Vector3[] OverlapPanelCoord;
    public Vector3[] OverlapPanelScale;
    public Vector3[] OverlapPanelRot;
    public static GameObject CurrentCam;
    public GameObject Interlocutor1, Interlocutor2;
    public GameObject Spotlights;

    [Header ("HUD")]
    public GameObject TextFrameworkRef;
    public GameObject TextBoxRef;
    public Sprite[] TextFrameWorkSpritesRef;
    public Vector3 GameOverPos;
    private GameObject GameplayCam;

    public List<GameObject> HousesOnStage;


    private List<Person> _Census;

   
    
     [Header ("Hosts")]
    public GameObject ShowmanPrefab;
    private GameObject Showman;
    public GameObject[] Houses;
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

        HousesOnStage = new List<GameObject>();


        GameObject houseCandidate = Instantiate(Houses[Random.Range(0, Houses.Length)]) as GameObject;
        Vector3 spawnPos = HouseOnStagePos;
        spawnPos.x -= 20;
        houseCandidate.transform.position = spawnPos;
        HousesOnStage.Add(houseCandidate);

        CurrentCassette = ConversationManager.Instance.InsertCassette;

        ShowmanSignIn();

        ShadowsChecked = new List<GameObject>();
        TextBox = gameObject.GetComponentInChildren<Text>();

        SpotLightAudiosource = NewAudiosource(Sounds, false, false);

        //Spotlights.SetActive(false);
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
            OverlapCamPanel.SetActive(false);
            return;
        }
        UpdateOverlap();
        OverlapCamPanel.SetActive(true);
    }
   IEnumerator UpdateOverlap()
    {
        bool overlapCam = (OverlapCamPanel.activeSelf) ? true : false;
        OverlapCamPanel.SetActive(false);


        if (ShowPhase== ShowStatus.ADVERTISING)
        {
            OverlapCamPanel.SetActive(false);
            StopCoroutine(UpdateOverlap());
            yield return null;
        }
        //else if (ShowPhase == ShowStatus.PHASE2)
        //{
        //}
        yield return new WaitForSeconds(gameObject.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time);
        if (overlapCam)
        {
            OverlapCamPanel.SetActive(true);
        }
        for (int i = 0; i < Cams.Length; i++)
        {
            if (Cams[i] == CurrentCam)
            {
                OverlapCamPanel.transform.position = OverlapPanelCoord[i];
                //OverlapCamPanel.transform.localScale = OverlapPanelScale[i];
                OverlapCamPanel.transform.localEulerAngles = OverlapPanelRot[i];


                OverlapCam.transform.localPosition = OverlapCamCoord[i];
                OverlapCam.transform.localEulerAngles = OverlapCamRot[i];
                OverlapCam.GetComponent<Camera>().orthographicSize = OverlapCamScale[i];

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

        //Debug.Log(ShowPhase);
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

        GameObject buddy = Instantiate(citizen.GetComponent<NpcController>().Identificator.StageBuddy);
        buddy.transform.position = new Vector3(0, 100, 0);
        citizen.GetComponent<NpcController>().Identificator.StageBuddy = buddy;


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
        Debug.Log("#####PHASE1####");


      






        OverlapCamPanel.SetActive(false);
        ShowPhase = ShowStatus.PHASE1;

        //ConversationManager.Instance.current = HousesOnStage[0].GetComponent<HousePersonality>().Dialogues.dialogues[Random.Range(0, HousesOnStage[0].GetComponent<HousePersonality>().Dialogues.dialogues.Length)];

        //CurrentMovieScript = ConversationManager.Instance.current;




        StartCoroutine(P1_HouseEntryStage(HousesOnStage[0]));
    }
    public void Phase3_CandidatesPresentation()
    {
        OverlapCamPanel.SetActive(false);


        ShowPhase = ShowStatus.PHASE2;
        StartCoroutine(P3_CandidatesPresentation());
    }

    void AdjustInterlocutorSpawnScale()
    {

    }


   IEnumerator NpcToFirstPlane2(Person npc, bool shadow)
    {
        TextBoxCanvas.SetActive(true);

        //GameObject character = Instantiate(npc) as GameObject;
        if (Interlocutor1.activeSelf)
        {
            if (Interlocutor1.GetComponent<NpcController>().NpcType == "Showman")
            {
                Interlocutor1.GetComponent<NpcController>().Refresh(npc, 0, shadow);
            }
            else if (!Interlocutor2.activeSelf)
            {



                Interlocutor2.SetActive(true);
                Interlocutor2.GetComponent<NpcController>().Refresh(npc, 0, shadow);

            }
            else
            {
                Interlocutor2.SetActive(true);
                Interlocutor2.GetComponent<NpcController>().Refresh(npc, 1, shadow);
            }
        }
        //else  if (ShowPhase== ShowStatus.PHASE2)
        //{
        //    Interlocutor2.SetActive(true);
        //    Interlocutor2.GetComponent<NpcController>().Refresh(npc, 0, shadow);          
        //}
        else
        {
            Interlocutor1.SetActive(true);
            Interlocutor1.GetComponent<NpcController>().Refresh(npc, 0, shadow);
        }
        //character.transform.localPosition = new Vector3(0,0,-69);

        yield return null;
    }
    public  void NpcToFirstPlane(Person npc, bool shadow)
    {
        Debug.Log(Interlocutor1.GetComponent<NpcController>().NpcType);
        TextBoxCanvas.SetActive(true);

        //GameObject character = Instantiate(npc) as GameObject;
        if (Interlocutor1.activeSelf)
        {
            if (Interlocutor1.GetComponent<NpcController>().NpcType == "Showman")
            {
                Interlocutor1.GetComponent<NpcController>().Refresh(npc, 0, shadow);
            }
            else if (!Interlocutor2.activeSelf)
            {
                
                

                    Interlocutor2.SetActive(true);
                    Interlocutor2.GetComponent<NpcController>().Refresh(npc, 0, shadow);
                
            }
            else
            {
              Interlocutor2.SetActive(true);
            Interlocutor2.GetComponent<NpcController>().Refresh(npc, 1, shadow);
            }  
        }
        //else  if (ShowPhase== ShowStatus.PHASE2)
        //{
        //    Interlocutor2.SetActive(true);
        //    Interlocutor2.GetComponent<NpcController>().Refresh(npc, 0, shadow);          
        //}
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

       gameObject.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Style = blend;

        foreach (GameObject cam in Cams)
        {
            if (cam == Cams[shot])
            {
                cam.GetComponent<CinemachineVirtualCamera>().Priority = 2;
                CurrentCam = cam;
                StartCoroutine(UpdateOverlap());

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
        Showman.gameObject.GetComponent<NpcController>().Identificator.StageBuddy.transform.position = ShowmanSpawnPos;
       

    }
    IEnumerator Presentation()
    {
        OverlapCam.SetActive(false);
        yield return new  WaitForSeconds(2);
        EnterShowman(false);
        //Interlocutor1.GetComponent<NpcController>().Dialogue = ConversationManager.Instance.ShowmanScript.PossibleShowPresentations[Random.Range(0, ConversationManager.Instance.ShowmanScript.PossibleShowPresentations.Length)].PresentantionDialogue;
        
        
        
        //System.Array.Copy(FirstDialogue, Interlocutor1.GetComponent<NpcController>().Dialogue ,FirstDialogue.Length);
            ResumeShow();
        yield return StartCoroutine(Interlocutor1.GetComponent<NpcController>().TalkDialogue(  ConversationManager.Instance.P1_ShowPresentation.PresentantionDialogue));

        //yield return StartCoroutine(WaitingInput());
        Phase1_HousePresentation(Houses[0]);

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
           StartCoroutine(Presentation());
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
    IEnumerator P1_HouseEntryStage(GameObject houseCandidate)

    {
        Debug.Log("HouseEntryStage");

        yield return StartCoroutine(WaitingInput());

        #region ShowmanPresentHouse

        yield return StartCoroutine(ReplaceDialogueKeywords(ConversationManager.Instance.P2_ShowmanPresentHouse));
        string[] showmanPresentHouse = ConversationManager.Instance.NextCheckedText;


        TextBoxCanvas.GetComponentInChildren<Text>().text = "";

      yield return StartCoroutine(NpcToFirstPlane2(Showman.GetComponent<NpcController>().Identificator, false));
                          
        StartCoroutine(Interlocutor1.GetComponent<NpcController>().TalkDialogue(showmanPresentHouse));

        #endregion

        yield return StartCoroutine(WaitingInput());




       
        float timeToReachTarget = HouseEntrySeconds;
        float t = 0;




        float firstDistance = Vector3.Distance(new Vector2(houseCandidate.transform.position.x, houseCandidate.transform.position.y), new Vector3(HouseOnStagePos.x, HouseOnStagePos.y));
        float distance = Mathf.Abs(HouseOnStagePos.x - houseCandidate.transform.position.x);
        //Vector3 pos = houseCandidate.transform.position;
        //pos.x = -20;
        //pos.z = 20;
        //houseCandidate.transform.position = pos;
        //Interlocutor1.GetComponent<NpcController>().Dialogue = new string[] { gameObject.GetComponent<DialogueLibrary>().HousePresentations[Random.Range(0, gameObject.GetComponent<DialogueLibrary>().HousePresentations.Count)] };
        gameObject.GetComponent<DialogueLibrary>().SetName();



        //NpcToFirstPlane(Showman.GetComponent<NpcController>().Identificator, false);

        //string[] dialogue = ConversationManager.Instance.ShowmanScript.PossibleHousePresentations[Random.Range(0, ConversationManager.Instance.ShowmanScript.PossibleHousePresentations.Length)].HousePresentationDialogue;


        //yield return StartCoroutine(ReplaceDialogueKeywords(dialogue));

        TextBoxCanvas.GetComponentInChildren<Text>().text = "";

        //StartCoroutine(Interlocutor1.GetComponent<NpcController>().TalkDialogue(showmanAsk));

        //yield return StartCoroutine(WaitingInput());


        SpotsPlane.SetActive(false);
     
        SwapCamera(1, CinemachineBlendDefinition.Style.EaseInOut);

        yield return new WaitForSeconds(gameObject.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time/2);


        while (distance>0.1f)
        {

            float timeToResearchTargetSeconds = timeToReachTarget * 10;
            t += Time.deltaTime / timeToResearchTargetSeconds;

        
            houseCandidate.transform.position = Vector3.Lerp(houseCandidate.transform.position, HouseOnStagePos,t);

          //houseCandidate.GetComponent<Rigidbody>().velocity = new Vector3(1,0,0)* HouseEntrySpeed;
            distance = Mathf.Abs(HouseOnStagePos.x - houseCandidate.transform.position.x);
                                                                                                                
            yield return null;
        }

        yield return StartCoroutine(P1_ShowmanBuddyMovesTowardsHouse());

        //int randomInterviewBeginning = Random.Range(0, house.GetComponent<HousePersonality>().FirstInterview.Length);

        //string showmanFirstInterviewLine = house.GetComponent<HousePersonality>().FirstInterview[randomInterviewBeginning];
        //string[] showmanFirstInterviewDialogue= new string[1] { showmanFirstInterviewLine };                                           

        //Showman.GetComponent<NpcController>().Dialogue = showmanFirstInterviewDialogue;
        //NpcToFirstPlane(Showman.GetComponent<NpcController>().Identificator, false);


        //yield return StartCoroutine(Interlocutor1.GetComponent<NpcController>().TalkDialogue(dialogue));




        yield return StartCoroutine(P1_HouseFirstSpeak(houseCandidate));

    }
         IEnumerator ReplaceDialogueKeywords(string[] dialogue)
    {
        List<string> dialogueLines = new List<string>();

        foreach (string line in dialogue)
        {


            if (line.Contains("/houseName"))
            {
                string lineReplacement = line.Replace("/houseName", HousesOnStage[0].GetComponent<HousePersonality>().Name);

                dialogueLines.Add(lineReplacement);


            }
            else
            {
                dialogueLines.Add(line);
                yield return null;

            }


            yield return null;

        }
        ConversationManager.Instance.NextCheckedText = dialogueLines.ToArray();

            yield return dialogueLines.ToArray();

    }
    IEnumerator P1_HouseFirstSpeak(GameObject house)
    {


        #region ShowmanAskHouse
        string[] showmanAsk = { ConversationManager.Instance.P2_ShowmanQuestion.ShowmanAsk }; 

        yield return StartCoroutine(ReplaceDialogueKeywords(showmanAsk));
        showmanAsk = ConversationManager.Instance.NextCheckedText ;


        TextBoxCanvas.GetComponentInChildren<Text>().text = "";
        NpcToFirstPlane(Showman.GetComponent<NpcController>().Identificator, false);
        StartCoroutine(Interlocutor1.GetComponent<NpcController>().TalkDialogue(showmanAsk));
        Debug.Log("HouseFirstSpeak");


        #endregion

        yield return StartCoroutine(WaitingInput());




        string[] houseResponse = { ConversationManager.Instance.P2_ShowmanQuestion.HouseReponse.HouseResponse };



        for (int i = 0; i < houseResponse.Length; i++)
        {
            TalkingDialogue = true;



            GameObject speechBubble = Instantiate(ComicDialogue) as GameObject;
            speechBubble.AddComponent<SpeechBubbleManager>();
            speechBubble.transform.SetParent(GameObject.Find("Canvas").transform);

           

            SpeechBubbleManager speechBubble2 = new SpeechBubbleManager(house, houseResponse[i]);
            speechBubble2.CloneParameters(speechBubble.GetComponent<SpeechBubbleManager>());

            speechBubble.GetComponent<SpeechBubbleManager>().Initialize();



            TalkingDialogue = false;

            if (i== houseResponse.Length-1)
            {

                yield return StartCoroutine(WaitingLastBubble(speechBubble));
            }

           
            yield return null;

        }
       
        string[] showmanPresentCandidates =ConversationManager.Instance.P3_ShowmanPresentCandidates;
        StartCoroutine(Interlocutor1.GetComponent<NpcController>().TalkDialogue(showmanPresentCandidates));


        TalkingDialogue = true;
        yield return StartCoroutine(WaitingInput());

        Phase3_CandidatesPresentation();
    }
    IEnumerator P1_ShowmanBuddyMovesTowardsHouse()
    {
        float timeToResearchTargetSeconds = ShowmanGreetingHouseTime * 10;
        GameObject buddy = Showman.gameObject.GetComponent<NpcController>().Identificator.StageBuddy.gameObject;

        float distance = Vector3.Distance(buddy.transform.position, ShowmanGreetingHousePos);
    

        float t = 0;
        while (Mathf.Abs(distance) > 0.1f)
        {
            buddy.GetComponentInChildren<Animator>().SetFloat("Blend", 1);
            t += Time.deltaTime / timeToResearchTargetSeconds;


           buddy.transform.position = Vector3.Lerp(buddy.transform.position,ShowmanGreetingHousePos, t);
            distance = Vector3.Distance(buddy.transform.position, ShowmanGreetingHousePos);


            //houseCandidate.GetComponent<Rigidbody>().velocity = new Vector3(1,0,0)* HouseEntrySpeed;
            //distance = Mathf.Abs(HouseOnStagePos.x - houseCandidate.transform.position.x);

            yield return null;
        }
        buddy.GetComponentInChildren<Animator>().SetFloat("Blend", 0);


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

    IEnumerator P3_CandidatesPresentation()
    {
        OverlapCamPanel.SetActive(true);
        int candidateNumber = -1;
        foreach (Person candidate in Census)
        {

            candidateNumber++;
            yield return StartCoroutine(P3_CandidatePresentationTime(candidate, candidateNumber));

        }
        StartCoroutine(P2_CompileShadows());
    }
   
    IEnumerator P3_CandidatePresentationTime(Person candidate, int candidateNumb)
    {

        candidate.StageBuddy.transform.position = CandidatePresentationPos;
        candidate.StageBuddy.transform.eulerAngles = new Vector3(0, 90, 0);

        candidate.StageBuddy.GetComponentInChildren<SpriteRenderer>().color = Color.black;

        Color color = candidate.StageBuddy.GetComponentInChildren<SpriteRenderer>().color;
        color.a = 0.5f;

        candidate.StageBuddy.GetComponentInChildren<SpriteRenderer>().color = color;


        ShadowsChecked.Add(candidate.StageBuddy);
        CurrentShadow = candidate.StageBuddy;

        //foreach (GameObject possibleShadow in CandidateShadows)
        //{
        //    //Debug.Log(possibleShadow.name + ";" + candidate.ShadowRef);
        //    if (possibleShadow.name==candidate.ShadowRef)
        //    {
        //        GameObject shadow = Instantiate(possibleShadow) as GameObject;
        //        shadow.name = candidate.ShadowRef;
        //        shadow.transform.position = CandidatePresentationPos;
        //        shadow.transform.eulerAngles = new Vector3(0, 90, 0);
        //        ShadowsChecked.Add(shadow.gameObject);
        //        CurrentShadow = shadow;
        //    }
        //    yield return null;

        //}
        SwapCamera(2, CinemachineBlendDefinition.Style.EaseIn);
        yield return new WaitForSeconds(gameObject.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time);


        OverlapCurrentShot(true);

        yield return StartCoroutine(P3_DistributePresentations(candidate));
        OverlapCurrentShot(false);

        SwapCamera(1, CinemachineBlendDefinition.Style.Cut);
                            GameObject.Find("Brain").GetComponent<Manager>().OverlapCurrentShot(false);

        ShadowsChecked[candidateNumb].GetComponentInChildren<SpriteRenderer>().enabled=false;
        yield return new WaitForSeconds(gameObject.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time);


    }
    IEnumerator P3_DistributePresentations(Person candidate)
    {
        string[] presentationDialogue = new string[1];

        if (HousesOnStage[0].GetComponent<HousePersonality>().HouseType == candidate.HouseType)
        {
            presentationDialogue = new string[]{ ConversationManager.Instance.P3_BestCandidatePresentation};

        }
        else if (HousesOnStage[0].GetComponent<HousePersonality>().AntiHouseType == candidate.HouseType)
        {
            presentationDialogue = new string[] { ConversationManager.Instance.P3_WorstCandidatePresentation };
        } 
        else
        {
            presentationDialogue = new string[]{ ConversationManager.Instance.P3_NeutralCandidatePresentation };   
        }


         yield return StartCoroutine(P3_CandidatePresentationSpeak(presentationDialogue, candidate));
        yield return null;
    }

    public IEnumerator P3_CandidatePresentationSpeak(string[] dialogue, Person candidate)
    {
       

            for (int i = 0; i < dialogue.Length; i++)
        {
            TalkingDialogue = true;



            GameObject speechBubble = Instantiate(ComicDialogue) as GameObject;
            speechBubble.AddComponent<SpeechBubbleManager>();
            speechBubble.transform.SetParent(GameObject.Find("Canvas").transform);

            SpeechBubbleManager speechBubble2 = new SpeechBubbleManager(candidate, dialogue[i]);

            if (HousesOnStage[0].GetComponent<HousePersonality>().HouseType == candidate.HouseType)
            {
                speechBubble.gameObject.GetComponent<SpeechBubbleControl>().Protocol = HousesOnStage[0].GetComponent<HousePersonality>().Actitude;
                speechBubble2.Line = ConversationManager.Instance.P3_BestCandidatePresentation;

            }
            if (HousesOnStage[0].GetComponent<HousePersonality>().AntiHouseType == candidate.HouseType)
            {
                speechBubble2.Line = ConversationManager.Instance.P3_WorstCandidatePresentation;

            }
            else
            {
                speechBubble2.Line = ConversationManager.Instance.P3_NeutralCandidatePresentation;

            }



            speechBubble2.CloneParameters(speechBubble.GetComponent<SpeechBubbleManager>()); 
            speechBubble.GetComponent<SpeechBubbleManager>().Initialize();



            TalkingDialogue = false;
            yield return StartCoroutine(WaitingInput());

        }
    }


      IEnumerator WaitingInput()
    {
        while (!Input.GetKeyDown(KeyCode.KeypadEnter) || TalkingDialogue)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                TalkingDialogue = false;
            }

            yield return null;
        }
        StopCoroutine(WaitingInput());
    }
    IEnumerator WaitingLastBubble(GameObject lastBubble)
    {
        bool eventPlaying = true;
        while  (eventPlaying)
        {
            if (lastBubble.GetComponentInChildren<Image>().color.a<0.2f)
            {
                eventPlaying = false;
            }
            yield return null; 

        }
    }

    //IEnumerator UpdateBubbleSpeech(GameObject bubble)
    //{
    //    for (int i = 0; i < Cams.Length; i++)
    //    {
    //        if (Cams[i] == CurrentCam)
    //        {
    //            bubble.transform.localPosition = CurrentShadow.transform.localPosition;
    //            //bubble.transform.localEulerAngles = BubbleSpeechRot[i];
                
    //        }
    //        yield return null;
    //    }
    //}
 
  
  
    IEnumerator P2_CompileShadows()
    {
        OverlapCamPanel.SetActive(true);

        SwapCamera(2, CinemachineBlendDefinition.Style.EaseInOut);
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

            ShadowsChecked[i].transform.position = new Vector3(ShadowsChecked[i - 1].transform.position.x, ShadowsChecked[i - 1].transform.position.y, ShadowsChecked[i - 1].GetComponent<BoxCollider>().bounds.max.z  /*-Mathf.Abs(ShadowsChecked[i].transform.position.z + ShadowsChecked[i].GetComponent<BoxCollider>().bounds.min.z)*/- DistanceBetweenShadows);
            yield return null;
            }            
        }
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
