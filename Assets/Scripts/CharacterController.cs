using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed,
                                   jumpHeight;
    [SerializeField] private Transform runTowards,
                                       parentSyncObject,
                                       laneSyncObject;
    [SerializeField] private Transform[] lanePosArr = new Transform[3];
    [SerializeField] private Rigidbody gameCharRB;
    private Vector3 swipeStartPos,
                    swipeEndPos;
    private float swipeDistance,
                  minSwipeDistance;
    private Lanes currLane;

    private const int PLATFORM_LAYER = 9;

    private void OnEnable()
    {
        anim.enabled = true;
        gameCharRB.constraints = RigidbodyConstraints.None;
        gameCharRB.freezeRotation = true;
    }
    private void Start()
    {
        minSwipeDistance = 100;
        currLane = Lanes.mid;
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
        gameCharRB.velocity = transform.forward * speed;
        gameCharRB.transform.LookAt(runTowards);
        parentSyncObject.position = transform.TransformPoint(gameCharRB.transform.localPosition);
        transform.position = parentSyncObject.position;
    }

    void CheckSwipe()
    {
        if (Input.touchCount > 0)
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
        if (xDistance < yDistance)
        {
            if (distance.y > 0)
            {
                Jump();
            }
            else if (distance.y < 0)
            {
                Debug.Log("Slide Swipe");
                anim.SetBool("Slide", true);
                //Slide
            }

        }
    }

    private void Jump()
    {
        Debug.Log("Jump Swipe");
        anim.SetBool("Jump", true);
        gameCharRB.AddForce(Vector3.up * jumpHeight * Time.deltaTime);
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
        EventsManager.Instance.ObstacleCollision();
    }
}
