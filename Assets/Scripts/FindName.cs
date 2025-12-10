using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class FindName : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] string sceneName;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && input.text.Length > 1) { ChatScript.yourName = input.text; SceneManager.LoadScene(sceneName); }
    }
}
