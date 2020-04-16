using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ViewModels.BaseClasses
{
    public class MyTraceListener<T> : TraceListener
    {
        // Control output;
        TextBox output;
        public MyTraceListener(TextBox output)
        {
            this.Name = "Trace";
            this.output = output;
            
        }
        public override void Write(string message)
        {
            Action append = delegate ()
            {
                output.AppendText(message);
            };

            output?.Dispatcher.BeginInvoke(append);
        }
        
        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }
    }
}