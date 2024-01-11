using UnityEngine;
using UnityEditor;
using System.IO;

public class ReplaceNamespaceWindow : EditorWindow
{
    [MenuItem("Custom Tools/Replace Namespace in Specific Folder", false, 0)]
    public static void ReplaceNamespaceInFolder()
    {
        string oldNamespace = "namespace SlimeGirlPopit {";
        string newNamespace = "namespace SlimeGirlsFriend {";

        string folderPath = "Assets/NameSpace/Abc"; // Change to your actual folder path
        string[] scriptFiles = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);

        foreach (string filePath in scriptFiles)
        {
            ReplaceNamespaceInScript(filePath, oldNamespace, newNamespace);
        }

        Debug.Log("Namespace replaced in all scripts within the specified folder.");
    }

    static void ReplaceNamespaceInScript(string filePath, string oldNamespace, string newNamespace)
    {
        if (File.Exists(filePath))
        {
            string originalContent = File.ReadAllText(filePath);

            if (originalContent.Contains(oldNamespace))
            {
                string newContent = originalContent.Replace(oldNamespace, newNamespace);
                File.WriteAllText(filePath, newContent);
                Debug.Log("Replaced namespace in " + filePath);
            }
            else
            {
                Debug.Log("Namespace not found in " + filePath);
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }
}
