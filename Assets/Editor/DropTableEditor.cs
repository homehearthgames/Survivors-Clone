#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DropTable))]
public class DropTableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DropTable dropTable = (DropTable)target;

        dropTable.chanceToDropNothing = EditorGUILayout.Slider("Chance to Drop Nothing", dropTable.chanceToDropNothing, 0f, 100f);

        for (int i = 0; i < dropTable.items.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            dropTable.items[i].item = (GameObject)EditorGUILayout.ObjectField(dropTable.items[i].item, typeof(GameObject), false);
            dropTable.items[i].dropChance = EditorGUILayout.Slider(dropTable.items[i].dropChance, 0f, 100f);
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add New Drop Item"))
        {
            dropTable.items.Add(new DropTable.DropItem());
        }

        if (GUI.changed) // If anything was changed
        {
            EditorUtility.SetDirty(dropTable); // Mark this object as "dirty" or changed.
            AssetDatabase.SaveAssets(); // Save the changes
        }
    }
}
#endif
