using System.Collections.Generic;
using System.Linq;
using Script;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Editor
{
    [CustomEditor(typeof(TileSpawner))]
    public class TileSpawnerEditor : UnityEditor.Editor
    {
        private TileSpawner spawner;
        
        private void OnEnable()
        {
            spawner = (TileSpawner) target;
            SceneView.duringSceneGui += OnSceneGUI; // Subscribe to SceneView GUI events
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI; // Unsubscribe from SceneView GUI events
        }

        // private static pattern FindResources(string name)
        // {
        //     string[] pat = AssetDatabase.FindAssets(name);
        //     foreach (var str in pat)
        //     {
        //         string path = AssetDatabase.GUIDToAssetPath(str);
        //         pattern pattern = AssetDatabase.LoadAssetAtPath<pattern>(path);
        //         return pattern;
        //     }
        //
        //     return null;
        // }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            
            // Button to save all points
            if (GUILayout.Button("Save Pattern"))
            {
                
                foreach (var tile in spawner.instantiatedTiles.ToList())
                {
                    spawner.pattern.Pattern.Add(new CustomPattern(tile.transform.position, tile.transform.rotation));


                    DestroyImmediate(tile);
                }


                spawner.instantiatedTiles.Clear();
                EditorUtility.SetDirty(spawner);
            }

            // Button to reset all points
            if (GUILayout.Button("Reset Points"))
            {
                foreach (var tile in spawner.instantiatedTiles)
                {
                    DestroyImmediate(tile);
                }

                spawner.instantiatedTiles.Clear();
                EditorUtility.SetDirty(spawner);
            }
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            Event e = Event.current;


            // Handle prefab instantiation on right-click
            if (e.type == EventType.MouseDown && e.button == 1)
            {
                // Raycast to get the world position where the mouse was clicked
                Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                Vector2 worldPos = ray.origin;

                // Instantiate the prefab at the clicked position if a prefab is assigned
                if (spawner.prefabToInstantiate != null)
                {
                    GameObject newObject = Instantiate(spawner.prefabToInstantiate, worldPos, Quaternion.identity);

                    Undo.RegisterCreatedObjectUndo(newObject, "Create Object");

                    // Register the new object with the spawner
                    spawner.RegisterInstantiatedObject(newObject);

                    // Use the event so it doesn’t propagate further
                    e.Use();
                }
            }
        }
    }
}