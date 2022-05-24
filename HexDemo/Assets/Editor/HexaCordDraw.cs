using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HexaCord))]
public class HexaCordDraw : PropertyDrawer
{
   
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        HexaCord cord = new HexaCord(property.FindPropertyRelative("x").intValue, property.FindPropertyRelative("y").intValue);
        position = EditorGUI.PrefixLabel(position, label);
        GUI.Label(position, cord.ToString());
    }
}
