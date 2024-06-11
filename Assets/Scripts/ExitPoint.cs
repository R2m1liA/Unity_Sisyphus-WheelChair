using Unity.VisualScripting;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 当玩家进入时，退出游戏
        if (other.CompareTag("Player"))
        {
            Application.Quit();
        }
    }
}
