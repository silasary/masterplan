using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utils
{
    /// <summary>
    /// Due to the way EventHandlers keep object references around, static events can be very dangerous.
    /// This class is used to act as a middle-man and can detach unwanted subscribers.
    /// </summary>
    class StaticEventDispatcher
    {
        static StaticEventDispatcher()
        {
            Application.Idle += OnIdle;
        }

        private static void OnIdle(object sender, EventArgs e)
        {
            // Iterate through the registered event handlers.
            foreach (EventHandler subscriber in ApplicationIdle.GetInvocationList())
            {
                // If the event's target is a Control (or Form).
                if (subscriber.Target is Control)
                {
                    Control control = subscriber.Target as Control;
                    // If it's been disposed, detach it.
                    if (control.IsDisposed)
                    {
                        ApplicationIdle -= subscriber;
                    }
                }
                subscriber(sender, e);
            }
        }

        public static EventHandler ApplicationIdle;
    }
}
