using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.Functions
{
    [Get]

    [Inline(nameof(m_Function), typeof(Function), false)]

    [Title("Function")]
    [Category("Scriptable Objects/Visual Script/Function")]

    [Description("A constant Function asset")]
    [Image(typeof(IconFunction), ColorTheme.Type.TextLight)]

    [Serializable]
    public class TypeScriptableObjectFunction : TypeScriptableObject
    {
        [SerializeField] private Function m_Function;

        public override ScriptableObject GetPreview(GameObject source) => this.m_Function;

        public TypeScriptableObjectFunction() : this(null)
        { }

        public TypeScriptableObjectFunction(Function value)
        {
            this.m_Function = value;
        }

        public override ScriptableObject Get(Args args)
        {
            return this.m_Function;
        }

        public override string ToString()
        {
            return TextUtils.Name(this.m_Function);
        }

        protected override SubscriptionMode Subscription => SubscriptionMode.OnEditorChange;
    }
}