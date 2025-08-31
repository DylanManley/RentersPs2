using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CRTEffect : MonoBehaviour
{
    [Header("Assign a Material that uses the CRT shader")]
    public Material crtMaterial;

    [Header("CRT Settings")]
    [Range(0, 1)]
    public float scanlineIntensity = 0.6f;

    [Range(0, 1)]
    public float curvature = 0.2f;

    [Range(0, 1)]
    public float vignetteIntensity = 0.3f;

    [Range(0, 1)]
    public float noiseIntensity = 0.1f;

    [Header("Advanced Effects")]
    [Range(0, 5)]
    public float aberration = 1.0f;

    [Range(0, 1)]
    public float maskStrength = 0.3f;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (crtMaterial != null)
        {
            crtMaterial.SetFloat("_ScanlineIntensity", scanlineIntensity);
            crtMaterial.SetFloat("_Curvature", curvature);
            crtMaterial.SetFloat("_VignetteIntensity", vignetteIntensity);
            crtMaterial.SetFloat("_NoiseIntensity", noiseIntensity);
            crtMaterial.SetFloat("_Aberration", aberration);
            crtMaterial.SetFloat("_MaskStrength", maskStrength);

            Graphics.Blit(src, dest, crtMaterial);
        }
        else
        {
            Graphics.Blit(src, dest); // fallback if material is missing
        }
    }
}
