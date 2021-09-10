using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 5.0f;
    [SerializeField] private float _maxSpeed = 20.0f;
    [SerializeField] private LineRenderer _lineRenderer;

    private float _drag = 10f;
    private Vector3 _shotDirection = Vector3.zero;
    private float _shotPower = 5.0f;
    private bool _canShot = false;
    private bool _shot = false;

    private PanZoom _panZoom;

    private void Start()
    {
        _panZoom = Camera.main.GetComponent<PanZoom>();
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void Update()
    {
        if (_shot)
        {
            transform.position += _shotDirection * _shotPower * Time.deltaTime;
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        }

        if (_shotPower > 0f)
        {
            _shotPower = Mathf.MoveTowards(_shotPower, 0f, _drag * Time.deltaTime);

            if (_shotPower <= 0f)
            {
                _shotPower = 0f;
                _shot = false;
                _panZoom.enabled = true;
            }
        }
    }

    private void OnMouseDrag()
    {
        if (!_shot)
        {
            if (Input.touchCount > 0)
            {
                _panZoom.enabled = false;

                Touch touch = Input.GetTouch(0);
                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

                _shotDirection = transform.position - pos;
                _shotDirection.z = 0f;

                if (_shotDirection.magnitude > 0.5f)
                {
                    _shotPower = Mathf.Clamp(_shotDirection.magnitude * _minSpeed, _minSpeed, _maxSpeed);

                    _lineRenderer.positionCount = 2;
                    Vector3 newPoint = transform.position + _shotDirection;
                    _lineRenderer.SetPositions(new Vector3[2] {
                        transform.position,
                        newPoint
                    });

                    _shotDirection.Normalize();

                    _canShot = true;
                }
                else
                {
                    _lineRenderer.positionCount = 0;
                    _canShot = false;
                }
            }
        }
    }

    private void OnMouseUp()
    {
        if (!_shot)
        {
            if (Input.touchCount > 0 && _canShot)
            {
                _shot = true;
                _lineRenderer.positionCount = 0;
            }
            else
            {
                _panZoom.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundry"))
        {
            var boundry = collision.GetComponent<Boundry>();
            _shotDirection *= -1f;

            float angle = Vector3.SignedAngle(_shotDirection, boundry.GetNormal(), Vector3.forward);
            _shotDirection = Quaternion.Euler(0, 0, angle * 2f) * _shotDirection;
            _shotDirection.Normalize();
        }
    }
}
