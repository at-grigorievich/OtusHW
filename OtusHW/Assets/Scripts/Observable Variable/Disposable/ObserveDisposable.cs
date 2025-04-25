using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATG.Observable
{
    public class ObserveDisposable: IDisposable
    {
        private Action _disposable;

        public ObserveDisposable(Action dis)
        {
            _disposable = dis;
        }

        public void Dispose()
        {
            if (_disposable == null) return;
            
            _disposable.Invoke();
            _disposable = null;
        }
    }
}
