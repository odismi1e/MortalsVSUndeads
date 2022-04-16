using UnityEditor;
using UnityEngine;

namespace Assets.SimpleLocalization.Editor
{
    /// <summary>
    /// Adds "Sync" button to LocalizationSync script.
    /// </summary>
    [CustomEditor(typeof(DataSync))]
    public class DataSyncEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var component = (DataSync)target;

            if (GUILayout.Button("Sync"))
            {
                component.Sync();
            }
        }
    }
}