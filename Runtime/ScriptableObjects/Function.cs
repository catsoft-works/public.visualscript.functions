using System;
using UnityEngine;

namespace VisualScript.Runtime.Functions
{
    [CreateAssetMenu(
        menuName = "Visual Script/Function",
        fileName = "New Function"
    )]

    [Icon("Packages/works.catsoft.visualscript-functions/Editor/Gizmos/GizmoFunction.png")]

    [Serializable]
    public class Function : ScriptableObject, IRun, ISerializationCallbackReceiver
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private RunnerInstructions m_Instructions = new RunnerInstructions();

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private IdString m_FunctionId;

        // EVENTS: --------------------------------------------------------------------------------

        public event Action EventRun;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Run(Args args)
        {
            this.EventRun?.Invoke();
            this.m_Instructions.Run(args).Fire(null, this);
        }

        public Awaitable Schedule(Args args)
        {
            this.EventRun?.Invoke();
            return this.m_Instructions.Run(args, this.m_FunctionId);
        }

        // SERIALIZATION CALLBACKS: ---------------------------------------------------------------

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        { }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.m_FunctionId = new IdString(this.GetEntityId());
        }
    }
}