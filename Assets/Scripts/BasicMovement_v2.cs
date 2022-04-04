using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement_v2 : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public KeyCode previousArrowKey;
    bool isEasyMode;

    public BasicMovement_v2()
    {
        this.speed = 1;
        this.isEasyMode = true;
    }

    void Start()
    {
    }

    bool IsValidSprintSequenceKeyPressed()
    {
        if (isEasyMode)
        {
            return Input.GetKey(KeyCode.LeftArrow);
        }

        var validLeftArrowPress = Input.GetKey(KeyCode.LeftArrow) && (previousArrowKey == default(KeyCode) || previousArrowKey != KeyCode.LeftArrow);
        var validRightArrowPress = Input.GetKey(KeyCode.RightArrow) && (previousArrowKey == default(KeyCode) || previousArrowKey != KeyCode.RightArrow);
        return validLeftArrowPress || validRightArrowPress;
    }

    void ResetSprint()
    {
        previousArrowKey = default(KeyCode);
        speed = 1;
        animator.speed = 1;
    }

    KeyCode GetPressedArrowKey()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            return KeyCode.RightArrow;
        return KeyCode.LeftArrow;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || 
            Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || 
            Input.GetKey(KeyCode.D))
        {
            float horizontal = 0;
            float vertical = 0;
            if (Input.GetKey(KeyCode.A)) horizontal = -1;
            if (Input.GetKey(KeyCode.D)) horizontal = 1;
            if (Input.GetKey(KeyCode.W)) vertical = 1;
            if (Input.GetKey(KeyCode.S)) vertical = -1;

            var movement = new Vector3(horizontal, vertical, 0.0f);

            if (IsValidSprintSequenceKeyPressed())
            {
                this.speed += 0.015f;

                if (!isEasyMode)
                {
                    this.previousArrowKey = GetPressedArrowKey();
                }
            }

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Magnitude", movement.magnitude);

            transform.position = transform.position + (movement * this.speed) * Time.deltaTime;

            animator.speed = this.speed;
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Magnitude", 0);

            this.ResetSprint();
        }
        
        //Debug.Log("here");
    }

    void Update_old()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;
    }

    //void Movement_v1()
    //{
    //    animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
    //    animator.SetFloat("Vertical", Input.GetAxis("Vertical"));

    //    Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
    //    transform.position += horizontal * speed;
    //}
}
