using UnityEngine;

public class Boundry : MonoBehaviour
{
    [SerializeField] private Vector2 _normal;

    public Vector2 GetNormal()
    {
        return _normal;
    }
}