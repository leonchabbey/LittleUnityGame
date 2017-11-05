using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    OutOfScreen outOfScreen;

    private void Start() {
        outOfScreen = GetComponent<OutOfScreen>();
    }

    private void Update() {
        if (!outOfScreen.isInCameraView()) {
            Destroy(gameObject);
        }
            
    }

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
