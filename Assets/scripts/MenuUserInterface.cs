using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUserInterface : MonoBehaviour {

    [SerializeField]
    Number LastGameScore;

    [SerializeField]
    Number RecordScore;

	// Use this for initialization
	void Start () {
        LastGameScore.Value = PlayerPrefs.GetInt(PlayerPrefsConst.LastGameScore, 0);
        RecordScore.Value = PlayerPrefs.GetInt(PlayerPrefsConst.RecordScore, 0);
	}

    public void LoadGame()
    {
        FindObjectOfType<ScreenChanger>().ChangeScene(SceneNames.Game);
    }
}
