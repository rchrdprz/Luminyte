using UnityEngine;

public class QuitApp : MonoBehaviour
{
    public void Quit()
    {
        #if UNITY_STANDALONE
        Application.Quit();
        #endif

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}