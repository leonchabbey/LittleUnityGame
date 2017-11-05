using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour {

    [SerializeField]
    private Button button;

    // Use this for initialization
    void Start() {
        Button startGameButton = button.GetComponent<Button>();
        startGameButton.onClick.AddListener(startGame);
    }

    // Update is called once per frame
    void Update() {

    }

    void startGame() {
        SceneManager.LoadScene("FirstLevel");
    }
}
