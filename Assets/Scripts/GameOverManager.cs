using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour {

    void Start()
    {
        gameObject.SetActive(false);
    }

	public void LoadCurrentScene()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
    }
}
