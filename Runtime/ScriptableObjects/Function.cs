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
    public class Function : ScriptableObject, IRun
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private Note m_Note = new Note();

        [SerializeField] private RunnerInstructions m_Instructions = new RunnerInstructions();

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private IdString m_FunctionId;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Note Note => this.m_Note;

        private IdString FunctionId
        {
            get
            {
                if (this.m_FunctionId.IsEmpty)
                {
                    this.m_FunctionId = IdString.Unique;
                }

                return this.m_FunctionId;
            }
        }

        // EVENTS: --------------------------------------------------------------------------------

        public event Action EventRun;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Run(Args args)
        {
            this.EventRun?.Invoke();
            this.m_Instructions.Run(args, this.FunctionId).Fire(null, this);
        }

        public Awaitable Schedule(Args args)
        {
            return this.Schedule(args, null);
        }

        public Awaitable Schedule(Args args, ICancelToken cancelToken)
        {
            this.EventRun?.Invoke();
            return this.m_Instructions.Run(args, this.FunctionId, cancelToken);
        }
    }
}