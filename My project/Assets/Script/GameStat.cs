using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStat : MonoBehaviour
{
    private float _gameTime;
    private int _gameScore;
    private float _gameEnergy;    // energy level [0..1]
    private int _maxScore;      // saved record
    private float _maxGameTime;
    private string _maxScoreFilename = "maxscore.sav";
    private string _maxDataFilename = "maxscore.json";
    private int _healthCount = 3;

    [SerializeField]
    private TMPro.TextMeshProUGUI clock;   // посилання на годинник у GameStat
    [SerializeField]
    private TMPro.TextMeshProUGUI score;   // посилання на Score(Text) у GameStat
    [SerializeField]
    private UnityEngine.UI.Image energy;   // посилання на Image змінного наповнення

    GameObject point1;
    GameObject point2;
    GameObject point3;

    GameMenu gameMenu;

    public float GameTime
    {
        get => _gameTime;
        set
        {
            _gameTime = value;
            UpdateUiTime();
        }
    }
    public int GameScore
    {
        get => _gameScore;
        set
        {
            _gameScore = value;
            UpdateUiScore();
        }
    }
    public float GameEnergy
    {
        get => _gameEnergy;
        set
        {
            _gameEnergy = value;
            UpdateUiEnergy();
        }
    }


    void Start()
    {
        gameMenu =                        
            GameObject.Find("GameMenu")   
            .GetComponent<GameMenu>();    
        point1 = GameObject.Find("Point1");
        point2 = GameObject.Find("Point2");
        point3 = GameObject.Find("Point3");
        GameEnergy = energy.fillAmount;
        if (System.IO.File.Exists(_maxScoreFilename))
        {
            string[] lines =
                System.IO.File.ReadAllLines(_maxScoreFilename, System.Text.Encoding.UTF8);

            try { _maxScore = Convert.ToInt32(lines[0]); } catch { _maxScore = 0; }
            try { _maxGameTime = Convert.ToSingle(lines[1]); } catch { _maxGameTime = 0; }
        }
        else
        {
            System.IO.File.WriteAllText(_maxScoreFilename, "0\n0");
            _maxScore = 0;
            _maxGameTime = 0;
        }

        if (System.IO.File.Exists(_maxDataFilename))
        {
            var data = JsonUtility.FromJson<MaxData>(
                System.IO.File.ReadAllText(_maxDataFilename, System.Text.Encoding.UTF8)
            );
            _maxScore = data.GameScore;
            _maxGameTime = data.GameTime;
        }

        Debug.Log($"GameStat Start: maxScore = {_maxScore}, maxGameTime = {_maxGameTime}");
    }
    void LateUpdate()
    {
        GameTime += Time.deltaTime;
    }
    private void OnDestroy()
    {
        

        System.IO.File.WriteAllText(_maxScoreFilename,
            $"{(_gameScore > _maxScore ? _gameScore : _maxScore)}\n{(_gameTime > _maxGameTime ? _gameTime : _maxGameTime)}");

        var data = new MaxData
        {
            GameScore = (_gameScore > _maxScore ? _gameScore : _maxScore),
            GameTime = (_gameTime > _maxGameTime ? _gameTime : _maxGameTime)
        };
        System.IO.File.WriteAllText(
            _maxDataFilename,
            JsonUtility.ToJson(data, true));
    }

    private void UpdateUiTime()
    {
        int t = (int)_gameTime;
        int h = t / 3600;
        int m = (t % 3600) / 60;
        int s = t % 60;
        int d = (int)((_gameTime - t) * 10);

        clock.text = $"{h:00}:{m:00}:{s:00}.{d}";
    }
    private void UpdateUiScore()
    {
        score.text = $"{_gameScore:0000}";
        if (_gameScore > _maxScore)
        {
            score.fontStyle = TMPro.FontStyles.Bold;
        }
    }
    private void UpdateUiEnergy()
    {
        if (_gameEnergy >= 0 && _gameEnergy <= 1)
        {
            energy.fillAmount = _gameEnergy;
        }
        else
        {
            Debug.LogError($"gameEnergy set error (out of range) {_gameEnergy}");
        }
    }

    public class MaxData
    {
        public int GameScore;
        public float GameTime;
    }

    public void GetDamage()
    {
        if (_healthCount == 3)
        {
            _healthCount -= 1;
            point3.SetActive(false);
        }
        else if (_healthCount == 2)
        {

            _healthCount -= 1;
            point2.SetActive(false);
        }
        else if (_healthCount == 1)
        {

            _healthCount -= 1;
            point1.SetActive(false);
            gameMenu.GameOver();

        }
    }
    public void GetHealth()
    {
        if (_healthCount == 3)
        {
            return;
        }
        else if (_healthCount == 2)
        {
            
            _healthCount += 1;
            point3.SetActive(true);
        }
        if (_healthCount == 1)
        {
            
            _healthCount += 1;
            point2.SetActive(true);

        }
    }
}
