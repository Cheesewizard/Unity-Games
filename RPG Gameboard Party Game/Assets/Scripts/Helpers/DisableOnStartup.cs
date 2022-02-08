using Mirror;

namespace Helpers
{
    public class DisableOnStartup : NetworkBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);
        }
    }
}
