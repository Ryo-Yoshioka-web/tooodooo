using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ToDoManager.src;
using ToDoManager.Control;

namespace ToDoManager
{
    public partial class MainForm : Form
    {
        TaskEditView taskEditView;
        SettingView settingView;

        public MainForm()
        {
            InitializeComponent();

            // fzやリストの量を持ってくる
            this.settingView = new SettingView();
            // ボタンを押したときの動作をEventとして、実行する
            this.settingView.okEvent += setting_okButton_Click;
            this.settingView.cancelEvent += setting_cancelButton_Click;

            // 編集画面を生成
            this.taskEditView = new TaskEditView();
            // ボタンを押したときの動作をEventとして、実行する
            this.taskEditView.okEvent += okButton_Click;
            this.taskEditView.cancelEvent += cancelButton_Click;

            // サイズを設定
            System.Drawing.Size formSize = this.DisplayRectangle.Size;

            // alt属性を付与
            this.buttonToolTip.SetToolTip(this.addButton,     "新規タスク登録");
            this.buttonToolTip.SetToolTip(this.settingButton, "アプリケーション設定");

            // 位置
            this.settingButton.Location = new System.Drawing.Point(formSize.Width - this.settingButton.Width - 10, this.settingButton.Location.Y);

            // レイアウトを調整
            this.mainPanel.SetRowSpan(this.taskListPanel, 2);
            this.mainPanel.Size = new System.Drawing.Size(formSize.Width - 4, formSize.Height - this.mainPanel.Location.Y - 2);

            //表示する
            refreshTaskTable();
        }

        // 保存する
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        // タスクビューの生成
          private void addTaskView(TaskItem task)
        {
            TaskView view = new TaskView(task);
            // ボタンを押したときの動作をEventとして、実行する
            view.doneButton_Click += done;
            view.editButton_Click += edit;
            view.deleteButton_Click += delete;
            // 表示する
            this.taskListPanel.Controls.Add(view);
            // 
			TaskViewManager.getInstance().addTaskView(view);
        }


        // 期日の入力    
        private void addDateView(DateTime date)
        {
            DeadlineLabel view = new DeadlineLabel(date);
            this.taskListPanel.Controls.Add(view);
        }



        private void showTaskEditView(EDIT_MODE mode)
        {
            // 編集フォームを入れれるようにスペースをあける
            this.mainPanel.SetRowSpan(this.taskListPanel, 1);
            this.mainPanel.SetRow(this.taskListPanel, 1);

            // 編集画面を表示
            this.mainPanel.Controls.Add(this.taskEditView);
            this.mainPanel.SetRow(this.taskEditView, 0);

            // ボタンを押せなくする
            this.settingButton.Enabled = false;
            // 画像を変更
            this.settingButton.BackgroundImage = Properties.Resources.setting_disable;

            //新規タスクを押せなくする
            this.addButton.Enabled = false;
           //  画像を変更
            this.addButton.BackgroundImage = Properties.Resources.plus_disable;

            // 実行
            this.taskEditView.setEditMode(mode);

            // 設定画面を開いてる時は、おせなくする
            TaskViewManager.getInstance().setTaskViewLock(true);
        }


        //タスク編集ビューを非表示
        private void hideTaskEditView()
        {
            // 削除
            this.mainPanel.Controls.Remove(this.taskEditView);
            // レイアウトを再調整
            this.mainPanel.SetRow(this.taskListPanel, 0);
            this.mainPanel.SetRowSpan(this.taskListPanel, 2);

            // 押せるようにする
            this.settingButton.Enabled = true;
            this.settingButton.BackgroundImage = Properties.Resources.setting_enable;

            this.addButton.Enabled = true;
            this.addButton.BackgroundImage = Properties.Resources.plus_enable;

            // 解除
			TaskViewManager.getInstance().setTaskViewLock(false);
		}


        // タスクを追加するボタン
        private void addButton_Click(object sender, EventArgs e)
        {
            // タスクを追加する NEWのため新規作成になる
            showTaskEditView(EDIT_MODE.New);

			TaskViewManager.getInstance().hideAllMenuContent();
		}


        
        private void okButton_Click(object sender, EventArgs e)
        {
            // タスク管理
            TaskManager manager = TaskManager.getInstance();
            // IDを付与
            long id = this.taskEditView.ID;

            // 0以上なら編集画面
            if(id < 0) 
            {
                 // IDの付与がされる
                TaskItem task = new TaskItem(this.taskEditView.Task, this.taskEditView.Deadline, this.taskEditView.RepeatType);
                // タスクの追加
                manager.addTask(task);
            }
            else  
            {
                // IDで指定したタスクを編集
                manager.editTaskItemByID(id, this.taskEditView.Task, this.taskEditView.Deadline, this.taskEditView.RepeatType);
            }
            // 保存する
            manager.saveTaskList();

            // タスク編集を閉じる
            hideTaskEditView();

            // タスクを更新する
			refreshTaskTable();
        }

        // タスクを更新して表示
        private void refreshTaskTable()
        {
            // タスクを取得して、リストに格納
            TaskManager manager = TaskManager.getInstance();
            List<TaskItem> taskList = manager.getTaskList();

            // タスクを見せれるようにする
            this.taskListPanel.Controls.Clear();
			TaskViewManager.getInstance().clear();
            
            // 現在の時間と日付
            DateTime date = new DateTime();
            // 設定タスク量
            int max = Properties.Settings.Default.taskNum;

            // タスクの数もしくは、設定された量まで
            for(int i = 0; i < taskList.Count && i < max; i++)
            {
                TaskItem task = taskList[i];

                // 締め切り日が今日じゃないもの
                if (task.Deadline.Date != date)
                {
                    // 締め切り日を表示して
                    addDateView(task.Deadline.Date);
                    date = task.Deadline.Date;
                }
                // タスクを追加
                addTaskView(task);
            }

            this.taskListPanel.AutoScroll = false;
            this.taskListPanel.AutoScroll = true;
        }

        //　戻るボタン　
        private void cancelButton_Click(object sender, EventArgs e)
        {
            //タスク編集ビューを非表示
            hideTaskEditView();
		}

        // 追加
        private void done(object sender)
        {
            TaskView taskView = (TaskView)sender;
            TaskManager manager = TaskManager.getInstance();

            manager.completeTaskById(taskView.getTaskItemID());
            manager.saveTaskList();

            refreshTaskTable();
        }

        //　編集
        private void edit(object sender)
        {
            TaskItem taskItem = TaskManager.getInstance().getTaskItemByID(((TaskView)sender).getTaskItemID());

            if(null != taskItem)
            {
                this.taskEditView.reflectTaskItem(taskItem);
            }
            // 編集モードに切り替え
            showTaskEditView(EDIT_MODE.Edit);
        }

        // 削除
        private void delete(object sender)
        {
            TaskView taskView = (TaskView)sender;
            TaskManager manager = TaskManager.getInstance();

            manager.deleteTaskById(taskView.getTaskItemID());

            manager.saveTaskList();
            refreshTaskTable();
        }

        // 日付などが変わった時用に、残日数を更新
        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < this.taskListPanel.Controls.Count; i++)
            {
                System.Windows.Forms.Control control = this.taskListPanel.GetControlFromPosition(0, i);

                if(control is DeadlineLabel)
                {
                    ((DeadlineLabel)control).refreshRemainDays();
                }
            }
        }
        // 歯車クリック
        private void settingButton_Click(object sender, EventArgs e)
        {
            showSettingView();
        }

        private void showSettingView()
        {
            // 設定画面を入れれるようにスペースをあける
            this.mainPanel.SetRowSpan(this.taskListPanel, 1);
            this.mainPanel.SetRow(this.taskListPanel, 1);

            //設定画面の追加
            this.mainPanel.Controls.Add(this.settingView);
            this.mainPanel.SetRow(this.settingView, 0);

            // ボタンを押せなくし、画像の変更
            this.settingButton.Enabled = false;
            this.settingButton.BackgroundImage = Properties.Resources.setting_disable;
            this.addButton.Enabled = false;
            this.addButton.BackgroundImage = Properties.Resources.plus_disable;

            // 他のボタンを押したときに非表示に
			TaskViewManager.getInstance().hideAllMenuContent();
            // 設定画面を開いてる時は、おせなくする
			TaskViewManager.getInstance().setTaskViewLock(true);
		}

        private void hideSettingView()
        {
            // 設定画面をなくす
            this.mainPanel.Controls.Remove(this.settingView);
            // レイアウト再調整
            this.mainPanel.SetRow(this.taskListPanel, 0);
            this.mainPanel.SetRowSpan(this.taskListPanel, 2);

            // ボタンを押せるようにする
            this.settingButton.Enabled = true;
            this.settingButton.BackgroundImage = Properties.Resources.setting_enable;

            this.addButton.Enabled = true;
            this.addButton.BackgroundImage = Properties.Resources.plus_enable;

            // 解除
			TaskViewManager.getInstance().setTaskViewLock(false);
		}

        //　設定画面のOKを押したとき
        private void setting_okButton_Click(object sender, EventArgs e)
        {
            SettingEventArgs args = (SettingEventArgs)e;

            // 設定画面を隠す
            hideSettingView();

            // 以下追加してもらう
            if(args.changeTaskNum)
            {
                //タスクを更新
                this.refreshTaskTable();
            }
           // fzの設定
            this.settingView.setFontSize(Properties.Settings.Default.fontSize);
            this.taskEditView.setFontSize(Properties.Settings.Default.fontSize);

            // 設定の設定(ろめち)
            for (int i = 0; i < this.taskListPanel.Controls.Count; i++)
            {
                System.Windows.Forms.Control control = this.taskListPanel.GetControlFromPosition(0, i);

                if (control is DeadlineLabel)
                {
                    ((DeadlineLabel)control).setFontSize(Properties.Settings.Default.fontSize);
                }
                else if(control is TaskView)
                {
                    ((TaskView)control).setFontSize(Properties.Settings.Default.fontSize);
                }
            }

			this.taskListPanel.AutoScroll = false;
            this.taskListPanel.AutoScroll = true;
        }

        private void setting_cancelButton_Click(object sender, EventArgs e)
        {
            hideSettingView();
		}

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
