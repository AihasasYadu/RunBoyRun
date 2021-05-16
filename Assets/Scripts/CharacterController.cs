using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed,
                                   jumpHeight;
    [SerializeField] private Transform[] lanePosArr = new Transform[3];
    [SerializeField] private Vector3 colliderToggleSize;

    private const int PLATFORM_LAYER = 9;
    private Rigidbody gameCharRB;
    private bool isGrounded;
    private Vector3 swipeStartPos,
                    swipeEndPos;
    private float swipeDistance,
                  minSwipeDistance;
    private Lanes currLane;
    private BoxCollider charCollider;

    private void OnEnable()
    {
        anim.enabled = true;
    }
    private void Start()
    {
        gameCharRB = GetComponent<Rigidbody>();
        charCollider = GetComponent<BoxCollider>();
        isGrounded = true;
        minSwipeDistance = 100;
        currLane = Lanes.mid;
        //gameCharRB.drag = -900;
    }

    private void Update()
    {
        CheckSwipe();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 updateVel = transform.forward + new Vector3(0, gameCharRB.velocity.y, 0);
        gameCharRB.velocity = speed * updateVel * Time.deltaTime;
        anim.transform.localPosition = new Vector3(0, 0, 0);
        anim.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    void CheckSwipe()
    {
        if (Input.touchCount > 0 && !GameManager.Instance.isGamePaused)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                swipeStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeEndPos = touch.position;
                swipeDistance = (swipeEndPos - swipeStartPos).magnitude;
                if (swipeDistance > minSwipeDistance)
                {
                    SwipeAction();
                }
            }
        }
    }

    private void SwipeAction()
    {
        Vector2 distance = swipeEndPos - swipeStartPos;
        float xDistance = Mathf.Abs(distance.x);
        float yDistance = Mathf.Abs(distance.y);
        if (xDistance > yDistance)
        {

            if (distance.x > 0)
            {
                MoveRight();
                //Right
            }
            else if (distance.x < 0)
            {
                MoveLeft();
                //Left
            }
        }
        if (xDistance < yDistance && isGrounded)
        {
            if (distance.y > 0)
            {
                Jump();
            }
            else if (distance.y < 0)
            {

                StartCoroutine(Slide());
            }
        }
    }

    private void Jump()
    {
        Debug.Log("Jump Swipe");
        anim.SetBool("Jump", true);
        gameCharRB.AddForce(transform.up * jumpHeight);
        isGrounded = false;
    }

    private IEnumerator Slide()
    {
        isGrounded = false;
        Vector3 initialSize = charCollider.size;
        Vector3 initialCenter = charCollider.center;
        Debug.Log("Slide Swipe");
        anim.SetBool("Slide", true);
        Vector3 temp = charCollider.size;
        temp.y = temp.y / 2;
        charCollider.size = temp;
        charCollider.center = colliderToggleSize;

        yield return new WaitForSeconds(1);

        charCollider.size = initialSize;
        charCollider.center = initialCenter;
    }

    private void MoveLeft()
    {

        Debug.Log("Left Swipe");
        anim.SetBool("Move_Left", true);
        if (currLane != Lanes.left)
        {
            Transform temp = transform;
            temp.position = transform.TransformPoint(lanePosArr[(int)Lanes.left].localPosition);
            transform.position = Vector3.Lerp(transform.position, temp.position, speed / 2);
            currLane = currLane - 1;
        }
    }
    
    private void MoveRight()
    {
        Debug.Log("Right Swipe");
        anim.SetBool("Move_Right", true);
        if (currLane != Lanes.right)
        {
            Transform temp = transform;
            temp.position = transform.TransformPoint(lanePosArr[(int)Lanes.right].localPosition);
            transform.position = Vector3.Lerp(transform.position, temp.position, speed / 2);
            currLane = currLane + 1;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer.Equals(PLATFORM_LAYER) && !isGrounded)
        {
            isGrounded = true;
        }
    }
}
