using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Movement : MonoBehaviour
{
    float currentVelocity;
    float rotationSpeed = 50f;
    float speedModifier = 1;
    float impulseSpeedModifier = 15;
    float warpSpeedModifier = 50;
    bool atWarp = false;
    bool moving;
    GameObject ship;
    GameObject cursorObj;
    public Object targetPrefab;
    int planeLayer;
    Animator shipAnimator;
    GameObject warpEffects;
    Vector3 targetPosition;
    Vector3 cursorPosition;
    Quaternion shipRotation;
    bool rotated;

    void Start()
    {
        Cursor.visible = false;
        ship = this.gameObject;
        shipAnimator = ship.GetComponentInChildren<Animator>();
        warpEffects = GameObject.FindGameObjectWithTag("WarpEffects");
        warpEffects.SetActive(false);
        cursorObj = GameObject.FindGameObjectWithTag("Cursor");
        planeLayer = LayerMask.GetMask("Plane");
    }

    void Move()
    {
        if (atWarp)
        {
            currentVelocity = speedModifier * warpSpeedModifier;
        }
        if (!atWarp)
        {
            currentVelocity = speedModifier * impulseSpeedModifier;
        }
        if (ship.transform.position == targetPosition)
        {
            moving = false;
        }
        Turn();
        if (rotated)
        {
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, targetPosition, currentVelocity * Time.deltaTime);
        }
    }

    void Turn()
    {
        ship.transform.rotation = Quaternion.RotateTowards(ship.transform.rotation, shipRotation, rotationSpeed * Time.deltaTime);
        if (shipRotation.y < ship.transform.rotation.y)
        {
            shipAnimator.SetTrigger("TurnLeft");
            shipAnimator.ResetTrigger("NoTurn");
        }
        if (shipRotation.y > ship.transform.rotation.y)
        {
            shipAnimator.SetTrigger("TurnRight");
            shipAnimator.ResetTrigger("NoTurn");
        }
        if (rotated)
        {
            shipAnimator.ResetTrigger("TurnRight");
            shipAnimator.ResetTrigger("TurnLeft");
            shipAnimator.SetTrigger("NoTurn");
        }
        if (ship.transform.rotation == shipRotation)
        {
            rotated = true;
        }
        if (ship.transform.rotation != shipRotation)
        {
            rotated = false;
        }
    }

    void GoToClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000, planeLayer))
            {
                targetPosition = hit.point;
                GameObject tmpTarget = GameObject.Find("target(Clone)");
                if (tmpTarget != null)
                {
                    Destroy(tmpTarget);
                }
                Instantiate(targetPrefab, targetPosition, new Quaternion(0, 90, 0, 0));                
                Vector3 lookAtCursor = new Vector3(ship.transform.position.x - targetPosition.x,
                    ship.transform.position.y,
                     ship.transform.position.z - targetPosition.z);
                shipRotation = Quaternion.LookRotation(lookAtCursor);
                moving = true;
            }
        }
    }

    void CursorFollow()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000, planeLayer))
        {
            cursorPosition = hit.point;
            cursorObj.transform.position = cursorPosition;
        }
    }

    void Update()
    {
        GoToClick();
        CursorFollow();
        if (moving)
        {
            Move();
        }
    }

    void FixedUpdate()
    {
        
    }
}
