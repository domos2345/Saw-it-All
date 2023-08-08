using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private Transform _transform;

        //JUMP
        private bool _jumpingUp;
        private bool _fallingDown;
        private bool _onTheGround;

        //MOVEMENT SIDES
        private bool _movingRight;

        // WALL
        private bool _isCloseToWallToHoldIt;
        private bool _isHoldingWall;
        private float slideSpeed = 3f;


        private Vector2 _jumpStartPos;
        [SerializeField] private float jumpHeight = 8f;
        [SerializeField] private float jumpSpeed = 1f;
        [SerializeField] private float movementSpeed = 1f;
        [SerializeField] private float gravityScale = 1f;


        // Start is called before the first frame update
        void Start()
        {
            _movingRight = true;
            Debug.Log("log msg");
            print("Hello world lol :D");
            _rb = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            //legsCollider = legs.GetComponent<BoxCollider2D>();
            StartFallingFromJump();
        }

        // Update is called once per frame
        void Update()
        {
            //END OF JUMP
            if (_jumpingUp && ShouldStartFalling())
            {
                StartFallingFromJump();
            }

            //JUMP
            if (Input.GetKeyDown(KeyCode.W))
            {
                JumpInputKeyDown();
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                StartFallingFromJump();
            }

            // MOVE LEFT INPUT
            if (Input.GetKey(KeyCode.A))
            {
                LeftMoveInputKeyDown();
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                StopXMovement();
            }

            // MOVE RIGHT INPUT
            if (Input.GetKey(KeyCode.D))
            {
                RightMoveInputKeyDown();
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                StopXMovement();
            }

            //DEBUG
            if (Input.GetKeyDown(KeyCode.F))
            {
                DebugAll();
            }
        }

        //JUMP INPUT KEY DOWN FUNC
        private void JumpInputKeyDown()
        {
            if (_fallingDown)
            {
                return;
            }

            if (_isHoldingWall)
            {
                StartWallJump();
                return;
            }

            if (_onTheGround)
            {
                StartJump();
            }
        }

        //JUMP INPUT KEY UP FUNC


        // MOVE LEFT INPUT KEY DOWN
        private void LeftMoveInputKeyDown()
        {
            if (_movingRight)
            {
                var transformRotation = _transform.rotation;
                transformRotation.y = 180;
                _transform.rotation = transformRotation;
            }

            _movingRight = false;
            var rbVelocity = _rb.velocity;
            rbVelocity.x = -movementSpeed;
            _rb.velocity = rbVelocity;

            if (_isCloseToWallToHoldIt && _fallingDown)
            {
                _isHoldingWall = true;
                SlideOnWall();
            }
        }

        //MOVE RIGHT INPUT DOWN
        private void RightMoveInputKeyDown()
        {
            if (!_movingRight)
            {
                var transformRotation = _transform.rotation;
                transformRotation.y = 0;
                _transform.rotation = transformRotation;
            }

            _movingRight = true;
            var rbVelocity = _rb.velocity;
            rbVelocity.x = movementSpeed;
            _rb.velocity = rbVelocity;

            if (_isCloseToWallToHoldIt && _fallingDown)
            {
                _isHoldingWall = true;
                SlideOnWall();
            }
        }


        // JUMP Start
        private void StartJump()
        {
            print("StartJump");
            _jumpStartPos = _transform.position;
            _jumpingUp = true;
            var rbVelocity = _rb.velocity;
            rbVelocity.y = 1 * jumpSpeed;
            _rb.velocity = rbVelocity;
        }

        private void StartWallJump()
        {
            print("StartWallJump");
            _jumpStartPos = _transform.position;
            _jumpingUp = true;
            var rbVelocity = _rb.velocity;
            rbVelocity.y = 1 * jumpSpeed;
            rbVelocity.x = movementSpeed * (_movingRight ? -1 : 1);
            _rb.velocity = rbVelocity;
        }

        private void DebugAll()
        {
            print("jumpingUP : " + _jumpingUp + "; _fallingDown : " + _fallingDown);
        }

        private void StopXMovement()
        {
            var rbVelocity = _rb.velocity;
            rbVelocity.x = 0;
            _rb.velocity = rbVelocity;
        }

        private void StartFallingFromJump()
        {
            _fallingDown = true;
            _jumpingUp = false;

            //_rb.velocity = Vector2.down * gravityScale;
            var rbVelocity = _rb.velocity;
            rbVelocity.y = -gravityScale;
            _rb.velocity = rbVelocity;
        }


        private bool ShouldStartFalling()
        {
            return _transform.position.y - _jumpStartPos.y >= jumpHeight;
        }


        public void HitTheGround()
        {
            _fallingDown = false;
            _jumpingUp = false;
            _isHoldingWall = false;
            _onTheGround = true;
        }

        public void LostTheGround()
        {
            _onTheGround = false;
            if (_jumpingUp)
            {
                return;
            }

            StartFallingFromJump();
        }

        public void HitTheHeadOnGround()
        {
            StartFallingFromJump();
        }

        public void GotCloseToWallToHoldIt()
        {
            _isCloseToWallToHoldIt = true;
            //_jumpingUp = false;
            _fallingDown = false;

            //SlideOnWall();
        }

        private void SlideOnWall()
        {
            _isHoldingWall = true;
            _fallingDown = false;
            _rb.velocity = Vector2.down * slideSpeed;
        }

        public void LostTheWall()
        {
            _isHoldingWall = false;
            _isCloseToWallToHoldIt = false;
            if (_jumpingUp)
            {
                return;
            }

            StartFallingFromJump();
        }
    }
}