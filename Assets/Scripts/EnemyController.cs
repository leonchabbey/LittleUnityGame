using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
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
}
