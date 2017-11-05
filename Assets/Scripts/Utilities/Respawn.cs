using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    [Header("Respawn")]
    [SerializeField]
    private GameObject spawn;

    public void outOfWorld(Collider2D collision, Action callback = null) {
        if (collision.tag == "WorldLimit") {
            respawn();
            if (callback != null)
                callback();
        }
    }

    public void respawn() {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        rb.rotation = 0;

        gameObject.transform.position = spawn.transform.position;
    }
}
