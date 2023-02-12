using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private int _ticksInAir;
    private Rigidbody2D _body;
    private bool _movingRight;
    private bool _movingLeft;
    private bool _jumping;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _ticksInAir = 1000;
        _body = GetComponent<Rigidbody2D>();
        _movingRight = false;
        _movingLeft = false;
        _jumping = false;
        _camera = Camera.main;

        _body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        var newVelocity = _body.velocity;

        if (Input.GetKeyDown("up"))
        {
            _jumping = true;
        }

        if (Input.GetKeyUp("up"))
        {
            _jumping = false;
        }

        if (Input.GetKeyDown("right"))
        {
            _movingRight = true;
        }

        if (Input.GetKeyUp("right"))
        {
            _movingRight = false;
            newVelocity.x = 0;
        }

        if (Input.GetKeyDown("left"))
        {
            _movingLeft = true;
        }

        if (Input.GetKeyUp("left"))
        {
            _movingLeft = false;
            newVelocity.x = 0;
        }

        if (_ticksInAir < 30)
        {
            if (_ticksInAir > 0)
            {
                _ticksInAir += 1;
                newVelocity.y = (30.0f - _ticksInAir) / 5.0f;
            }
            else if (_jumping)
            {
                _ticksInAir += 1;
                newVelocity.y = 6;
            }
        }

        if (_movingRight)
        {
            newVelocity.x = 2;
        }

        if (_movingLeft)
        {
            if (newVelocity.x >= 1.5)
            {
                newVelocity.x = 0;
            }
            else
            {
                newVelocity.x = -2;
            }
        }

        _body.velocity = newVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Bottom World Box"))
        {
            _ticksInAir = 0;
        }
    }
}