namespace ConnectTools.BACnet.Properties
{
    /// <summary>
    /// Transaction State Machine
    /// </summary>
    public class TransactionStateMachine
    {
        public enum TsmState { Idle, AwaitConfirmation, AwaitResponse };
        
        private TsmState _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionStateMachine"/> class.
        /// </summary>
        public TransactionStateMachine()
        {
            _state = TsmState.Idle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionStateMachine"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public TransactionStateMachine(TsmState state)
        {
            this._state = state;
            // Create the timer
            // Timer RequestTimer = new Timer();
            // RequestTimer.Tick += new EventHandler(RequestTimer_Tick);
        }
    }
}