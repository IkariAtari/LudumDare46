using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject MenuObject;
    [SerializeField] private GameObject HelpScreen;
    [SerializeField] private GameObject CommandsScreen;
    [SerializeField] private GameObject ControlsScreen;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private Text ScoreText;

    public bool Paused = true;

    public void ShowMenu()
    {
        MenuObject.SetActive(true);
    }

    public void HideMenu()
    {
        MenuObject.SetActive(false);
    }

    public void Help()
    {
        HelpScreen.SetActive(true);
    }

    public void Commands()
    {
        CommandsScreen.SetActive(true);
    }

    public void CloseHelp()
    {
        HelpScreen.SetActive(false);
    }

    public void CloseCommands()
    {
        CommandsScreen.SetActive(false);
    }

    public void Controls()
    {
        ControlsScreen.SetActive(true);
    }

    public void CloseConstrols()
    {
        ControlsScreen.SetActive(false);
    }

    public void SetGameOverScreen(int _score)
    {
        GameOverScreen.SetActive(true);
        ScoreText.text = _score.ToString();
    }

    public void StartOver()
    {
        GameOverScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
