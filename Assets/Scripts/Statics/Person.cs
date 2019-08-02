using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person
{
    public ShowCensus._HouseType HouseType { get; set; }
    private int _BirthYear;
    private int _ID;
    private bool _Candidate;
    private GameObject _Appearance;
    private string _Name;
    private string _Type;
    Npc_SpecialComponent special;
    private string _ShadowRef;
    private GameObject _StageBuddy;
    public string ShadowRef
    {
        get
        {
            return _ShadowRef;
        }
        set
        {
            _ShadowRef = value;
        }
    }
    public GameObject StageBuddy
    {
        get
        {
            return _StageBuddy;
        }
        set
        {
            _StageBuddy = value;
        }
    }


    public string Name { get; set; }
    public GameObject Appearance { get; set; }
    public int BirthYear
    {
        get
        {
            return this._BirthYear;
        }
        set
        {
            this._BirthYear = value;
        }
    }
        public int ID
    {
        get
        {
            return this._ID;
        }
        set
        {
            this._ID = value;
        }
    }
    public string Type
    {
        get
        {
            return this._Type;
        }
        set
        {
            this._Type = value;
        }
    }

    public bool Candidate
    {
        get
        {
            return this._Candidate;
        }
        set
        {
             this._Candidate = value;
        }
    }

    
         public Person()
    {
        ID = Manager.IDAssign;
        Manager.IDAssign++;

    }
   // public Person(GameObject personPrefab, string name, int birthYear)
   //{
   //     ID = Manager.IDAssign;
   //     Manager.IDAssign++;

   //     this.Appearance = personPrefab;
   //     this.Name = name;
   //     this.BirthYear = birthYear;
   //     Appearance = GameObject.Instantiate(Appearance) as GameObject;
   //     Appearance.AddComponent<NpcController>();
   //     Appearance.GetComponent<NpcController>().Identificator = this;
   //     Debug.Log("Name:" + this.Name + ";Year:" + BirthYear);

   // }
    public Person(GameObject person, Person identity)
   {
        person.GetComponent<NpcController>().Identificator = identity;

    }
    public Person(GameObject personPrefab, string name, int birthYear)
    {
        ID = Manager.IDAssign;
        Manager.IDAssign++;
        ShadowRef = "Shadow_" +personPrefab.name;

        Type = personPrefab.name;

        this.Appearance = personPrefab;
        this.Name = name;
        this.BirthYear = birthYear;
        StageBuddy = Appearance.gameObject.GetComponent<Buddy>().RagDoll;




    }

    

}
