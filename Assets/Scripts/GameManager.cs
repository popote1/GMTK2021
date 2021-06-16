using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Transform RecetteConateur;
    public RecetteComponent PrefabRecetteComponent;
    public List<Transform> SaveTargets = new List<Transform>();
    public StarShipComponent StarShipComponent;
    public Text TxtScore;
    public int GeneralScore;
    public Text TxtTimer;
    public int PartieTimer;
    
    
    [Range(0, 200)] public int TimeFactor = 100;
    
    public LineRenderer LineRenderer;
    [Header("Recette Parameteres")]
    public Transform SpaceStation;
    public bool IsUsingUnWantedTarget;
    public bool IsUsingBonusTargets;
    public bool IsUsingDelayToNewRecettes;
    public float MinDelayToRecette;
    public float MaxDelayToRacette;
    public List<AudioClip> SFXSelection;
    public AudioClip SFXCancel;
    [Header("Meteor Parameters")]
    public MeteorComponent PrefabMeteorComponent;
    public int TimeBetweenMeteor;
    public List<MeteorTrajectory> MeteorTrajectories;
    
    [Header("GalaxieCOmposition")] 
    public int NbRedPlanet;
    public int NbBleuPlanet;
    public int NbGreenPlanet;
    public int NbSun;
    public int NbMeteor;
    public int NbNebuleuse;

    [Header("linked Components")] 
    public InGameHUDManager InGameHudManager;
    public DataHolder DataHolder;

    public static List<PlanetComponent.PlanetColor> GalaxieColor = new List<PlanetComponent.PlanetColor>();
    
    private RecetteComponent _selectedRecette;
    private float _gametimer;
    private float _meteorTimer;
    private float _recetteTimer;
    public void Start()
    {
        PutNewRecettePanel();
        _gametimer = PartieTimer;
        GameObject[] Dataholders = GameObject.FindGameObjectsWithTag("DataHolder");
        DataHolder = Dataholders[0].GetComponent<DataHolder>();

        DataHolder = Dataholders[0].GetComponent<DataHolder>();
    }

    public void AddTarget(PlanetComponent planet)
    {
        if (planet.ColorPlanet == _selectedRecette.RecetteTargert[_selectedRecette.NbTargetReady])
        {
            if (!SaveTargets.Contains(planet.transform))
            {
                AudioManager.PlaySound(SFXSelection[SaveTargets.Count]);
                SaveTargets.Add(planet.transform);
                _selectedRecette.ConfirmOneTarget();
                CameraShakeManager.DoShake(1);
                if (_selectedRecette.NbTargetReady == _selectedRecette.RecetteTargert.Count)
                {
                    if (SpaceStation!=null)SaveTargets.Add(SpaceStation);
                    CurseOrder curse = new CurseOrder(SaveTargets.ToArray(), _selectedRecette.RecetteTargert.ToArray(),
                        _selectedRecette.UnwandedPlanetColor, _selectedRecette.BonusPlanetColor);
                    LaunchShip(curse);
                    Destroy(_selectedRecette.gameObject);
                   if (IsUsingDelayToNewRecettes) _recetteTimer = Random.Range(MinDelayToRecette, MaxDelayToRacette);
                   else PutNewRecettePanel();
                   SaveTargets.Clear();
                    Debug.Log("Recette Confirmer , Lancement de la navette");
                }
            }
            else {
                CameraShakeManager.DoShake(2);
                Debug.Log("Planet Déja séléctionner");
            }
        }
        else {
            CameraShakeManager.DoShake(2);
            Debug.Log(" La planet ne corespond pas");
        }
    }

        [ContextMenu("Generate new Recette ")]
    public void PutNewRecettePanel()
    {
        SetGalaxiCount();
        RecetteComponent recetteComponent = Instantiate(PrefabRecetteComponent, RecetteConateur);
        recetteComponent.RecetteTargert = GetRecette(4);
        
        if (IsUsingUnWantedTarget) recetteComponent.UnwandedPlanetColor = GetUnwantedPlanetColor();
        else recetteComponent.UnwandedPlanetColor = PlanetComponent.PlanetColor.none;
        
        if (IsUsingBonusTargets) recetteComponent.BonusPlanetColor = GetBonusPlanetColor(recetteComponent.UnwandedPlanetColor);
        else recetteComponent.BonusPlanetColor = PlanetComponent.PlanetColor.none;
        
        recetteComponent.SetRecettePanel();
        _selectedRecette = recetteComponent;
    }

    public List<PlanetComponent.PlanetColor> GetRecette(int length)
    {
       
        List<PlanetComponent.PlanetColor> recette = new List<PlanetComponent.PlanetColor>();
        bool colorIsOk;
        for (int i = 0; i < length; i++)
        {
            colorIsOk = false;
            while (!colorIsOk) {
                int index = Random.Range(0, 3);
                switch (index) {
                    case 0:
                        if (NbRedPlanet > 0) {
                            NbRedPlanet--;
                            recette.Add(PlanetComponent.PlanetColor.Red);
                            colorIsOk = true;
                        }
                        break;
                    case 1:
                        if (NbBleuPlanet > 0) {
                            recette.Add(PlanetComponent.PlanetColor.Bleu);
                            NbBleuPlanet--;
                            colorIsOk = true;
                        }
                        break;
                    case 2:
                        if (NbGreenPlanet > 0) {
                            recette.Add(PlanetComponent.PlanetColor.Green);
                            NbGreenPlanet--;
                            colorIsOk = true;
                        }
                        break;
                }
            }
        }
        return recette;
    }

    public void AddScore(int toAdd)
    {
        GeneralScore += toAdd;
        TxtScore.text = GeneralScore + "";
    }

    private void LaunchShip(CurseOrder curseOrder)
    {
        StarShipComponent ship= Instantiate(StarShipComponent , SpaceStation.position , quaternion.identity);
        //ship.Targets = SaveTargets.ToArray();
        ship.CurseOrder = curseOrder;
        SaveTargets.Clear();
        //ship.recette = _selectedRecette.RecetteTargert;
        ship.IndexTarget = 0;
        ship.OnShipDestion += AddScore;
    }

    private void Update()
    {
        if (_selectedRecette.NbTargetReady > 0)
        {
            LineRenderer.enabled = true;
            List<Vector3> points = new List<Vector3>();
            if (SpaceStation!=null) points.Add(SpaceStation.position); 
            else points.Add(Vector3.zero);
            foreach (Transform target in SaveTargets)
            {
                points.Add(target.position);
            }
            Debug.Log("Il y a "+points.Count+" dans la ligne");
            LineRenderer.positionCount = points.Count;
            LineRenderer.SetPositions(points.ToArray());
        }
        else
        {
            LineRenderer.enabled = false;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            SaveTargets.Remove(SaveTargets[SaveTargets.Count - 1]);
            _selectedRecette.GoBackCheck();
            AudioManager.PlaySound(SFXCancel);
        }
        
        //Timer de Jeu
        _gametimer -= Time.deltaTime * TimeFactor / 100;
        TxtTimer.text = Mathf.FloorToInt(_gametimer) + "";
        if (_gametimer <= 0)
        {
            int stars;
            if (GeneralScore > DataHolder.LevelPanels[DataHolder.LevelIndex].ScoreStart3) {
                stars = 3;
                if (DataHolder.LevelIndex + 1 < DataHolder.LevelPanels.Count)
                    DataHolder.LevelPanels[DataHolder.LevelIndex + 1].IsUnlock = true;
            }
            else if (GeneralScore > DataHolder.LevelPanels[DataHolder.LevelIndex].ScoreStart2) {
                stars = 2;
                if (DataHolder.LevelIndex + 1 < DataHolder.LevelPanels.Count)
                    DataHolder.LevelPanels[DataHolder.LevelIndex + 1].IsUnlock = true;
            }
            else if (GeneralScore > DataHolder.LevelPanels[DataHolder.LevelIndex].ScoreStart1) stars = 1;
            else stars = 0;

            if (DataHolder.LevelPanels[DataHolder.LevelIndex].BestScore < GeneralScore) {
                DataHolder.LevelPanels[DataHolder.LevelIndex].BestScore = GeneralScore;
            }

            DataHolder.LevelPanels[DataHolder.LevelIndex].StarOwn = stars;
            InGameHudManager.SetEndGamePanel(GeneralScore , stars);
            Time.timeScale = 0;
            _gametimer = PartieTimer;
        }

        //Timer de Spawn des Meteors
        _meteorTimer += Time.deltaTime;
        if (_meteorTimer > TimeBetweenMeteor) {
            _meteorTimer = 0;
            SpawnMeteor();
        }

        
        //Timer des recettes
        if (IsUsingDelayToNewRecettes&& _recetteTimer!=0) {
            _recetteTimer -= Time.deltaTime;
            if (_recetteTimer <= 0) {
                PutNewRecettePanel();
                _recetteTimer = 0;
            }
        }
    }

    private void SetGalaxiCount()
    {
        NbRedPlanet = 0;
        NbBleuPlanet = 0;
        NbGreenPlanet = 0;
        NbSun = 0;
        NbMeteor = 0;
        NbNebuleuse = 0;
        foreach (PlanetComponent.PlanetColor color in GalaxieColor) {
            switch (color) {
                case PlanetComponent.PlanetColor.Red: NbRedPlanet++; break;
                case PlanetComponent.PlanetColor.Bleu: NbBleuPlanet++; break;
                case PlanetComponent.PlanetColor.Green: NbGreenPlanet++; break;
                case PlanetComponent.PlanetColor.Sun: NbSun++;  break;
                case PlanetComponent.PlanetColor.Meteor: NbMeteor++; break;
                case PlanetComponent.PlanetColor.Nebuleuse : NbNebuleuse++; break;
                case PlanetComponent.PlanetColor.none: break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

    private PlanetComponent.PlanetColor GetUnwantedPlanetColor()
    {
        int index = Random.Range(0, 6);
        if ((index == 3||index == 1)&& NbSun>0) return PlanetComponent.PlanetColor.Sun;
        if ((index == 4||index == 2)&& NbMeteor>0) return PlanetComponent.PlanetColor.Meteor;
        if ((index == 5||index == 0)&& NbNebuleuse>0) return PlanetComponent.PlanetColor.Nebuleuse;
        return PlanetComponent.PlanetColor.none;
    }

    private PlanetComponent.PlanetColor GetBonusPlanetColor(PlanetComponent.PlanetColor unwentedColor)
    {
        int index = Random.Range(0, 6);
        if (index == 3&& NbSun>0&&unwentedColor!=PlanetComponent.PlanetColor.Sun) return PlanetComponent.PlanetColor.Sun;
        if (index == 4&& NbMeteor>0&&unwentedColor!=PlanetComponent.PlanetColor.Meteor) return PlanetComponent.PlanetColor.Meteor;
        if (index == 5&& NbNebuleuse>0&&unwentedColor!=PlanetComponent.PlanetColor.Nebuleuse) return PlanetComponent.PlanetColor.Nebuleuse;
        return PlanetComponent.PlanetColor.none;
    }

    private void SpawnMeteor()
    {
        MeteorTrajectory traj = MeteorTrajectories[Random.Range(0, MeteorTrajectories.Count)];

        MeteorComponent meteor = Instantiate(PrefabMeteorComponent, traj.StartPos , Quaternion.identity);
        meteor.target = traj.EndPos;
        meteor.Speed = traj.Speed;
        meteor.lineRenderer.SetPositions(new Vector3[]{traj.StartPos, traj.EndPos});
    }
}
[Serializable]
public class MeteorTrajectory
{
    public Vector3 StartPos;
    public Vector3 EndPos;
    public float Speed = 5;
}

public struct CurseOrder
{
    public Transform[] Targets;
    public PlanetComponent.PlanetColor[] RecetteOrder;
    public PlanetComponent.PlanetColor UnwandedPlanetColor;
    public PlanetComponent.PlanetColor BonusPlanetColor;

    public CurseOrder(Transform[] targets, PlanetComponent.PlanetColor[] recetteOrder,
        PlanetComponent.PlanetColor unwandedPlanetColor, PlanetComponent.PlanetColor bonusPlanetColor) {
        Targets = targets;
        RecetteOrder = recetteOrder;
        UnwandedPlanetColor = unwandedPlanetColor;
        BonusPlanetColor = bonusPlanetColor;
    }
}
