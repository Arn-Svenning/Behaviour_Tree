using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

namespace BT
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE

    };
    public class BTNode
    {
        public BTNode parent;

        protected List<BTNode> children = new List<BTNode>();

        protected NodeState State;

        private Dictionary<string, object> _nodeData = new Dictionary<string, object>(); 

        public BTNode()
        {
            parent = null;
        }
        public BTNode(List<BTNode> children)
        {
            // Attaches the nodes to the tree
            foreach(var child in children)
            {
                AttachNode(child);
            }
        }

       
        private void AttachNode(BTNode node)
        {
            node.parent = this;
            children.Add(node);
        }

        // The children will override this for correct functionality, if not set up correctly, return failure
        public virtual NodeState Evaluate() => NodeState.FAILURE; 

        public void SetNodeData(string key, object value)
        {
            _nodeData[key] = value;
        }

        // recursively check the nodes in the tree for the value (moving upwards)
        public object GetNodeData(string key)
        {
            object value = null;

            if(_nodeData.TryGetValue(key, out value))
            {
                return value;
            }

            BTNode node = parent;           
            while(node != null)
            {
                value = node.GetNodeData(key);
                if(value != null)
                {
                    return value;
                }
                node = node.parent;
            }
            return null;
        }
        // recursively check the nodes in the tree for the node that should be cleared (moving upwards)
        public bool ClearNodeData(string key)
        {
            if (_nodeData.ContainsKey(key))
            {
                _nodeData.Remove(key);
                return true;
            }

            BTNode node = parent;
            while (node != null)
            {
                bool bIsCleared = node.ClearNodeData(key);
                if (bIsCleared == true)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }

    }
}

