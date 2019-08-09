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

    [Header("DefaultSpeechBubbleParameters")]
         public float TalkingDelay ;


    [Header("Phase2Commands")]
    public Vector3 CandidatePresentationPos;
    public Vector2 BubbleSpawnPos;
    public Vector3 CompileShadowsPos;
    public int CandidatePresentationTime;
    public float SpeechBubbleZoomMultiplier;
    [Range(0.05f,0.5f)]
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
        OverlapCamPanel.SetActive(false);


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



        StartCoroutine(InstantiateCandidates(3));

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
    public void ResumeShow()
    {
        ShowPhase = ShowStatus.SHOW;
    }


    public void SP1_EnterShowman(bool shadow)
    {
        if (Interlocutor1.GetComponent<Animator>().runtimeAnimatorController!=null)
        {
                                  Interlocutor1.GetComponent<NpcController>().Reset();

        }
        if (shadow)
        {
           StartCoroutine(SP1_Presentation());
        }
       if(shadow==false)
        {
            Curtain.GetComponent<Animator>().SetTrigger("PlayCurtain");
            Spotlights.SetActive(false);
            SpotLightAudiosource.Play();

        }
        NpcToFirstPlane(Showman.GetComponent<NpcController>().Identificator, shadow);
        //Interlocutor1.GetComponent<Animator>().SetBool("Shadow", true);
    }

    IEnumerator SP1_Presentation()
    {
        OverlapCam.SetActive(false);
        yield return new WaitForSeconds(2);
        SP1_EnterShowman(false);
        //Interlocutor1.GetComponent<NpcController>().Dialogue = ConversationManager.Instance.ShowmanScript.PossibleShowPresentations[Random.Range(0, ConversationManager.Instance.ShowmanScript.PossibleShowPresentations.Length)].PresentantionDialogue;



        //System.Array.Copy(FirstDialogue, Interlocutor1.GetComponent<NpcController>().Dialogue ,FirstDialogue.Length);
        ResumeShow();
        yield return StartCoroutine(Interlocutor1.GetComponent<NpcController>().TalkDialogue(ConversationManager.Instance.P1_ShowPresentation.PresentantionDialogue));

        //yield return StartCoroutine(WaitingInput());
        StartCoroutine(SP2_HouseEntryOnStage(Houses[0]));

    }





    IEnumerator SP2_HouseEntryOnStage(GameObject houseCandidate)  
    {
        Debug.Log("#####PHASE1####");                                             

        ShowPhase = ShowStatus.PHASE1;

        yield return StartCoroutine(WaitingInput());

        #region ShowmanPresentHouse

        //Interpretación de la presentación cambiando las palabras clave
        yield return StartCoroutine(ConversationManager.Instance.ReplaceDialogueKeywords(ConversationManager.Instance.P2_ShowmanPresentHouse));
        string[] showmanPresentHouse = ConversationManager.Instance.NextCheckedText;


        TextBoxCanvas.GetComponentInChildren<Text>().text = "";

        yield return StartCoroutine(NpcToFirstPlane2(Showman.GetComponent<NpcController>().Identificator, false));
                          
        StartCoroutine(Interlocutor1.GetComponent<NpcController>().TalkDialogue(showmanPresentHouse));

        yield return StartCoroutine(WaitingInput());
        #endregion



        #region House entry on stage
        SpotsPlane.SetActive(false);

        SwapCamera(1, CinemachineBlendDefinition.Style.EaseInOut);

        float timeToReachTarget = HouseEntrySeconds;
        float t = 0; 

        float firstDistance = Vector3.Distance(new Vector2(houseCandidate.transform.position.x, houseCandidate.transform.position.y), new Vector3(HouseOnStagePos.x, HouseOnStagePos.y));
        float distance = Mathf.Abs(HouseOnStagePos.x - houseCandidate.transform.position.x);      

        TextBoxCanvas.GetComponentInChildren<Text>().text = "";         

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
        #endregion

        //Showman aproaches de house

        yield return StartCoroutine(SP2_ShowmanBuddyMovesTowardsHouse()); 

        StartCoroutine(SP2_HouseFirstSpeak(houseCandidate));

    }        

    IEnumerator SP2_ShowmanBuddyMovesTowardsHouse()
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


            yield return null;
        }
        buddy.GetComponentInChildren<Animator>().SetFloat("Blend", 0);


    }

    IEnumerator SP2_HouseFirstSpeak(GameObject house)
    {     
        #region ShowmanAskHouse
        string[] showmanAsk = { ConversationManager.Instance.P2_ShowmanQuestion.ShowmanAsk }; 

        //Se cambian las palabras claves de la pregunta inicial a la casa y se guarda el diálogo resultante en la array de strings "showmanAsk"
        yield return StartCoroutine(ConversationManager.Instance.ReplaceDialogueKeywords(showmanAsk));
        showmanAsk = ConversationManager.Instance.NextCheckedText ;


        TextBoxCanvas.GetComponentInChildren<Text>().text = "";
        NpcToFirstPlane(Showman.GetComponent<NpcController>().Identificator, false);
        StartCoroutine(Interlocutor1.GetComponent<NpcController>().TalkDialogue(showmanAsk));

        yield return StartCoroutine(WaitingInput());

        #endregion   


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

            if (i== houseResponse.Length-1)
            {

                yield return StartCoroutine(WaitingLastBubble(speechBubble));
            } 
            
            yield return null;

        }

        TalkingDialogue = false;

        string[] showmanPresentCandidates =ConversationManager.Instance.P3_ShowmanPresentCandidates;

        yield return StartCoroutine(Interlocutor1.GetComponent<NpcController>().TalkDialogue(showmanPresentCandidates));

        StartCoroutine(SP3_CandidatesPresentation());
    }




    IEnumerator SP3_CandidatesPresentation()
    {
        OverlapCamPanel.SetActive(false);


        ShowPhase = ShowStatus.PHASE2;

        OverlapCamPanel.SetActive(true);
        int candidateNumber = -1;
        foreach (Person candidate in Census)
        {

            candidateNumber++;
            yield return StartCoroutine(SP3_CandidatePresentationTime(candidate, candidateNumber));

        }
        StartCoroutine(SP3_CompileShadows());
    }

    IEnumerator SP3_CandidatePresentationTime(Person candidate, int candidateNumb)
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

        yield return StartCoroutine(SP3_DistributePresentations(candidate));


        OverlapCurrentShot(false);

        SwapCamera(1, CinemachineBlendDefinition.Style.Cut);
        GameObject.Find("Brain").GetComponent<Manager>().OverlapCurrentShot(false);

        ShadowsChecked[candidateNumb].GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(gameObject.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time);


    }

    IEnumerator SP3_DistributePresentations(Person candidate)
    {
        string[] presentationDialogue = new string[1];

        if (candidate.Priority == ShowCensus._CandidatesFeeling.BEST)
        {
            presentationDialogue = new string[] { ConversationManager.Instance.P3_BestCandidatePresentation };


        }
        else if (candidate.Priority == ShowCensus._CandidatesFeeling.WORST)
        {
            presentationDialogue = new string[] { ConversationManager.Instance.P3_WorstCandidatePresentation };
        }
        else if (candidate.Priority == ShowCensus._CandidatesFeeling.NEUTRAL)
        {
            presentationDialogue = new string[] { ConversationManager.Instance.P3_NeutralCandidatePresentation };
        }


        yield return StartCoroutine(SP3_CandidatePresentationSpeak(presentationDialogue, candidate));
        yield return null;
    } 

    IEnumerator SP3_CandidatePresentationSpeak(string[] dialogue, Person candidate)
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

    IEnumerator SP3_CompileShadows()
    {
        OverlapCamPanel.SetActive(true);

        SwapCamera(2, CinemachineBlendDefinition.Style.EaseInOut);
        for (int i = 0; i < ShadowsChecked.Count; i++)
        {
            if (i == 0)
            {
                ShadowsChecked[i].GetComponent<SpriteRenderer>().enabled = true;

                ShadowsChecked[i].transform.position = CompileShadowsPos;
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





    public IEnumerator GameOver()
    {


        while (Vector3.Distance(GameplayCam.transform.position, GameOverPos) > float.Epsilon)
        {
            Debug.Log("algo");
            GameplayCam.transform.position = Vector3.Slerp(GameplayCam.transform.position, GameOverPos, Time.deltaTime*0.5f);

            yield return null;

        }

    }
    public IEnumerator SpectatorCam()
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
        StartCoroutine(SetCandidatesPriority());
        yield return null;

    }

    IEnumerator SetCandidatesPriority()
    {
        int census = Census.Count;
        foreach(Person candidate in Census)
        {
            if (HousesOnStage[0].GetComponent<HousePersonality>().HouseType == candidate.HouseType)
            {
                candidate.Priority = ShowCensus._CandidatesFeeling.BEST;
                census++;
                yield return null;     
            }
            else if (HousesOnStage[0].GetComponent<HousePersonality>().AntiHouseType == candidate.HouseType||census==2)
            {
                candidate.Priority = ShowCensus._CandidatesFeeling.WORST;
                census++;
                yield return null;
            }
            else
            {
                candidate.Priority = ShowCensus._CandidatesFeeling.NEUTRAL;
                census++;
                yield return null;
            }

        }

       

        yield return null;
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
        while (eventPlaying)
        {
            if (lastBubble.GetComponentInChildren<Image>().color.a<0.2f)
            {
                eventPlaying = false;
            }
            yield return null; 

        }
    }


  
  
    public void hack ()
    {
        ShowPhase = ShowStatus.SHOW;
    }
}
