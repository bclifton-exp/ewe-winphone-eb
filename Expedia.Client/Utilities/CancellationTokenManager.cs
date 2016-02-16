using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Expedia.Client.Utilities
{
    public class CancellationTokenManager
    {
        private static CancellationTokenManager _instance;
        private CancellationTokenSource CurrentTokenSource { get; set; }

        protected CancellationTokenManager()
        {
            CurrentTokenSource = new CancellationTokenSource();
        }

        public static CancellationTokenManager Instance()
        {
            return _instance ?? (_instance = new CancellationTokenManager());
        }

        public CancellationToken CreateAndSetCurrentToken()
        {
            var cts = new CancellationTokenSource();
            CurrentTokenSource = cts;
            return cts.Token;
        }

        public void CancelCurrentToken()
        {
            CurrentTokenSource?.Cancel();
        }
    }
}
