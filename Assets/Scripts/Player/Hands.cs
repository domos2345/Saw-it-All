using UnityEngine;

namespace Player
{
    public class Hands : MonoBehaviour
    {
        

        

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.transform.GetComponent<Wall>() == null) return;

            GetComponentInParent<PlayerMovement>().GotCloseToWallToHoldIt();
            print("Caught the wall");
        }


        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.transform.GetComponent<Wall>() == null) return;

            GetComponentInParent<PlayerMovement>().LostTheWall();
            print("Lost the wall");
        }
    }
}