using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecetteComponent : MonoBehaviour
{
    public List<PlanetComponent.PlanetColor> RecetteTargert;
    public PlanetComponent.PlanetColor UnwandedPlanetColor;
    public PlanetComponent.PlanetColor BonusPlanetColor;
    

    public List<Image> IndividualPanels;
    public List<Image> IndividualImages;
    public int NbTargetReady;

    public Sprite SpriteRedPlanet;
    public Sprite SpriteBeuPlanet;
    public Sprite SpriteGreenPlanet;
    public Sprite SpriteSun;
    public Sprite SpriteNebula;
    public Sprite SpriteAsteroide;

    public void SetRecettePanel() {
        for (int i = 0; i < RecetteTargert.Count; i++) {
            switch (RecetteTargert[i])
            {
                case PlanetComponent.PlanetColor.Red :
                    //IndividualImages[i].color = Color.red;
                    IndividualImages[i].sprite = SpriteRedPlanet;
                    break;
                case PlanetComponent.PlanetColor.Bleu :
                    //IndividualImages[i].color = Color.blue;
                    IndividualImages[i].sprite = SpriteBeuPlanet;
                    break;
                case  PlanetComponent.PlanetColor.Green :
                    //IndividualImages[i].color = Color.green;
                    IndividualImages[i].sprite = SpriteGreenPlanet;
                    break;
            }
        }

        if (UnwandedPlanetColor !=PlanetComponent.PlanetColor.none)
        {
            IndividualPanels[4].gameObject.SetActive(true);
            switch (UnwandedPlanetColor)
            {
                case PlanetComponent.PlanetColor.Red: break;
                case PlanetComponent.PlanetColor.Bleu: break;
                case PlanetComponent.PlanetColor.Green: break;
                case PlanetComponent.PlanetColor.Sun: 
                    //IndividualImages[4].color = Color.yellow;
                    IndividualImages[4].sprite = SpriteSun;
                    break;
                case PlanetComponent.PlanetColor.Meteor:
                   // IndividualImages[4].color = Color.grey;
                    IndividualImages[4].sprite = SpriteAsteroide;
                    break;
                case PlanetComponent.PlanetColor.Nebuleuse:
                    //IndividualImages[4].color = Color.magenta;
                    IndividualImages[4].sprite = SpriteNebula;
                    break;
                case PlanetComponent.PlanetColor.none: break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        else IndividualPanels[4].gameObject.SetActive(false);

        if (BonusPlanetColor != PlanetComponent.PlanetColor.none)
        {
            IndividualPanels[5].gameObject.SetActive(true);
            switch (BonusPlanetColor) {
                case PlanetComponent.PlanetColor.Red: break;
                case PlanetComponent.PlanetColor.Bleu: break;
                case PlanetComponent.PlanetColor.Green: break;
                case PlanetComponent.PlanetColor.Sun: 
                    //IndividualImages[4].color = Color.yellow;
                    IndividualImages[5].sprite = SpriteSun;
                    break;
                case PlanetComponent.PlanetColor.Meteor:
                    // IndividualImages[4].color = Color.grey;
                    IndividualImages[5].sprite = SpriteAsteroide;
                    break;
                case PlanetComponent.PlanetColor.Nebuleuse:
                    //IndividualImages[4].color = Color.magenta;
                    IndividualImages[5].sprite = SpriteNebula;
                    break;
                case PlanetComponent.PlanetColor.none: break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        else IndividualPanels[5].gameObject.SetActive(false);
        
        
    }

    public void ConfirmOneTarget()
    {
        IndividualPanels[NbTargetReady].color = Color.green;
        NbTargetReady++;
    }

    public void GoBackCheck()
    {
        NbTargetReady--;
        IndividualPanels[NbTargetReady].color = Color.white;
        
    }
}
