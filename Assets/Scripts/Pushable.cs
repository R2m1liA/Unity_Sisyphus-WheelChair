using UnityEngine;

public class Pushable : MonoBehaviour
{
    public LayerMask detectLayer;

    public Vector2 originalPosition;
    public Vector2 _targetPosition;
    public Vector2 _moveDir;
    public bool isInTarget;

    public AudioClip PushSoundFXClip;

    private void Awake()
    {
        originalPosition = (Vector2)transform.position;
        _targetPosition = (Vector2)transform.position;
        isInTarget = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if(_moveDir != Vector2.zero)
        {
            while(CanMove(_moveDir))
            {
                int moveDistance = 0;
                _targetPosition += _moveDir;

                if (moveDistance > 100)
                {
                    _targetPosition = (Vector2)transform.position;
                    Debug.LogError("Move distance is too long.");
                    break;
                }
            }
            if(_targetPosition != (Vector2)transform.position)
            {
                SoundManager.Instance.PlaySFX(PushSoundFXClip);
            }
        }
        _moveDir = Vector2.zero;
        if ((_targetPosition - (Vector2)transform.position).sqrMagnitude > 0.001f)
        {
            Vector2 moveDir = (_targetPosition - (Vector2)transform.position).normalized;
            transform.Translate(moveDir * 5f * Time.fixedDeltaTime);
        }
    }
    
    public void Push(Vector2 dir)
    {
        Debug.Log("Push");
        _moveDir = dir;
    }
    private bool CanMove(Vector2 dir)
    {
        if(isInTarget)
        {
            return false;
        }
        RaycastHit2D hit = Physics2D.Raycast(_targetPosition + dir * 0.5f, dir, 1f, detectLayer);
        if(!hit)
        {
            return true;
        }
        else
        {
            if(hit.collider.CompareTag("Pushable"))
            {
                hit.collider.GetComponent<Pushable>().Push(dir);
            }
        }
        return false;
    }

    public void Reset()
    {
        transform.position = originalPosition;
        _targetPosition = originalPosition;
        _moveDir = Vector2.zero;
        isInTarget = false;
    }
}
