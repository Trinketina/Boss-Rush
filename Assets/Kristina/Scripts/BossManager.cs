using UnityEngine;

namespace hatsune_miku
{
    public class BossManager : MonoBehaviour
    {
        BossStateMachine machine;
        // Start is called before the first frame update
        void Start()
        {
            machine = new BossStateMachine();
        }

        // Update is called once per frame
        void Update()
        {
            machine.Update();
        }
    }
}
