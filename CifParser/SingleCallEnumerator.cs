using System;
using System.Collections;

namespace CifParser
{
    internal class SingleCallEnumerator : IEnumerable
    {
        private bool _runOnce;

        private IEnumerable _enumerable;

        internal SingleCallEnumerator(IEnumerable enumerable)
        {
            _enumerable = enumerable;
        }

        public IEnumerator GetEnumerator()
        {
            if (_runOnce)
                throw new InvalidOperationException("Can only iterate once.");

            _runOnce = true;
            return _enumerable.GetEnumerator();
        }
    }
}