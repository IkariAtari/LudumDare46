using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int Wave = 1;

    public float TimeToNextWave;
    public float SpawnRate;

    public float SpawnCount;

    public GameObject StartNode;

    public GameObject[] enemies;

    private float SpawnTimer;
    private float TimeToWave;
    public bool WaveActive;

    public bool Activated;

    private float SpawnCountVal;

    private void Start()
    {
        WaveActive = false;
        GameManager.Instance.ui.SetWave(Wave.ToString());
        GameManager.Instance.ui.SetEnemies((int)SpawnCount);
        GameManager.Instance.ui.SetTime(TimeToNextWave - TimeToWave);
    }

    private void Update()
    {
        if(Activated)
        {
            if(WaveActive)
            {
                if(SpawnTimer < SpawnRate)
                {
                    SpawnTimer++;
                }
                else
                {
                    if(SpawnCountVal <= SpawnCount)
                    {
                        SpawnEnemy(enemies[0]);
                        SpawnCountVal++;
                        SpawnTimer = 0;
                    }
                    else
                    {
                        NextWave();
                    }
                    
                }
            }
            else
            {
                if(TimeToWave < TimeToNextWave)
                {
                    TimeToWave++;
                    GameManager.Instance.ui.SetTime(TimeToNextWave - TimeToWave);
                }
                else
                {
                    WaveActive = true;
                }
            }
        }
    }

    private void SpawnEnemy(GameObject _enemy)
    {
        GameObject _object = Instantiate(_enemy, StartNode.transform.position, Quaternion.identity);
        _object.GetComponent<Enemy>().Node = StartNode;
    }
    
    public void StartGame()
    {
        Activated = true;
        GameManager.Instance.ui.ToggleButton(false);
    }

    private void NextWave()
    {
        WaveActive = false;
        Wave++;
        SpawnRate *= 0.98f;
        SpawnCount = Mathf.Ceil(SpawnCount * 1.15f);
        TimeToWave = 0;
        TimeToNextWave = 2000;
        SpawnCountVal = 0;

        GameManager.Instance.ui.SetEnemies((int)SpawnCount);
        GameManager.Instance.ui.SetWave(Wave.ToString());
        //GameManager.Instance.ui.ToggleButton(true);
    }
}
