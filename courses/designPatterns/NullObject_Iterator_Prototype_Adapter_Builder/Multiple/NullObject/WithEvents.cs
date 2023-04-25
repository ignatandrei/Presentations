using System;

namespace NullObject
{
    public class ModifyValueEventArgs : EventArgs
    {
        public int OldValue { get; set; }
        public int NewValue { get; set; }
    }
    class WithEvents
    {
        public event EventHandler<int> ModifyX;
        public event EventHandler<EventArgs> ModifyNoArgs;
        private int _x;
        public int x
        {
            get
            {
                return _x;
            }
            set
            {
                if(ModifyX != null)
                {
                    ModifyX(this, value);

                }
                if(ModifyNoArgs != null)
                {
                    //
                    // Summary:
                    //     Provides a value to use with events that do not have event data.
                    //public static readonly EventArgs Empty;
                    ModifyNoArgs(this, EventArgs.Empty);
                }
                _x = value;
            }
        }

    }
}
