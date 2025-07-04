using UnityEngine;

namespace Client.Services
{
    public static class MoveInput
    {
        public static Vector3 GetDirection()
        {
            Vector3 direction = Vector3.zero;
            
            direction.x = Input.GetAxis("Horizontal");
            direction.z = Input.GetAxis("Vertical");

            return direction;
        }
    }
}