using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour {

    [Header("Setup")]
    [SerializeField]
    protected GameObject spawn;

    protected void outOfWorld(Collider2D collision) {
        if (collision.tag == "WorldLimit") {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            gameObject.transform.position = spawn.transform.position;
        }
    }
}
