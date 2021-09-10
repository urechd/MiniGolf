using UnityEngine;

public class Goal : MonoBehaviour
{
    public delegate void OnScoreDelegate();
    public static event OnScoreDelegate scoreDelegate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scoreDelegate();
        }
    }
}
