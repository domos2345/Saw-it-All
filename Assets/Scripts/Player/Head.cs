using UnityEngine;

namespace Player
{
    public class Head : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.transform.GetComponent<Platfrom>() != null)
            {
                GetComponentInParent<PlayerMovement>().HitTheHeadOnGround();
                print("Hit the Head");
            }
        }
    }
}
