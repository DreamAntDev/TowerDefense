// Copyright 2018-2020 tripolygon Inc. All Rights Reserved.

using UnityEngine;
using UnityEditor;
using tripolygon.UModeler;

namespace TPUModelerEditor
{
    [CanEditMultipleObjects, CustomEditor(typeof(Transform))]
    public class TransformEditor : Editor
    {
        private const float FIELD_WIDTH = 212.0f;
        private const bool WIDE_MODE = true;

        private const float POSITION_MAX = 100000.0f;

        private static GUIContent positionGUIContent = new GUIContent(LocalString("Position")
                                                                     , LocalString("The local position of this Game Object relative to the parent."));
        private static GUIContent rotationGUIContent = new GUIContent(LocalString("Rotation")
                                                                     , LocalString("The local rotation of this Game Object relative to the parent."));
        private static GUIContent scaleGUIContent = new GUIContent(LocalString("Scale")
                                                                     , LocalString("The local scaling of this Game Object relative to the parent."));

        private static string positionWarningText = LocalString("Due to floating-point precision limitations, it is recommended to bring the world coordinates of the GameObject within a smaller range.");

        private SerializedProperty positionProperty;
        private SerializedProperty rotationProperty;
        private SerializedProperty scaleProperty;

        private static string LocalString(string text)
        {
#if true
            return text;
#else
            return LocalizationDatabase.GetLocalizedString(text);
#endif
        }

        public void OnEnable()
        {
            this.positionProperty = this.serializedObject.FindProperty("m_LocalPosition");
            this.rotationProperty = this.serializedObject.FindProperty("m_LocalRotation");
            this.scaleProperty = this.serializedObject.FindProperty("m_LocalScale");

            UpdateInstance((Transform)target, true/*root*/);
        }

        private void UpdateInstance(Transform transform, bool root)
        {
            if (EditorUtil.IsPrefabOnProject(transform.gameObject))
            {
                return;
            }

            UModeler modeler = transform.gameObject.GetComponent<UModeler>();
            if (!root || modeler == null)
            {
                for (int i = 0; i < transform.childCount; ++i)
                {
                    Transform childTM = transform.GetChild(i);
                    UModeler childModeler = childTM.GetComponent<UModeler>();
                    if (childModeler != null)
                    {
                        if (childModeler.CheckMesh())
                        {
                            EditorUtil.RefreshModeler(childTM);
                            childModeler.UpdateMeshContainer();
                        }
                    }
                    UpdateInstance(childTM, /*root*/false);
                }
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUIUtility.wideMode = TransformEditor.WIDE_MODE;
            EditorGUIUtility.labelWidth = 70;// EditorGUIUtility.currentViewWidth - TransformEditor.FIELD_WIDTH; // align field to right of inspector

            this.serializedObject.Update();

            EditorGUILayout.PropertyField(this.positionProperty, positionGUIContent);
            this.RotationPropertyField(this.rotationProperty, rotationGUIContent);
            EditorGUILayout.PropertyField(this.scaleProperty, scaleGUIContent);

            if (!ValidatePosition(((Transform)this.target).position))
            {
                EditorGUILayout.HelpBox(positionWarningText, MessageType.Warning);
            }

            this.serializedObject.ApplyModifiedProperties();
        }

        private bool ValidatePosition(Vector3 position)
        {
            if (Mathf.Abs(position.x) > TransformEditor.POSITION_MAX) return false;
            if (Mathf.Abs(position.y) > TransformEditor.POSITION_MAX) return false;
            if (Mathf.Abs(position.z) > TransformEditor.POSITION_MAX) return false;
            return true;
        }

        private void RotationPropertyField(SerializedProperty rotationProperty, GUIContent content)
        {
            Transform transform = (Transform)this.targets[0];
            Quaternion localRotation = transform.localRotation;
            foreach (UnityEngine.Object t in (UnityEngine.Object[])this.targets)
            {
                if (!SameRotation(localRotation, ((Transform)t).localRotation))
                {
                    EditorGUI.showMixedValue = true;
                    break;
                }
            }

            EditorGUI.BeginChangeCheck();

            Vector3 eulerAngles = EditorGUILayout.Vector3Field(content, localRotation.eulerAngles);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObjects(this.targets, "Rotation Changed");
                foreach (UnityEngine.Object obj in this.targets)
                {
                    Transform t = (Transform)obj;
                    t.localEulerAngles = eulerAngles;
                }
                rotationProperty.serializedObject.SetIsDifferentCacheDirty();
            }

            EditorGUI.showMixedValue = false;
        }

        private bool SameRotation(Quaternion rot1, Quaternion rot2)
        {
            if (rot1.x != rot2.x) return false;
            if (rot1.y != rot2.y) return false;
            if (rot1.z != rot2.z) return false;
            if (rot1.w != rot2.w) return false;
            return true;
        }
    }
}