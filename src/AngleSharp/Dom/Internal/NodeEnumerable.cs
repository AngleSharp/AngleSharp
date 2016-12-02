namespace AngleSharp.Dom
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    sealed class NodeEnumerable : IEnumerable<INode>
    {
        private readonly INode _startingNode;

        public NodeEnumerable(INode startingNode)
        {
            _startingNode = startingNode;
        }

        public IEnumerator<INode> GetEnumerator() => new NodeEnumerator(_startingNode);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class NodeEnumerator : IEnumerator<INode>
        {
            private readonly Stack<EnumerationFrame> _frameStack;

            public NodeEnumerator(INode startingNode)
            {
                _frameStack = new Stack<EnumerationFrame>();
                TryPushFrame(startingNode, 0);
            }

            public INode Current
            {
                get;
                private set;
            }

            Object IEnumerator.Current => Current;

            public Boolean MoveNext()
            {
                if (_frameStack.Count > 0)
                {
                    var currentFrame = _frameStack.Pop();
                    Current = currentFrame.Parent.ChildNodes[currentFrame.ChildIndex];

                    TryPushFrame(currentFrame.Parent, currentFrame.ChildIndex + 1);
                    TryPushFrame(Current, 0);

                    return true;
                }

                return false;
            }

            private void TryPushFrame(INode parent, Int32 childIndex)
            {
                if (childIndex < parent.ChildNodes.Length)
                {
                    _frameStack.Push(new EnumerationFrame { Parent = parent, ChildIndex = childIndex });
                }
            }

            public void Dispose()
            {
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            private struct EnumerationFrame
            {
                public INode Parent;
                public Int32 ChildIndex;
            }
        }
    }
}
