using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : InteractableObject
{
    [Header("문설정")]
    public bool isOpen = false;
    public Vector3 openPosition;
    public float openSpeed = 2f;

    private Vector3 closedPosition;

    protected override void Start()
    {
        base.Start();
        objectName = "문";
        interactionText = "[E] 문 열기";
        interactionType = InteractionType.Building;
        
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.right * 3f;
    }

    protected override void AccessBuilding()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            interactionText = "[E] 문 닫기";

        }
    }

    IEnumerator MoveDoor(Vector3 targetposition)
    {
        while (Vector3.Distance(transform.position, targetposition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetposition, openSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetposition;
    }
}
