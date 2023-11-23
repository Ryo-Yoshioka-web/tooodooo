using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoManager
{
    public partial class TaskOptionPanel : UserControl
    {
        public delegate void optionButtonEventHandler();
        public event optionButtonEventHandler returnEvent;
        public event optionButtonEventHandler doneEvent;
        public event optionButtonEventHandler editEvent;
        public event optionButtonEventHandler deleteEvent;

        public TaskOptionPanel()
        {
            InitializeComponent();

            this.buttonToolTip.SetToolTip(this.returnButton, "戻る");
            this.buttonToolTip.SetToolTip(this.doneButton,   "完了");
            this.buttonToolTip.SetToolTip(this.editButton,   "編集");
            this.buttonToolTip.SetToolTip(this.deleteButton, "削除");
        }


        private void doneButton_Click(object sender, EventArgs e)
        {
            this.doneEvent();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.editEvent();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            this.returnEvent();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.deleteEvent();
        }
    }
}
