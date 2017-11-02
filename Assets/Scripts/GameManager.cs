using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private int life = 3;

    [SerializeField]
    private Text textLife;

    private const string TEXT_LIFE = "Life: ";

	// Use this for initialization
	void Start () {
        updateTextLife();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerDie() {
        if(life <= 0) {
            SceneManager.LoadScene("StartMenu");
        } else {
            life--;
            updateTextLife();
        }
    }

    void updateTextLife()
    {
        textLife.text = TEXT_LIFE + life;
    }
}
