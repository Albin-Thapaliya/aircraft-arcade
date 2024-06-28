using UnityEngine;

public class WeatherManager : MonoBehaviour {
    public WeatherType[] weatherTypes;
    private WeatherType currentWeather;
    private ParticleSystem currentEffectInstance;

    void Start() {
        ChangeWeather(Random.Range(0, weatherTypes.Length));
    }

    public void ChangeWeather(int index) {
        currentWeather = weatherTypes[index];
        Camera.main.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1.0f) * (1.0f - currentWeather.visibility);

        if (currentEffectInstance != null) {
            Destroy(currentEffectInstance.gameObject);
        }

        if (currentWeather.weatherEffectPrefab != null) {
            currentEffectInstance = Instantiate(currentWeather.weatherEffectPrefab, transform);
        }

        foreach (var plane in FindObjectsOfType<Plane>()) {
            plane.SetTurbulence(currentWeather.isTurbulent);
        }
    }
}