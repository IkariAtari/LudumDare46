using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] towerObjects;
    [SerializeField] string[] Names;

    public Dictionary<string, GameObject> Towers = new Dictionary<string, GameObject>();
    public int cpuUsage;
    public int Memory;
    [SerializeField] private int Energy;

    public int Score = 0;

    public UIManager ui;
    public Menu Menu;
    public EnemyManager enemyManager;

    public static GameManager Instance;

    private bool Paused;

    private Camera Camera;

    private GameObject LastClicked;

    private void Awake()
    {
        Instance = this;
        Pause();

        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Start()
    {
        PopulateDictionary();
        ui = GetComponent<UIManager>();
        enemyManager = GetComponent<EnemyManager>();
        ui.SetEnergy(Energy);
        ui.SetCPU(cpuUsage);
    }

    private void Update()
    {
        if(cpuUsage >= 100)
        {
            GameOver();
        }

        if(Input.GetMouseButtonDown(0))
        {
            Ray _ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            if(Physics.Raycast(_ray, out _hit))
            {
                if(_hit.collider.gameObject != null)
                {
                    if(_hit.collider.gameObject.GetComponent<PlaceableObject>() != null)
                    {
                        _hit.collider.gameObject.GetComponent<PlaceableObject>().Clicked();
                        LastClicked = _hit.collider.gameObject;
                    }
                    else
                    {
                        ui.UnsetInformationPanelTowers();
                        LastClicked.GetComponent<PlaceableObject>().UnClicked();
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        Menu.Paused = true;
        Menu.ShowMenu();

        ui.DeactivateCommandLine();

        Time.timeScale = 0f;
 
    }

    public void UnPause()
    {
        Menu.Paused = false;
        Menu.HideMenu();

        Time.timeScale = 1f;

        ui.ActivateCommandLine();
    }

    private void GameOver()
    {
        Reset();
        
        Menu.SetGameOverScreen(Score);
    }


    private void Reset()
    {
        Score = 0;
        cpuUsage = 0;
        Energy = 300;
        EnemyManager.Wave = 1;
        enemyManager.SpawnRate = 300;
        enemyManager.SpawnCount = 10;
        enemyManager.Activated = false;
        enemyManager.TimeToNextWave = 0;
        ui.ToggleButton(true);
        ui.SetCPU(cpuUsage);
        ui.SetWave(EnemyManager.Wave.ToString());
        ui.SetEnemies((int)enemyManager.SpawnCount);
        
        foreach(GameObject _object in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(_object);
        }

        Pause();
    }

    private void PopulateDictionary()
    {
        for(int i = 0; i < towerObjects.Length; i++)
        {
            Towers.Add(Names[i], towerObjects[i]);
        }
    }

    public bool Purchase(int _amount)
    {
        Debug.Log("test");
        if(_amount > Energy)
        {
            return false;
        }
        else
        {
            Energy -= _amount;
            ui.SetEnergy(Energy);

            return true;
        }
    }

    public void Earn(int _value)
    {
        if(enemyManager.Activated)
        {
            Energy += _value;
            ui.SetEnergy(Energy);
        }
    }

    public void SetUsage(int _usage)
    {
        cpuUsage += _usage;
        ui.SetCPU(cpuUsage);
    }

    public void AddScore(int _amount)
    {
        Score += _amount;
        ui.SetScore(Score);
    }
}
