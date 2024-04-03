using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTSelector : BTNode
    {
        public BTSelector() : base()
        {

        }
        public BTSelector(List<BTNode> children) : base(children)
        {

        }

        public override NodeState Evaluate()
        {
            foreach (var node in children)
            {
                // call evaluate on all the child nodes
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;

                    // Return early if child succeeds
                    case NodeState.SUCCESS:
                        State = NodeState.SUCCESS;
                        return State;

                    case NodeState.RUNNING:
                        State = NodeState.RUNNING;
                        return State;

                    default:
                        continue;
                }
            }
            // If no child node returns success, return failure
            State = NodeState.FAILURE;
            return State;
        }
    }
}

