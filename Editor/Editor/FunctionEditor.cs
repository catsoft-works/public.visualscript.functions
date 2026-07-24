using System;
using UnityEditor;
using UnityEngine.UIElements;
using VisualScript.Runtime.Functions;

namespace VisualScript.Editor.Functions
{
    [CustomEditor(typeof(Function), true)]
    public class FunctionEditor : UnityEditor.Editor
    {
        private static readonly Length DEFAULT_MARGIN_T = new Length(3, LengthUnit.Pixel);
        private static readonly Length DEFAULT_MARGIN_B = new Length(3, LengthUnit.Pixel);
        private static readonly Length DEFAULT_MARGIN_L = new Length(7, LengthUnit.Pixel);
        private static readonly Length DEFAULT_MARGIN_R = new Length(7, LengthUnit.Pixel);

        [NonSerialized] private InstructionsListFieldProperty m_Instructions;

        public override bool UseDefaultMargins()
        {
            return false;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement
            {
                style =
                {
                    marginLeft = DEFAULT_MARGIN_L,
                    marginRight = DEFAULT_MARGIN_R,
                    marginTop = DEFAULT_MARGIN_T,
                    marginBottom = DEFAULT_MARGIN_B,
                }
            };

            SerializedProperty instructions = this.serializedObject
                .FindProperty("m_Instructions")
                .FindPropertyRelative("m_Instructions");

            Box boxInstructions = new Box(null, string.Empty, true);
            boxInstructions.Add(InstructionsDrawer.Create(instructions, false));

            root.Add(Space.CreateSmaller());
            root.Add(boxInstructions);

            return root;
        }
    }
}