using UnityEngine;

public class BallController : MonoBehaviour
{
    //[SerializeField] private float _minSpeed = 2.0f;
    [SerializeField] private float _maxSpeed = 8.0f;

    private float _drag = 10f;
    private Vector3 _shotDirection = Vector3.zero;
    private float _currentSpeed = 0f;
    private bool _shot = false;

    // Update is called once per frame
    void Update()
    {
        if (_shot)
        {
            transform.position += _shotDirection * _currentSpeed * Time.deltaTime;
        }

        if (_currentSpeed > 0f)
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0f, _drag * Time.deltaTime);

            if (_currentSpeed <= 0f)
            {
                _currentSpeed = 0f;
                _shot = false;
            }
        }
    }

    void OnMouseUp()
    {
        if (!_shot)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

                _shotDirection = transform.position - pos;
                _shotDirection.z = 0f;
                _shotDirection.Normalize();

                _currentSpeed = _maxSpeed;
                _shot = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
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
