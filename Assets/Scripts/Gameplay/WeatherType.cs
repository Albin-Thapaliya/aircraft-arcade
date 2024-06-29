using UnityEngine;
[System.Serializable]
public class WeatherType {
    [SerializeField] private string name;
    [SerializeField] private float visibility;
    [SerializeField] private bool isTurbulent;
    [SerializeField] private ParticleSystem weatherEffect;

    public string Name {
        get => name;
        set => name = value;
    }

    public float Visibility {
        get => visibility;
        set => visibility = Mathf.Clamp(value, 0f, 100f);
    }

    public bool IsTurbulent {
        get => isTurbulent;
        set => isTurbulent = value;
    }

    public ParticleSystem WeatherEffect {
        get => weatherEffect;
        set => weatherEffect = value;
    }
    public void ActivateWeatherEffect() {
        if (weatherEffect != null) {
            weatherEffect.Play();
        } else {
            Debug.LogWarning("No weather effect is assigned to " + name);
        }
    }
    public void DeactivateWeatherEffect() {
        if (weatherEffect != null) {
            weatherEffect.Stop();
        } else {
            Debug.LogWarning("No weather effect is assigned to " + name);
        }
    }
}