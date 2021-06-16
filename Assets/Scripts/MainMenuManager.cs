using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public List<LevelPanel> LevelPanels;
    public DataHolder DataHolder;

    public int levelIndex=0;

    [Header("UI Elements")] 
    public Button BPPreview;
    public Button BPNext;
    public Button BPLevel;
    public Text TxtLevelName;
    public Image[] ImgLevelStars;
    public GameObject PanelLevelLock;

    public GameObject PanelMainMenu;
    public GameObject PanelOption;
    public GameObject PanelCredits;

    public AudioClip MenuMusic;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        
        GameObject[] Dataholders = GameObject.FindGameObjectsWithTag("DataHolder");
        foreach (var dataholder in Dataholders) { 
            if (dataholder.GetComponent<DataHolder>().LevelIndex > -1) {
                DataHolder = dataholder.GetComponent<DataHolder>();
                LevelPanels = DataHolder.LevelPanels;
                SetLevelPanel(DataHolder.LevelIndex);
                return;
            }
        }
        DataHolder = Dataholders[0].GetComponent<DataHolder>();
        SetLevelPanel(0);
        AudioManager.PlayMusic(MenuMusic);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void UIPreviewLevel()
    {
        if (levelIndex - 1 >= 0) SetLevelPanel(levelIndex - 1);
        else Debug.Log("On ne peux pas descendre en dessous du LVL 1");
    }
    
    public void UINextLevel()
    {
        if (levelIndex + 1 < LevelPanels.Count) SetLevelPanel(levelIndex + 1);
        else Debug.Log("Il n'y a pas plus de niveaux");
    }

    public void UICLickOnPlay()
    {
        SceneManager.LoadScene(LevelPanels[levelIndex].SceneIndex);
        DataHolder.LevelPanels = LevelPanels;
        DataHolder.LevelIndex = levelIndex;
    }

    public void UICLickOnOption()
    {
        PanelMainMenu.SetActive(false);
        PanelOption.SetActive(true);
    }

    public void UIClickReturnFromOption()
    {
        PanelMainMenu.SetActive(true);
        PanelOption.SetActive(false); 
    }

    public void UIClickCredit()
    {
        PanelMainMenu.SetActive(false);
        PanelCredits.SetActive(true);
    }

    public void UiClickReturnFromCredit()
    {
        PanelMainMenu.SetActive(true);
        PanelCredits.SetActive(false);
    }

    public void UIClickOnQuite()
    {
        Application.Quit();
    }

    private void SetLevelPanel(int indexLvl)
    {
        if (indexLvl == 0) BPPreview.interactable = false;
        else BPPreview.interactable = true;
        if (indexLvl >= LevelPanels.Count - 1) BPNext.interactable = false;
        else BPNext.interactable = true;
        if (LevelPanels[indexLvl].IsUnlock) {
            BPLevel.interactable = true;
            PanelLevelLock.SetActive(false);
        }
        else {
            BPLevel.interactable = true;
            PanelLevelLock.SetActive(true);
        }
        TxtLevelName.text = LevelPanels[indexLvl].Name;
        if (LevelPanels[indexLvl].StarOwn<=0) foreach (Image star in ImgLevelStars) star.color = Color.gray;
        else if (LevelPanels[indexLvl].StarOwn == 1) {
            ImgLevelStars[0].color = Color.yellow;
            ImgLevelStars[1].color = Color.gray;
            ImgLevelStars[2].color = Color.gray;
        } 
        else if (LevelPanels[indexLvl].StarOwn == 2) {
            ImgLevelStars[0].color = Color.yellow;
            ImgLevelStars[1].color = Color.yellow;
            ImgLevelStars[2].color = Color.gray;
        } 
        else if (LevelPanels[indexLvl].StarOwn>=3) foreach (Image star in ImgLevelStars) star.color = Color.yellow;
        levelIndex = indexLvl;
    }
}
[Serializable]
public class LevelPanel
{
    public string Name;
    public int BestScore;
    public int StarOwn;
    public bool IsUnlock;
    public int SceneIndex;
    public int ScoreStart1;
    public int ScoreStart2;
    public int ScoreStart3;
}
