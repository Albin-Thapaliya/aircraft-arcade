using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string[] planeNames = new string[] { "Plane 1", "Plane 2", "Plane 3" };
    private string selectedPlane = "None";
    private int selectedPlaneIndex = -1;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 500));
        GUILayout.Label("Select Your Plane:");

        for (int i = 0; i < planeNames.Length; i++)
        {
            if (GUILayout.Button(planeNames[i]))
            {
                selectedPlane = planeNames[i];
                selectedPlaneIndex = i;
            }
        }

        GUILayout.Space(20);
        GUILayout.Label("Selected Plane: " + selectedPlane);

        if (GUILayout.Button("Start Game"))
        {
            Debug.Log("Starting Game with " + selectedPlane);
            PlayerPrefs.SetInt("SelectedPlaneIndex", selectedPlaneIndex);
            SceneManager.LoadScene("Flight");
        }

        if (GUILayout.Button("Quit Game"))
        {
            Application.Quit();
        }

        GUILayout.EndArea();
    }
}