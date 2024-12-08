using UnityEngine;

namespace hatsune_miku
{
    public class DisablePlayer : MonoBehaviour
    {
        public void DisableInput()
        {
            StaticInputManager.input.Player.Disable();
        }
        public void EnableInput()
        {
            StaticInputManager.input.Player.Enable();
        }
    }
}