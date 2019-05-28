using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person
{
    private int _BirthYear;
    private int _ID;
    private bool _Candidate;
    private GameObject _Appearance;
    private string _Name;
    private string _Type;
    Npc_SpecialComponent special;


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
    public Person(GameObject personPrefab, string name, int birthYear)
   {
        ID = Manager.IDAssign;
        Manager.IDAssign++;

        this.Appearance = personPrefab;
        this.Name = name;
        this.BirthYear = birthYear;
        Appearance = GameObject.Instantiate(Appearance) as GameObject;
        Appearance.AddComponent<CharacterManager>();
        Debug.Log("Name:" + this.Name + ";Year:" + BirthYear);

    }
    public Person(GameObject personPrefab, string name, int birthYear,Vector3 pos, string type)
    {
        ID = Manager.IDAssign;
        Manager.IDAssign++;

        Type = type;

        this.Appearance = personPrefab;
        this.Name = name;
        this.BirthYear = birthYear;
        Appearance = GameObject.Instantiate(Appearance) as GameObject;
        Appearance.transform.localPosition = pos;
        Appearance.AddComponent<CharacterManager>();
        Debug.Log("Name:" + this.Name + ";Year:" + BirthYear);
        Appearance.GetComponent<NpcController>().NpcType = this.Type;
        Appearance.AddComponent<Npc_SpecialComponent>();
        Appearance.SetActive(false);

    }

}
