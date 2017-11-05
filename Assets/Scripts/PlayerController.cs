using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Stats")]
    [SerializeField]
    private int life = 3;

    [Header("Physics")]
    [SerializeField]
    private float force = 10;

    [Header("Jump")]
    [SerializeField]
    private float forceJump = 1;
    [SerializeField]
    private Transform positionRaycastJump;
    [SerializeField]
    private float radiusRaycastJump;
    [SerializeField]
    private LayerMask layerMaskJump;

    [Header("Fire gun super sonic lol boum")]
    [SerializeField]
    private Transform gunTransform;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawnerTransform;
    [SerializeField]
    private float bulletVelocity = 10;
    [SerializeField]
    private float timeToFire = 2;
    private float lastTimeFire;

    private Rigidbody2D rigid;
    private GameManager gameManager;
    private Respawn respawn;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        respawn = GetComponent<Respawn>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.updatePayerTextLife(life);
	}
	
	// Update is called once per frame
	void Update () {
        float horizInput = Input.GetAxis("Horizontal");
        Vector2 forceDirection = new Vector2(horizInput, 0);
        forceDirection *= force;
        rigid.AddForce(forceDirection);

        bool onFloor = Physics2D.OverlapCircle(positionRaycastJump.position, radiusRaycastJump, layerMaskJump);

        if (Input.GetAxis("Jump") > 0 && onFloor)
            rigid.AddForce(Vector2.up*forceJump, ForceMode2D.Impulse);

        if (Input.GetAxis("Fire1") > 0)
            Fire();

        gunFollowMouse();
    }

    private void gunFollowMouse() {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        gunTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f + angle));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        respawn.outOfWorld(collision, takeDamage);

        if (collision.tag == "Heart") {
            lifeUp();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "BulletEnemy")
            takeDamage();  
    }

    private void lifeUp() {
        life++;
        gameManager.updatePayerTextLife(life);
    }

    private void takeDamage() {
        life--;
        gameManager.updatePayerTextLife(life);

        if (life <= 0)
            gameManager.playerIsDead();
    }

    private void Fire() {
        if(Time.realtimeSinceStartup - lastTimeFire > timeToFire) {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnerTransform.position, bulletSpawnerTransform.rotation);
            bullet.tag = "BulletPlayer";
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnerTransform.right * bulletVelocity;
            Destroy(bullet, 5);
            lastTimeFire = Time.realtimeSinceStartup;
        } 
    }
}
