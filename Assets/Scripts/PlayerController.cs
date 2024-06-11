using System;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Vector2 targetPosition;
    
    public LayerMask detectLayer;

    private Rigidbody2D _rigidbody;
    public Vector2 _moveDir;
    
    public AudioClip MoveSoundFXClip;
    private void Awake()
    {
        if (InputManager.Instance == null)
        {
            Debug.LogError("InputManager is not found in the scene. Please add it to the scene.");
        }
    }

    private void Start()
    {
        targetPosition = (Vector2)transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        PushCheck();
    }

    private void Move()
    {
        Vector2 moveInput = InputManager.Instance.MoveValue;
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } 
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (targetPosition == (Vector2)transform.position && moveInput != Vector2.zero)
        {
            int moveDistance = 0;
            while (CanMove(moveInput))
            {
                moveDistance++;
                targetPosition += moveInput;
                
                if (moveDistance > 100)
                {
                    targetPosition = transform.position;
                    Debug.LogError("Move distance is too long.");
                    break;
                }
            }
            if(targetPosition != (Vector2)transform.position)
            {
                SoundManager.Instance.PlaySFX(MoveSoundFXClip);
            }
        }
        _moveDir = (targetPosition - (Vector2)transform.position).normalized;
        _rigidbody.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, 15f * Time.fixedDeltaTime));
    }

    bool CanMove(Vector2 Dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(targetPosition, Dir, 1f, detectLayer);

        return !hit;
    }

    bool PushCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)_moveDir * 0.5f, _moveDir * 0.5f, 1f, detectLayer);
        if(hit)
        {
            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.CompareTag("Pushable"))
            {
                hit.collider.gameObject.GetComponent<Pushable>().Push(_moveDir);
            }

        }

        return !hit;
    }
}
