using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController {

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

    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.addEnemy(this);
        StartCoroutine(Fire());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Fire() {
        while(true) {
            yield return new WaitForSeconds(timeToFire);

            foreach(Transform gun in gunTransformList) {
                GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
                bullet.tag = "BulletEnemy";
                bullet.GetComponent<Rigidbody2D>().velocity = gun.right * bulletVelocity;
                Destroy(bullet, 5);
            }
        }
    }

    private void takeDamage() {
        life--;

        if (life <= 0) {
            gameManager.removeEnemy(this);
            //Destroy(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "BulletPlayer")
            takeDamage();

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        outOfWorld(collision);
    }
}
