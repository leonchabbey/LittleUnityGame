using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        switch(gameObject.tag) {
            case "BulletEnemy":
                if (collision.gameObject.tag == "Player")
                    Destroy(gameObject);
                break;
            case "BulletPlayer":
                if (collision.gameObject.tag == "Enemy")
                    Destroy(gameObject);
                break;
        }
    }

}
