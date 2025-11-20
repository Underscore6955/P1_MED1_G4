using UnityEngine;
using UnityEngine.UI;

public class GetText : MonoBehaviour
{
    [SerializeField] TextAsset testFile;
    string[] lines;
    void Start()
    {
        lines = testFile.text.Split('\n');
    }
    public (string, int, Image) BuildNextText(int line)
    {
        string lineText = lines[line];
        string textBuild = "";
        string imgNameBuild = null;
        bool img = false;
        for (int i = 1; i < lineText.Length; i++)
        {
            if (lineText[i] == '|') { img = true; }
            if (img) imgNameBuild += lineText[i];
            else textBuild += lineText[i];
        }
        return (textBuild, lineText[0] == '*' ? 1:-1, null);
    }
}