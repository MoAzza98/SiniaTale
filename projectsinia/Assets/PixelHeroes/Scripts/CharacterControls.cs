using UnityEngine;

namespace Assets.PixelHeroes.Scripts
{
    public class CharacterControls : MonoBehaviour
    {
        public Character Character;
        public CharacterController Controller; // https://docs.unity3d.com/ScriptReference/CharacterController.html
        public float RunSpeed = 1f;
        public float JumpSpeed = 3f;
        public float CrawlSpeed = 0.25f;
        public float Gravity = -0.2f;

        private Vector3 _motion = Vector3.zero;
        private int _inputX, _inputY;
        private float _activityTime;
        
        public void Start()
        {
            Character.SetState(AnimationState.Idle);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) Character.Animator.SetTrigger("Attack");
            else if (Input.GetKeyDown(KeyCode.J)) Character.Animator.SetTrigger("Jab");
            else if (Input.GetKeyDown(KeyCode.P)) Character.Animator.SetTrigger("Push");
            else if (Input.GetKeyDown(KeyCode.H)) Character.Animator.SetTrigger("Hit");
            else if (Input.GetKeyDown(KeyCode.I)) { Character.SetState(AnimationState.Idle); _activityTime = 0; }
            else if (Input.GetKeyDown(KeyCode.R)) { Character.SetState(AnimationState.Ready); _activityTime = Time.time; }
            else if (Input.GetKeyDown(KeyCode.B)) Character.SetState(AnimationState.Blocking);
            else if (Input.GetKeyUp(KeyCode.B)) Character.SetState(AnimationState.Ready);
            else if (Input.GetKeyDown(KeyCode.D)) Character.SetState(AnimationState.Dead);

            if (Controller.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GetDown();
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    GetUp();
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _inputX = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _inputX = 1;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _inputY = 1;
            }
        }

        public void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var state = Character.GetState();

            if (state == AnimationState.Dead) return;

            if (_inputX != 0)
            {
                Turn(_inputX);
            }

            if (Controller.isGrounded)
            {
                if (state == AnimationState.Jumping)
                {
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        GetDown();
                    }
                    else
                    {
                        Character.Animator.SetTrigger("Landed");
                        Character.SetState(AnimationState.Ready);
                    }
                }

                _motion = state == AnimationState.Crawling
                    ? new Vector3(CrawlSpeed * _inputX, 0)
                    : new Vector3(RunSpeed * _inputX, JumpSpeed * _inputY);

                if (_inputX != 0 || _inputY != 0)
                {
                    if (_inputY > 0)
                    {
                        Character.SetState(AnimationState.Jumping);
                    }
                    else
                    {
                        switch (state)
                        {
                            case AnimationState.Idle:
                            case AnimationState.Ready:
                                Character.SetState(AnimationState.Running);
                                break;
                        }
                    }
                }
                else
                {
                    switch (state)
                    {
                        case AnimationState.Blocking:
                        case AnimationState.Crawling:
                            break;
                        default:
                            Character.SetState(Time.time - _activityTime > 5 ? AnimationState.Idle : AnimationState.Ready);
                            break;
                    }
                }
            }
            else
            {
                _motion = new Vector3(RunSpeed * _inputX, _motion.y);
                Character.SetState(AnimationState.Jumping);
            }

            _motion.y += Gravity;

            Controller.Move(_motion * Time.fixedDeltaTime);

            Character.Animator.SetBool("Grounded", Controller.isGrounded);
            Character.Animator.SetBool("Moving", Controller.isGrounded && _inputX != 0);
            Character.Animator.SetBool("Falling", !Controller.isGrounded && Controller.velocity.y < 0);

            if (_inputX != 0 && _inputY != 0 || Character.Animator.GetBool("Action"))
            {
                _activityTime = Time.time;
            }

            _inputX = _inputY = 0;
        }

        private void Turn(int direction)
        {
            Character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
        }

        private void GetDown()
        {
            Character.Animator.SetTrigger("GetDown");
            Character.CharacterController.center = new Vector3(0, 0.06f);
            Character.CharacterController.height = 0.08f;
        }

        private void GetUp()
        {
            Character.Animator.SetTrigger("GetUp");
            Character.CharacterController.center = new Vector3(0, 0.08f);
            Character.CharacterController.height = 0.16f;
        }
    }
}