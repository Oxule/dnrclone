using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectColorer : EditorWindow
{
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/Object Colorer")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ObjectColorer window = (ObjectColorer)EditorWindow.GetWindow(typeof(ObjectColorer));
        window.Show();
    }

    public Color Color;
    
    void OnGUI()
    {
        Color = EditorGUILayout.ColorField("Color", Color);
        if (GUILayout.Button("Color Selected Objects"))
        {
            foreach (var go in Selection.gameObjects)
            {
                if (go.TryGetComponent<Renderer>(out var renderer))
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = Color;
                    }
                    //make object dirty so it gets saved
                    EditorUtility.SetDirty(go);
                }
            }
        }
    } 
}
