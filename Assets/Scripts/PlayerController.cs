using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform gunTransform;
    [SerializeField]
    private float bulletVelocity = 10;
    [SerializeField]
    private float timeToFire = 2;
    private float lastTimeFire;

    private Transform spawnTransform;

    private Rigidbody2D rigid;

    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        spawnTransform = GameObject.Find("Spawn").transform;
        gameManager = FindObjectOfType<GameManager>();
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
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Limit") {
            transform.position = spawnTransform.position;
            gameManager.PlayerDie();
        }
    }

    private void Fire() {
        if(Time.realtimeSinceStartup - lastTimeFire > timeToFire) {
            GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = gunTransform.right * bulletVelocity;
            Destroy(bullet, 5);
            lastTimeFire = Time.realtimeSinceStartup;
        } 
    }
}
