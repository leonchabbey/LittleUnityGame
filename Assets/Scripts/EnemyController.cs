using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Stats")]
    [SerializeField]
    private int life = 2;

    [Header("Bullet")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletVelocity = 10;

    [Header("Guns")]
    [SerializeField]
    private Transform[] gunTransformList;
    [SerializeField]
    private float timeToFire = 2;
    private float lastTimeFire;

    private int numOfFlash = 3;
    private Color firstFlashColor = new Color(255, 0 ,0);
    private Color secondColor = new Color(255, 71, 71);
    private float flashingSpeed = 0.05f;

    private Color defaultColor;

    private GameManager gameManager;
    private Respawn respawn;
    private SpriteRenderer spriteRenderer;

    private Coroutine flashCoroutine = null;
    private bool isFlashCoroutineRunning = false;

    // Use this for initialization
    void Start() {
        respawn = GetComponent<Respawn>();
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = FindObjectOfType<SpriteRenderer>();
        gameManager.addEnemy(this);

        StartCoroutine(Fire());

        defaultColor = spriteRenderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void takeDamage() {
        life--;

        if (isFlashCoroutineRunning) {
            StopCoroutine(flashCoroutine);
            isFlashCoroutineRunning = false;
        }
           
        if (flashCoroutine == null || !isFlashCoroutineRunning)
            flashCoroutine = StartCoroutine(FlashEnemy());

        if (life <= 0) {
            gameManager.removeEnemy(this);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "BulletPlayer")
            takeDamage();

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        respawn.outOfWorld(collision);
    }

    private IEnumerator Fire() {
        while (true) {
            yield return new WaitForSeconds(timeToFire);

            foreach (Transform gun in gunTransformList) {
                GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
                bullet.tag = "BulletEnemy";
                bullet.GetComponent<Rigidbody2D>().velocity = gun.right * bulletVelocity;
                Destroy(bullet, 5);
            }
        }
    }

    private IEnumerator FlashEnemy() {
        isFlashCoroutineRunning = true;

        for (int i = 0; i < numOfFlash; i++) {
            spriteRenderer.material.color = firstFlashColor;
            yield return new WaitForSeconds(life * flashingSpeed);
            spriteRenderer.material.color = secondColor;
            yield return new WaitForSeconds(life * flashingSpeed);
        }
        spriteRenderer.material.color = defaultColor;

        isFlashCoroutineRunning = false;
    }
}
