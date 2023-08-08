using UnityEngine;

namespace Player
{
    public class Legs : MonoBehaviour
    {
        private readonly float _timeToCheckGround = 0.2f;
        private float _timeToReset;

        void Start()
        {
            _timeToReset = _timeToCheckGround;
        }

        // Update is called once per frame
        void Update()
        {
            if (_timeToReset <= 0)
                return;
            _timeToReset -= Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.transform.GetComponent<Platfrom>() == null) return;
            GetComponentInParent<PlayerMovement>().HitTheGround();
            print("Hit the ground");
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.collider.transform.GetComponent<Platfrom>() == null) return;
            if (_timeToReset > 0)
            {
                return;
            }

            _timeToReset = _timeToCheckGround;
            GetComponentInParent<PlayerMovement>().HitTheGround();

            print("Hit the ground (OnCollisionStay)");
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.transform.GetComponent<Platfrom>() == null) return;
            GetComponentInParent<PlayerMovement>().LostTheGround();
            print("Lost the ground");
        }
    }
}