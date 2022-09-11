using UnityEngine;

namespace Utils
{
    public class DisableRotation : MonoBehaviour
    {
        [SerializeField] private Transform parentTransform;
        [SerializeField] private float offsetY = 0.86f;

        private void LateUpdate()
        {
            transform.position = new Vector3(parentTransform.position.x, parentTransform.position.y + offsetY,
                parentTransform.position.z);
        
            transform.rotation = Quaternion.identity;
        }
    }
}
