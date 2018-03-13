using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour {

    [SerializeField]
    Number TimeCounter;

    [SerializeField]
    Number ScoreCounter;

	// Use this for initialization
	void Start () {
        FindObjectOfType<GameManager>().OnRemainingTimeChanged += time =>
            TimeCounter.Value = (int)time;

        FindObjectOfType<GameManager>().OnScoreChanged += score =>
    ScoreCounter.Value = (int)score;
    }
}
