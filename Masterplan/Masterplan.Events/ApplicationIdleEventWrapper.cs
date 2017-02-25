using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Masterplan.Events
{
    /// <summary>
    /// This class exists to fix the numerous memory leaks caused by excessive use of Application.Idle.
    /// Instead of attaching to Application.Idle directly, Forms and Controls should attach to this wrapper.
    /// The wrapper will then automatically detach any disposed targets, allowing them to be collected by the GC.
    /// </summary>
    /// <remarks>
    /// Ideally, forms should detach themselves either in Dispose, or Form_Close.  
    /// In reality, this is easier.
    /// </remarks>
    internal static class ApplicationIdleEventWrapper
    {
        static ApplicationIdleEventWrapper()
        {
            Application.Idle += OnIdle;
        }

        private static void OnIdle(object sender, EventArgs e)
        {
            // Iterate through the registered event handlers.
            foreach (EventHandler d in Idle.GetInvocationList())
            {
                // If the event's target is a Control or Form.
                if (d.Target is Control)
                {
                    Control control = d.Target as Control;
                    // If it's been disposed, detach it.
                    if (control.IsDisposed)
                    {
                        Idle -= d;
                    }
                }
                d(sender, e);
            } 
        }

        public static EventHandler Idle;
    }
}
