using UnityEngine;

public class PanZoom : MonoBehaviour
{
    [SerializeField] private Vector2 _minBoundry;
    [SerializeField] private Vector2 _maxBoundry;

    [SerializeField] private float _minZoom = 5f;
    [SerializeField] private float _maxZoom = 10f;
    [SerializeField] private float _zoomSpeed = 0.01f;

    private Vector3 touchStart;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * _zoomSpeed);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = transform.position + direction;

            newPosition.x = Mathf.Clamp(newPosition.x, _minBoundry.x, _maxBoundry.x);
            newPosition.y = Mathf.Clamp(newPosition.y, _minBoundry.y, _maxBoundry.y);

            transform.position = newPosition;
        }
    }

    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, _minZoom, _maxZoom);
    }
}
