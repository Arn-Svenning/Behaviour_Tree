using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public abstract class Tree : MonoBehaviour
    {
        private BTNode _root = null;

        public void Start()
        {
            _root = SetupTree();
        }
       
        private void Update()
        {
            // If a tree is attached to the root, evaluate it contiously
            if( _root != null )
            {
                _root.Evaluate();
            }
        }

        protected abstract BTNode SetupTree();
    }
}

