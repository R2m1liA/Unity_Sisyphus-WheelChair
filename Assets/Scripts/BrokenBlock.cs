using UnityEngine;
using UnityEngine.Tilemaps;

public class BrokenBlock : MonoBehaviour
{
    public GameObject[] Area;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject area in Area)
            {
                area.GetComponent<TilemapRenderer>().enabled = true;
            }
            gameObject.SetActive(false);
        }
    }
}
