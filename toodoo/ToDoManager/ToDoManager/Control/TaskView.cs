﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ToDoManager.src;

namespace ToDoManager
{
    public partial class TaskView : UserControl
    {
        private Label timeLabel;
        private Label taskLabel;
        private long ID;
		private bool isShowMenu;
		private bool isLock;

        public delegate void optionButtonEventHandler(object sender);
        public event optionButtonEventHandler doneButton_Click;
        public event optionButtonEventHandler editButton_Click;
        public event optionButtonEventHandler deleteButton_Click;

        // 読み込み
        public TaskView(TaskItem taskItem)
        {
            InitializeComponent();

			this.isShowMenu = false;
			this.isLock = false;
            // 固定する
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Margin = new Padding(0, 0, 0, 3);

            // 時間
            this.timeLabel = new Label();
            this.timeLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.timeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.timeLabel.Margin = new Padding(0);
            this.timeLabel.Padding = new Padding(0, 0, 0, 0);
            this.timeLabel.Click += Task_Click;

            // タスク
            this.taskLabel = new Label();
            this.taskLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.taskLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.taskLabel.Margin = new Padding(0);
            this.taskLabel.Padding = new Padding(0, 0, 0, 0);
            this.taskLabel.Click += Task_Click;

            // 追加
            this.mainPanel.Controls.Add(this.timeLabel);
            this.mainPanel.Controls.Add(this.taskLabel);

            setFontSize(Properties.Settings.Default.fontSize);

            setTaskItem(taskItem);
        }

        public long getTaskItemID()
        {
            return this.ID;
        }

        // 繰り返し　
        public void setTaskItem(TaskItem taskItem)
        {
            //IDごとに時間をだしている
            this.ID = taskItem.ID;
            int hour = taskItem.Deadline.TimeOfDay.Hours;
            int minute = taskItem.Deadline.TimeOfDay.Minutes;
            // int型のため
            // 文字列に変更 2桁の整数として保持　時間を表示
            this.timeLabel.Text = hour.ToString("D2") + ":" + minute.ToString("D2");

            // 内容
            this.taskLabel.Text = taskItem.Task;
            // 何行かを確認
            int line = taskItem.Task.Count(c => c == '\n') + 1;
            // 上下の余白の設定
            this.Height = (int)(this.taskLabel.Font.GetHeight() * (line + 2));
            
            // 内容に応じて変更　UI
            switch (taskItem.RepeatType)
            {
                case REPEAT_TYPE.none:
                    break;
                case REPEAT_TYPE.day:
                    this.timeLabel.Text += "\n(毎日)";
                    break;
                case REPEAT_TYPE.week:
                    this.timeLabel.Text += "\n(毎週)";
                    break;
                default:
                    break;
            }
        }


        //サイズ変更
        public void setFontSize(int size)
        {
            Font font = new Font("Meiryo UI", size);

            this.timeLabel.Font = font;
            this.taskLabel.Font = font;
            
            this.mainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, font.Height * 4);

            int line = this.taskLabel.Text.Count(c => c == '\n') + 1;

            if (line < 2) line = 2;
            this.Height = (int)(this.taskLabel.Font.GetHeight() * (line + 1));
        }



		public void hideMenuContent()
		{
			if(this.isShowMenu == true)
			{
				this.showTaskContent();
			}		
		}

		public void setLock(bool value)
		{
			this.isLock = value;
		}

        private void Task_Click(object sender, EventArgs e)
        {
			this.showOptionContent();
        }

		private void showOptionContent()
		{
			if (this.isLock)
			{
				return;
			}

			this.mainPanel.Controls.Clear();
			TaskOptionPanel opt = createOptionPanel();
			this.mainPanel.Controls.Add(opt, 0, 0);
			this.mainPanel.SetColumnSpan(opt, this.mainPanel.ColumnCount);
			this.isShowMenu = true;

			TaskViewManager.getInstance().indicateShowMenu(this);
		}

        private void showTaskContent()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.timeLabel);
            this.mainPanel.Controls.Add(this.taskLabel);
			this.isShowMenu = false;
        }

        private void returnButton_ClickEvent()
        {
            this.showTaskContent();
        }

        private void doneButton_ClickEvent()
        {
            this.showTaskContent();
            doneButton_Click(this);
        }

        private void editButton_ClickEvent()
        {
            this.showTaskContent();
            editButton_Click(this);
        }

        private void deleteButton_ClickEvent()
        {
            this.showTaskContent();
            deleteButton_Click(this);
        }

		private TaskOptionPanel createOptionPanel()
		{
			TaskOptionPanel view = new TaskOptionPanel();
			view.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
			view.Margin = new Padding(0);
			view.doneEvent += doneButton_ClickEvent;
			view.editEvent += editButton_ClickEvent;
			view.returnEvent += returnButton_ClickEvent;
			view.deleteEvent += deleteButton_ClickEvent;

			return view;
		}

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
