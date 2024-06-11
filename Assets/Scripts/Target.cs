using UnityEngine;

public class Target : MonoBehaviour
{
    public bool hasRock;

    private void Start()
    {
        hasRock = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Rock is in target.");
        if (other.CompareTag("Pushable"))
        {
            Debug.Log("Rock is in target.");
            other.GetComponent<Pushable>().isInTarget = true;
            other.GetComponent<Pushable>()._targetPosition = (Vector2)transform.position;
            hasRock = true;
        }
    }

    public void Reset()
    {
        hasRock = false;
    }
    
}
