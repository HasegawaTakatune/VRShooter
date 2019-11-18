using UnityEngine;

namespace VRStandardAssets.Utils
{
    /// <summary>
    /// レンダリングパフォーマンスを調整するためのスクリプト
    /// </summary>
    public class VRDeviceManager : MonoBehaviour
    {
        [SerializeField] private float m_RenderScale = 1.4f;
        private static VRDeviceManager s_instance;

        public static VRDeviceManager Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = FindObjectOfType<VRDeviceManager>();
                    // 新しいシーンを読み込んでもオブジェクトが自動で破棄されないように登録
                    DontDestroyOnLoad(s_instance.gameObject);
                }
                return s_instance;
            }
        }

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(this);
            }
            else if (this != s_instance)
                Destroy(gameObject);
        }
    }
}

