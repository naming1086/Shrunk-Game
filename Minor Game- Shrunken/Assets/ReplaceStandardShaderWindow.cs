using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.Universal;

public class ReplaceStandardShaderWindow : EditorWindow
{
    [MenuItem("Tools/Replace Standard Shader with Lit Shader")]
    static void Init()
    {
        ReplaceStandardShaderWindow window = (ReplaceStandardShaderWindow)EditorWindow.GetWindow(typeof(ReplaceStandardShaderWindow));
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Replace Standard Shader"))
        {
            ReplaceStandardShader();
        }
    }

    static void ReplaceStandardShader()
    {
        // 获取所有的材质文件
        string[] guids = AssetDatabase.FindAssets("t:Material");

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(assetPath);

            // 检查材质是否使用了 Standard Shader
            if (material.shader.name.Contains("Standard"))
            {
                // 替换材质为 URP 的 Lit Shader
                material.shader = Shader.Find("Universal Render Pipeline/Lit");

                // 标记材质文件为已修改
                EditorUtility.SetDirty(material);
            }
        }

        // 应用所有的修改
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}