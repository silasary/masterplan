using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Masterplan.Events
{
    internal static class ApplicationIdleEventWrapper
    {
        static ApplicationIdleEventWrapper()
        {
            Application.Idle += OnIdle;
        }
        private static void OnIdle(object sender, EventArgs e)
        {
            foreach (EventHandler d in Idle.GetInvocationList())
            {
                if (d.Target is Control)
                {
                    Control control = d.Target as Control;
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
