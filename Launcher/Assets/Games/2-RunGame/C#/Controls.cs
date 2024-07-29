using UnityEngine;

namespace Launcher.RunGame
{
    public class Controls : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Vector3 moveDirection;

        private void Update()
        {
            MoveDirectionSet();
            MoveTransfrom(moveDirection);
        }

        private void MoveDirectionSet()
        {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.z = Input.GetAxis("Vertical");
        }

        private void MoveTransfrom(Vector3 direction)
        {
            transform.position += (transform.right * direction.x + transform.up * 0 + transform.forward * direction.z) 
                * Time.deltaTime * moveSpeed;
        }
    }
}
