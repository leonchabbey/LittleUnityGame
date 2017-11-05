using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfScreen : MonoBehaviour {

    private Camera mainCamera;

    private void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public bool isInCameraView() {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(rb.position);

        return screenPoint.z > 0
            && screenPoint.x > 0 && screenPoint.x < 1
            && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
