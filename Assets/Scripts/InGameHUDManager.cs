using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameHUDManager : MonoBehaviour
{
    public GameObject PanelEndGame;
    public Text TxtEndGameScore;
    public List<Image> Stars;
    public GameObject PanelIntro;
    [Header("linked Components")] 
    public GameManager GameManager;

    public AudioClip InGameMusic;

    public void Start()
    {
        Time.timeScale = 0;
        PanelIntro.SetActive(true);
        AudioManager.PlayMusic(InGameMusic);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEndGamePanel(int score , int NbStars)
    {
        PanelEndGame.SetActive(true);
        TxtEndGameScore.text = score + "";
        
        if (NbStars<=0) foreach (Image star in Stars) star.color = Color.gray;
        else if (NbStars == 1) {
            Stars[0].color = Color.yellow;
            Stars[1].color = Color.gray;
            Stars[2].color = Color.gray;
        }   
        else if (NbStars == 2) {
            Stars[0].color = Color.yellow;
            Stars[1].color = Color.yellow;
            Stars[2].color = Color.gray;
        }  
        else if (NbStars>=3) foreach (Image star in Stars) star.color = Color.yellow;
        
    }

    public void UIIntroUnderstand()
    {
        PanelIntro.SetActive(false);
        Time.timeScale = 1;
    }

    public void UIGoToMainMenu()
    {
        GameManager.GalaxieColor.Clear();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
