using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // Start is called before the first frame update
    public float leftBorder;
    public float rightBorder;
    public float moveSpeed;
    public bool leftDirection;
    public bool oneDirection;

    private Vector3 startPosition;
    private float addMoveShift;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (oneDirection)
        {
            if (leftDirection)
            {
                if (transform.position.x > leftBorder)
                {
                    transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
                }

            }
            else
            {
                if (transform.position.x < rightBorder)
                {
                    transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);
                }
            }
        }

        else
        {
            addMoveShift =  (rightBorder - leftBorder) * Mathf.Sin(Time.time * moveSpeed);
            transform.position = new Vector3(startPosition.x + addMoveShift, transform.position.y, transform.position.z);
        }
        
        
    }
}
