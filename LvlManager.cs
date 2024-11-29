using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LvlManager : MonoBehaviour
{
    int currentLvl;
    [SerializeField] string[] loadScreenHints;
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text hintText;
    void Start()
    {
        currentLvl = SceneManager.GetActiveScene().buildIndex;
    }
    public void NextLvl()
    {
        //hintText.SetText(loadScreenHints[Random.Range(0, loadScreenHints.Length-1)]);
        //panel.SetActive(true);
        Invoke(nameof(LoadLvl),3);

    }
    void LoadLvl()
    {
        currentLvl++;
        if(currentLvl >= SceneManager.sceneCountInBuildSettings)
        {
            hintText.SetText("Thanks for playing my game!");
            panel.SetActive(true);
            Invoke(nameof(StartGame), 3);
        }
        SceneManager.LoadScene(currentLvl);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
}
