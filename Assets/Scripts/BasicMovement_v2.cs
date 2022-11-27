using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement_v2 : MonoBehaviour
{
    public Animator animator;
    public float speedOffset;
    public bool isEasyMode;

    KeyCode previousArrowKey;
    float speed;

    public BasicMovement_v2()
    {
        this.SetPublicPropertiesToDefaultValues();
        this.SetPrivateProperties();
    }

    void SetPublicPropertiesToDefaultValues()
    {
        this.isEasyMode = true;
        this.speedOffset = 2;
    }

    void SetPrivateProperties()
    {
        this.speed = 1;
        this.previousArrowKey = default(KeyCode);
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || 
            Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || 
            Input.GetKey(KeyCode.D))
        {
            // TODO add slight acceleration when standing->running
            // TODO add slight deceleration when running->stopping/standing

            // Calculate facing
            float horizontal = 0;
            float vertical = 0;
            if (Input.GetKey(KeyCode.A)) horizontal = -1;
            if (Input.GetKey(KeyCode.D)) horizontal = 1;
            if (Input.GetKey(KeyCode.W)) vertical = 1;
            if (Input.GetKey(KeyCode.S)) vertical = -1;

            var movement = new Vector3(horizontal, vertical, 0.0f);

            // Set speed
            if (IsValidSprintSequenceKeyPressed())
            {
                speed += 0.015f;

                if (!isEasyMode)
                {
                    previousArrowKey = GetPressedArrowKey();
                }
            }

            // Set animation state
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Magnitude", movement.magnitude);

            // Set position
            transform.position = transform.position + ((movement * speed) * Time.deltaTime * speedOffset);

            // Set animation speed
            animator.speed = speed;
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Magnitude", 0);

            ResetSprint();
        }

        //Debug.Log("here");
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

    void Update_old()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;
    }
}
