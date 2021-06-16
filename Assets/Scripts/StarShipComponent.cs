using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class StarShipComponent : MonoBehaviour
{

    //public Transform[] Targets;
    public int IndexTarget;
    //public List<PlanetComponent.PlanetColor> recette;
    public CurseOrder CurseOrder;

    public float Speed;
    public float DistanceToContact = 1;

    public Action<int> OnShipDestion;
    public SpriteRenderer HappySprite;
    public SpriteRenderer SadSrite;
    public Transform GraphiqueParte;
    [Header("Score Parameters")] 
    public float DetectRange = 3;


    public int ScoreOnBonus = 50;
    public int ScoreOnMalus = -50;
    public int ScoreForTarget =+20;
    public int ScoreForNothing=-10;
    public int ShipScore;
    public float CheckInterval = 2;
    public Text TxtShipScore;
    private float _checkTimer;
    
    

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = CurseOrder.Targets[IndexTarget].position - transform.position;
        transform.position += dir.normalized * Speed * Time.deltaTime;
        if (DistanceToContact > Vector3.Distance(CurseOrder.Targets[IndexTarget].position, transform.position)) {
            if (IndexTarget + 1 >= CurseOrder.Targets.Length) {
                Debug.Log(" TourFinie");
                OnShipDestion.Invoke(ShipScore);
                Destroy(gameObject);
                return;
            }
            IndexTarget++;
        }
        GraphiqueParte.forward = dir;

        _checkTimer += Time.deltaTime;
        if (_checkTimer >= CheckInterval)
        {
            _checkTimer = 0;
            //CheckWhatInRange();
            ShipScore += ScoreForNothing;
            TxtShipScore.text = ShipScore + "";
        }
    }

    private void CheckWhatInRange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, DetectRange);
        foreach (Collider col in cols) {
            if (col.transform.GetComponent<PlanetComponent>() != null)
            {
                PlanetComponent planetComponent = col.transform.GetComponent<PlanetComponent>();
                if (CurseOrder.Targets.Contains(col.transform)) {
                    ShipScore += ScoreForTarget;
                }

                if (CurseOrder.UnwandedPlanetColor == planetComponent.ColorPlanet) {
                    ShipScore += ScoreOnMalus;
                    SadSrite.DOFade(1, 0.5f).OnComplete(delegate { SadSrite.DOFade(0, 0.5f); });
                    Debug.Log("Do Sad");
                }
                
                if (CurseOrder.BonusPlanetColor == planetComponent.ColorPlanet) {
                    ShipScore += ScoreOnBonus;
                    HappySprite.DOFade(1, 0.5f).OnComplete(delegate { HappySprite.DOFade(0, 0.5f); });
                    Debug.Log("Do Happy");
                }
            }
        }

        if (cols.Length == 0) ShipScore += ScoreForNothing;
        TxtShipScore.text = ShipScore + "";
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.GetComponent<PlanetComponent>() != null)
        {
            PlanetComponent planetComponent = col.transform.GetComponent<PlanetComponent>();
            if (CurseOrder.Targets.Contains(col.transform)) {
                ShipScore += ScoreForTarget;
            }

            if (CurseOrder.UnwandedPlanetColor == planetComponent.ColorPlanet) {
                ShipScore += ScoreOnMalus;
                SadSrite.DOFade(1, 0.5f).OnComplete(delegate { SadSrite.DOFade(0, 0.5f); });
                Debug.Log("Do Sad");
            }
                
            if (CurseOrder.BonusPlanetColor == planetComponent.ColorPlanet) {
                ShipScore += ScoreOnBonus;
                HappySprite.DOFade(1, 0.5f).OnComplete(delegate { HappySprite.DOFade(0, 0.5f); });
                Debug.Log("Do Happy");
            }
        }
        TxtShipScore.text = ShipScore + "";
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position , DetectRange);
        Gizmos.color =Color.yellow;
    }
}
