using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameManager))]
public class UIManager : MonoBehaviour
{
    public Text[] TextObjects;
    public Button Button;
    private GameManager manager;

    public Text[] TextObjectsTowersPanel;

    public GameObject InformationPanelTowers;
    
    public GameObject CommandLine;

    private void Start()
    {
        manager = GetComponent<GameManager>();
    }

    public void SetCommandResult(string _result, Color _color)
    {
        TextObjects[0].text = _result;
        TextObjects[0].color = _color;
    }

    public void SetEnergy(int _energy)
    {
        TextObjects[1].text = _energy.ToString();
    }

    public void SetTime(float _time)
    {
        TextObjects[2].text = _time.ToString();
    }

    public void SetWave(string _wave)
    {
        TextObjects[3].text = "<round "+_wave+">";
    }

    public void ToggleButton(bool _toggle)
    {
        Button.interactable = _toggle;
    }

    public void SetEnemies(int _amount)
    {
        TextObjects[4].text = _amount.ToString();
    }

    public void SetCPU(int _amount)
    {
        TextObjects[5].text = _amount.ToString()+"%";
    }
    
    public void SetInformationPanelTowers(string[] _info)
    {
        InformationPanelTowers.SetActive(true);
        TextObjectsTowersPanel[0].text = _info[0];
        TextObjectsTowersPanel[1].text = _info[1];
        TextObjectsTowersPanel[2].text = _info[2];
        TextObjectsTowersPanel[3].text = _info[3];
        TextObjectsTowersPanel[4].text = _info[4];
        TextObjectsTowersPanel[5].text = _info[5];
        TextObjectsTowersPanel[6].text = _info[6];
    }

    public void UnsetInformationPanelTowers()
    {
        InformationPanelTowers.SetActive(false);
    }

    public void ActivateCommandLine()
    {
        CommandLine.SetActive(true);
    }

    public void DeactivateCommandLine()
    {
        CommandLine.SetActive(false);
    }

    public void SetScore(int _amount)
    {
        TextObjects[6].text = _amount.ToString();
    }
}
