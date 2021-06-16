using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetComponent : MonoBehaviour
{
    public bool IsTargetable;
    public bool IsRandomPlanet;
    public Material RedMat;
    public Material BluMat;
    public Material GreenMat;
    public PlanetColor ColorPlanet;

    public List<GameObject> PlanetRed;
    public List<GameObject> PlanetBlue;
    public List<GameObject> PlanetGreen;

    private MeshRenderer _meshRenderer;
    public enum PlanetColor
    {
        Red, Bleu, Green , Sun , Meteor,Nebuleuse, none
    }
    
    
    void Awake()
    {
        if (IsRandomPlanet)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            int index = Random.Range(0, 3);
            switch (index)
            {
                case 0:
                    ColorPlanet = PlanetColor.Red;
                    _meshRenderer.material = RedMat;
                    PlanetRed[Random.Range(0,PlanetRed.Count)].SetActive(true);
                    break;
                case 1:
                    ColorPlanet = PlanetColor.Bleu;
                    _meshRenderer.material = BluMat;
                    PlanetBlue[Random.Range(0,PlanetBlue.Count)].SetActive(true);
                    break;
                case 2:
                    ColorPlanet = PlanetColor.Green;
                    _meshRenderer.material = GreenMat;
                    PlanetGreen[Random.Range(0,PlanetGreen.Count)].SetActive(true);
                    break;
            }
            _meshRenderer.enabled = false;
        }

        
        GameManager.GalaxieColor.Add(ColorPlanet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.GalaxieColor.Remove(ColorPlanet);
    }
}
