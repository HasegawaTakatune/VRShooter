using UnityEngine;

/// <summary>
/// 
/// </summary>
public class WarpZone : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private new Renderer renderer = default;

    /// <summary>
    /// 
    /// </summary>
    private void Reset()
    {
        renderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        renderer.material.EnableKeyword("_EMISSION");
    }

    /// <summary>
    /// 
    /// </summary>
    public void Look()
    {
        renderer.material.SetColor("_EmissionColor", new Color(0.1254f, 0.6431f, 0.6431f, 1));
    }

    /// <summary>
    /// 
    /// </summary>
    public void NotLook()
    {
        renderer.material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f, 1));
    }
}
