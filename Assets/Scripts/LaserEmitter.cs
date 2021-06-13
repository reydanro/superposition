using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public float maxLength = 10;

    private LineRenderer line;
    private Transform endVfx;
    private BoxCollider2D collider;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;

        collider = GetComponent<BoxCollider2D>();

        endVfx = transform.Find("EndVFX");
    }

    void LateUpdate()
    {
        Vector3 maxEndPoint = transform.position + (transform.right * maxLength);
        RaycastHit2D hit = Physics2D.Linecast(transform.position, maxEndPoint, ~LayerMask.GetMask("Player", "Lasers", "Goal"));
        Vector3 endPoint = hit != null ? (Vector3)hit.point : maxEndPoint;

        //Debug.Log("Hit test: " + hit.collider.name + " - endPoint: " + endPoint);

        Vector3[] newPointsInLine = new Vector3[2];
        newPointsInLine[0] = transform.position;
        newPointsInLine[1] = endPoint;

        line.positionCount = newPointsInLine.Length;
        line.SetPositions(newPointsInLine);


        // Update particle system
        if (endVfx != null)
        {
            if (hit != null && Array.Exists(new string[] { "Ground", "Borders" }, layer => hit.collider.gameObject.layer == LayerMask.NameToLayer(layer)))
            {
                endVfx.gameObject.SetActive(true);
                endVfx.transform.position = endPoint;
                //endVfx.transform.rotation = Quaternion.FromToRotation(hit.collider.transform.forward, hit.normal);
            }
            else
            {
                endVfx.gameObject.SetActive(false);

            }
        }

        // Update collider
        if (hit != null)
        {
            var length = Vector3.Distance(transform.position, endPoint);
            collider.offset = new Vector2(length / 2, 0);
            collider.size = new Vector2(length, line.startWidth);
        }
    }
}