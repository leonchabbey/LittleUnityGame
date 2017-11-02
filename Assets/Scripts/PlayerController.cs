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

    private Transform spawnTransform;

    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        spawnTransform = GameObject.Find("Spawn").transform;
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
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Limit")
            transform.position = spawnTransform.position;
    }
}
