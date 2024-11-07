using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaver : MonoBehaviour
{
    public Transform objectToMove;
    public Vector2 moveDirection;
    public float moveDistance;
    public float moveSpeed;

    public Sprite leaverOnSprite;
    public Sprite leaverOffSprite;
    private SpriteRenderer spriteRenderer;

    private bool isActivable = false;
    private bool isMoving = false;
    private bool moveForward = true;
    private Vector3 targetPosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = leaverOffSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine && collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            isActivable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine && collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            isActivable = false;
        }
    }

    private void Update()
    {
        if (isActivable && Input.GetKeyDown(KeyCode.F) && !isMoving)
        {
            Vector3 direction = moveForward ? (Vector3)moveDirection : -(Vector3)moveDirection;
            targetPosition = objectToMove.position + direction.normalized * moveDistance;
            StartCoroutine(MoveObject());
            moveForward = !moveForward;
        }
    }

    private IEnumerator MoveObject()
    {
        isMoving = true;
        spriteRenderer.sprite = leaverOnSprite;
        while (Vector3.Distance(objectToMove.position, targetPosition) > 0.01f)
        {
            objectToMove.position = Vector3.MoveTowards(objectToMove.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        objectToMove.position = targetPosition;
        isMoving = false;
        spriteRenderer.sprite = leaverOffSprite;
    }

}
