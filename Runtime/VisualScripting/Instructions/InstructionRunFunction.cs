using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.Functions
{
    [Title("Run Function")]
    [Image(typeof(IconInstructions), ColorTheme.Type.Blue, typeof(BadgeArrowRight))]

    [Description("Executes a Function asset")]
    [Category("Control/Run Function")]

    [Parameter("Source", "The Source parameter passed to the Function")]
    [Parameter("Target", "The Target parameter passed to the Function")]
    [Parameter("Function", "The Function to execute")]
    [Parameter("Await", "Whether to await until it finishes or not")]

    [Note("An awaited Function stops when the caller is cancelled")]

    [Keywords("Call", "Execute")]

    [Serializable]
    public class InstructionRunFunction : Instruction
    {
        [SerializeField]
        private GetScriptableObject m_Function = new GetScriptableObject(new TypeScriptableObjectFunction());

        [SerializeField]
        private GetGameObject m_Source = new GetGameObject(new TypeGameObjectSource());

        [SerializeField]
        private GetGameObject m_Target = new GetGameObject(new TypeGameObjectTarget());

        [SerializeField] private bool m_Await = true;

        protected override string GetTitle(GameObject source)
        {
            string await = this.m_Await ? " and wait" : string.Empty;
            return $"Run {this.m_Function}{await}";
        }

        protected override async Awaitable Run(Args args)
        {
            Function function = this.m_Function.Get<Function>(args);
            if (function == null) return;

            GameObject source = this.m_Source.Get(args);
            GameObject target = this.m_Target.Get(args);

            using Args functionArgs = Args.Get(source, target);

            if (this.m_Await)
            {
                await function.Schedule(functionArgs, this.ParentCancelToken);
            }
            else
            {
                function.Schedule(functionArgs).Fire();
            }
        }
    }
}