using System;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/Custom Post FX Settings")]
public class PostFXSettings : ScriptableObject
{
    [SerializeField] Shader shader = default;

    [System.NonSerialized] private Material material;
    public Material Material
    {
        get
        {
            if (material == null && shader != null)
            {
                material = new Material(shader);
                material.hideFlags = HideFlags.HideAndDontSave;
            }
            return material;
        }
    }
    
    [System.Serializable]
    public struct BloomSettings
    {
        [Range(0f, 16f)] public int maxIterations;
        [Min(1f)] public int downscaleLimit;
        public bool bicubicUpsampling;
        [Min(0f)] public float threshold;
        [Range(0f, 1f)] public float thresholdKnee;
        [Min(0f)] public float intensity;
        public bool fadeFireflies;
        public enum Mode
        {
            Additive,
            Scattering
        }
        public Mode mode;
        [Range(0.05f, 0.95f)] public float scatter;
    }

    [SerializeField] private BloomSettings bloom = new BloomSettings
    {
        scatter = 0.7f
    };

    public BloomSettings Bloom => bloom;

    [Serializable]
    public struct ColorAdjustmentsSettings
    {
        public float postExposure;
        [Range(-100f, 100f)] public float contrast;
        [ColorUsage(false, true)] public Color colorFilter;
        [Range(-180f, 180f)] public float hueShift;
        [Range(-100f, 100f)] public float saturation;
    }

    [SerializeField] private ColorAdjustmentsSettings colorAdjustments = new ColorAdjustmentsSettings
    {
        colorFilter = Color.white
    };
    public ColorAdjustmentsSettings ColorAdjustments => colorAdjustments;
    
    [Serializable]
    public struct WhiteBalanceSettings
    {
        [Range(-100f, 100f)] public float temperature, tint;
    }

    [SerializeField] private WhiteBalanceSettings whiteBalance = default;
    public WhiteBalanceSettings WhiteBlance => WhiteBlance;
    
    
    [System.Serializable]
    public struct ToneMappingSettings
    {
        public enum Mode
        {
            None,
            ACES,
            Neutral,
            Reinhard
        }
        public Mode mode;
    }

    [SerializeField] private ToneMappingSettings toneMapping = default;
    public ToneMappingSettings ToneMapping => toneMapping;
    

}