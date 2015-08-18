using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BACnet
{
    class TransactionStateMachine
    {
        public enum TSMState { IDLE, AWAIT_CONFIRMATION, AWAIT_RESPONSE };
        TSMState State = TSMState.IDLE;
        int RetryCount;

        // Constructor
        public TransactionStateMachine()
        {
            // Create the timer
            //Timer RequestTimer = new Timer();
            //RequestTimer.Tick += new EventHandler(RequestTimer_Tick);
        }

        // Welcome To The Machine - what to do here ?
    }
}
