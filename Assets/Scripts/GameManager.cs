using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Text playerTextLife;

    private List<EnemyController> enemies = new List<EnemyController>();

    private const string TEXT_LIFE = "Life: ";

    public void playerIsDead() {
        SceneManager.LoadScene("DieMenu");
    }

    public void addEnemy(EnemyController enemy) {
        enemies.Add(enemy);
    }

    public void removeEnemy(EnemyController enemy) {
        enemies.Remove(enemy);
        checkEnemies();
    }

    public void checkEnemies() {
        if (enemies.Count == 0)
            SceneManager.LoadScene("WinMenu");
    }

    public void updatePayerTextLife(int playerLife) {
        playerTextLife.text = TEXT_LIFE + playerLife;
    }
}
