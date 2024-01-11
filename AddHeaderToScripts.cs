using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;



public class AddNamespaceToScriptWindow : EditorWindow 
{
    public PlayerData player;
    [MenuItem("Custom Tools/Add Namespace to Specific Folder", false, 0)]

    public static void AddNamespaceFolder()
    {
        //string relativeFilePath = "Fuly UnlockGame/Scripts/BearScripts.cs"; // Relative to "Assets" folder
        //string absoluteFilePath = Path.Combine(Application.dataPath, relativeFilePath);

        string namespaceText = "namespace SlimeGirlsFriend {\n";
        string endNamespaceText = "}\n";

        //AddNamespaceToScript(absoluteFilePath, namespaceText, endNamespaceText);


        string folderPath = "Assets/SlimeGirlfriend/"  /*"Assets/Fuly UnlockGame/TransparentCapture"*/ ; // Change "YourFolderName" to the actual folder name
        string[] scriptFiles = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);
        foreach (string filePath in scriptFiles)
        {
            AddNamespaceToScript(filePath, namespaceText, endNamespaceText);
        }

        Debug.Log("Namespace added to the specified script.");
            }
     //[MenuItem("Custom Tools/Add Namespace to Specific Script", false, 0)]
     //public static void AddNameSpaceToScript()
     //       {
     //           string relativeFilePath = "NameSpace/NewBehaviourScript";          /*"Fuly UnlockGame/Scripts/.cs";*/ // Relative to "Assets" folder
     //           string absoluteFilePath = Path.Combine(Application.dataPath, relativeFilePath);
     //           //AddNamespaceToScript(absoluteFilePath, namespaceText, endNamespaceText);
     //           string namespaceText = "namespace SlimeGirlsFriend {\n";
     //           string endNamespaceText = "}\n";
     //           AddNamespaceToScript(absoluteFilePath, namespaceText, endNamespaceText);
     //       }

    static void AddNamespaceToScript(string filePath, string namespaceText, string endNamespaceText)
    {
        
        // Check if the specified script file exists
        if (File.Exists(filePath))
        {
           
            string originalContent = File.ReadAllText(filePath);

            if (originalContent.Contains("namespace SlimeGirlsFriend"))
            {
                Debug.Log("namespace SlimeGirlsFriend" + "Already Exsists");
                
                // i want to rplace  the namespace with new one can you modify the code in this methood if block
                return;
            }
            // Check if the script already contains the specified namespace
            if (originalContent.Contains("namespace"))
            {
                string text = "namespace";
                if (originalContent.Contains(text))
                {
                    string classDefinition = GetClassDefinition(originalContent, text);
                    string headerFiles = GetHeaderFiles(originalContent,text);
                    string restOfFile = GetRestOfFile(originalContent, classDefinition);

                    string newContent = headerFiles + namespaceText + classDefinition + restOfFile + endNamespaceText;
                    File.WriteAllText(filePath, newContent);
                    Debug.Log("Added before NameSpace");
                    return;
                }
            }
            else
             if (originalContent.Contains("[System.Serializable]"))
            {
                string text = "[System.Serializable]";
                if (originalContent.Contains(text))
                {
                    string classDefinition = GetClassDefinition(originalContent, text);
                    string headerFiles = GetHeaderFiles(originalContent, text);
                    string restOfFile = GetRestOfFile(originalContent, classDefinition);

                    string newContent = headerFiles + namespaceText + classDefinition + restOfFile + endNamespaceText;
                    File.WriteAllText(filePath, newContent);
                    Debug.Log("Added before [System.Serializable]");
                    return;
                }
            }
            else
            // Check if the script contains a class definition
            if (originalContent.Contains("public class"))
            {
                // Add the namespace around the class definition
                string text = "public class";
                string classDefinition = GetClassDefinition(originalContent, text);
                string headerFiles = GetHeaderFiles(originalContent, text);
                string restOfFile = GetRestOfFile(originalContent, classDefinition);

                string newContent = headerFiles + namespaceText + classDefinition + restOfFile + endNamespaceText;
                File.WriteAllText(filePath, newContent);
                Debug.Log("Added before class");

                return;
            }
            else
            {
                Debug.LogError("Specified script does not contain a class definition.");
            }
        }
        else
        {
            Debug.Log("Namespace already exists in the specified script." +  filePath);
        }
       
       
    }

    static string GetClassDefinition(string scriptContent,string text)
    {
        // Extract the class definition
        int classStartIndex = scriptContent.IndexOf(text);
        int classEndIndex = scriptContent.IndexOf("{", classStartIndex);

        if (classStartIndex != -1 && classEndIndex != -1)
        {
            return scriptContent.Substring(classStartIndex, classEndIndex - classStartIndex);
        }

        return string.Empty;
    }

    static string GetHeaderFiles(string scriptContent, string text)
    {
        // Extract the content before the class definition
        int classStartIndex = scriptContent.IndexOf(text);
        return scriptContent.Substring(0, classStartIndex);
    }

    static string GetRestOfFile(string scriptContent, string classDefinition)
    {
        // Extract the content after the class definition
        int classEndIndex = scriptContent.IndexOf(classDefinition) + classDefinition.Length;
        return scriptContent.Substring(classEndIndex);
    }
}

