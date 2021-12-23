using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Trainer
{
    public class Trainer
    {
        public string ProcessName;

        private Memory.Mem memory;

        public Trainer(string processName)
        {
            ProcessName = processName;
            InitializeMemory();
        }

        private void InitializeMemory()
        {
            memory = new Memory.Mem();
        }

        public bool ProcessIsOpen()
        {
            return Process.GetProcessesByName(ProcessName).Length > 0;
        }

        public void WriteMemory(string code, string type, string write)
        {
            if (ProcessIsOpen())
            {
                memory.OpenProcess(Process.GetProcessesByName(ProcessName).FirstOrDefault().Id);

                if (memory.WriteMemory(code, type, write)) return;
                else Utilities.ShowErrorMessage("Failed to write memory to the process!");
            }
            else
            {
                Utilities.ShowErrorMessage("The process isn't open!");
            }
        }
    }

    public static class Utilities
    {
        public static void ShowErrorMessage(string content)
        {
            MessageBox.Show(content, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
