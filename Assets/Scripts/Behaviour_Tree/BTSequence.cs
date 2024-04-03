using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTSequence : BTNode
    {
        public BTSequence() : base ()
        {

        }

        public BTSequence(List<BTNode> children) : base(children)
        {

        }

        public override NodeState Evaluate()
        {
            bool bIsChildRunning = false;

            foreach(var node in children)
            {
                // call evaluate on all the child nodes
                switch (node.Evaluate())
                {
                    // If any child node is a failure then sequence failed
                    case NodeState.FAILURE:
                        State = NodeState.FAILURE;
                        return State;                        

                    // If a child node succeeds, then continue evaluating the sequencer (next iteration of the loop)
                    case NodeState.SUCCESS:
                        continue;                      

                    case NodeState.RUNNING:
                        bIsChildRunning = true;
                        continue;                       

                    default:
                        State = NodeState.SUCCESS;
                        return State;
                }
            }
            // Sets the State based on if all child nodes are running or has succeeded
            State = bIsChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return State;
        }
    }
}

