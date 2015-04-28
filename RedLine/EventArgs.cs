using System;

namespace RedLine
{
    public class EventArgs<TData>: EventArgs
    {
        public readonly TData Data;

        public EventArgs(TData data)
        {
            Data = data;
        }
    }
}
