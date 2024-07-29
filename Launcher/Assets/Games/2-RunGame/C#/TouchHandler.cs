using UnityEngine;

namespace Launcher.RunGame
{
    public class TouchHandler : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                gameManager.OnFinishReached();
            }
        }
    }
}
