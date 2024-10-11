using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivator : MonoBehaviour
{
    public Transform objectToMove;
    public Vector2 moveDirection;
    public float moveDistance;
    public float moveSpeed;
    private bool isPlayerOnButton = false;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        startPosition = objectToMove.position;
        targetPosition = startPosition + (Vector3)moveDirection.normalized * moveDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine && collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnButton = true;
            if (!isMoving)
            {
                StartCoroutine(MoveObject());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine && collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnButton = false;
            StopCoroutine(MoveObject());
            isMoving = false;
        }
    }

    private IEnumerator MoveObject()
    {
        isMoving = true;
        while (isPlayerOnButton)
        {
            objectToMove.position = Vector3.MoveTowards(objectToMove.position, targetPosition, moveSpeed * Time.deltaTime);

            // Si llegamos al final, invertimos la dirección
            if (Vector3.Distance(objectToMove.position, targetPosition) < 0.01f)
            {
                Vector3 temp = targetPosition;
                targetPosition = startPosition;
                startPosition = temp;
            }

            yield return null;
        }
    }
}
