using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    float GameDuration = 60f;

    private float remainingTime;
    public float RemainingTime
    {
        get { return remainingTime; }
        private set
        {
            remainingTime = value;

            if (remainingTime < 0f)
            {
                OnGameEnded();
            }

            if(OnRemainingTimeChanged != null)
            {
                OnRemainingTimeChanged.Invoke(remainingTime);
            }
        }
    }

    public event Action<float> OnRemainingTimeChanged;


    private int score;
    public int Score
    {
        get { return score; }
        private set
        {
            score = value;

            if (OnScoreChanged != null)
            {
                OnScoreChanged.Invoke(score);
            }
        }
    }

    public event Action<int> OnScoreChanged;

    // Use this for initialization
    void Start () {

        Score = 0;

        RemainingTime = GameDuration;
        StartCoroutine(TimeCounterCoroutine());

        FindObjectOfType<BlockConnection>().OnConnection += UpdateScore;
	}

    IEnumerator TimeCounterCoroutine()
    {
        while(true)
        {
            RemainingTime -= 1f;
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnGameEnded()
    {
        PlayerPrefs.SetInt(PlayerPrefsConst.LastGameScore, Score);

        var record = PlayerPrefs.GetInt(PlayerPrefsConst.RecordScore, 0);

        if (record < Score)
        {
            PlayerPrefs.SetInt(PlayerPrefsConst.RecordScore, Score);
        }

        FindObjectOfType<ScreenChanger>().ChangeScene(SceneNames.Menu);
    }

    private void UpdateScore(int length)
    {
        Score += length * length;
    }
}
