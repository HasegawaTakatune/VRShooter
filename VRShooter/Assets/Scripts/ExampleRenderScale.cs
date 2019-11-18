using UnityEngine;
using UnityEngine.XR;

namespace VRStandardAssets.Examples
{
    public class ExampleRenderScale : MonoBehaviour
    {
        [SerializeField] private float m_RenderScale = 1.5f;

        private void Start()
        {
            XRSettings.eyeTextureResolutionScale = m_RenderScale;
        }
    }
}
