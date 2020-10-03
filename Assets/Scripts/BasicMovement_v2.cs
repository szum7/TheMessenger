using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement_v2 : MonoBehaviour {

    public Animator animator;
    public float speed;

    void Start() {
        
    }

    void Update() {

        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));

        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        //Vector3 vertical = new Vector3(Input.GetAxis("Vertical"), 0.0f, 0.0f);
        transform.position += horizontal * speed;
    }
}
