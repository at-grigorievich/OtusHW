using UnityEngine;

namespace Client.Services
{
    public static class FireInput
    {
        public static bool IsFirePressDown()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}