using System.Collections.Generic;
//メインフォームをいじる
namespace ToDoManager.src
{
	class TaskViewManager
    {

		private List<TaskView> taskViewList;
        private static TaskViewManager instance = null;

        private TaskViewManager()
        {
			// タスクをリスト化
			taskViewList = new List<TaskView>();
        }

		// windowは一つしかないから　シングルトンパターンの採用
		public static TaskViewManager getInstance()
		{

			// インスタンスがなければ生成
			if(null == instance)
			{
				instance = new TaskViewManager();
			}

			return instance;
		}

		// タスクびゅーを追加
		public void addTaskView(TaskView view)
		{
			this.taskViewList.Add(view);
		}

		// タスクビューの削除
		public void clear()
		{
			this.taskViewList.Clear();
		}

		// content(戻る、完了、編集、削除)
		public void hideAllMenuContent()
		{
			// 複数あっても非表示できるように
			foreach(TaskView view in this.taskViewList)
			{
				view.hideMenuContent();
			}
		}

		public void setTaskViewLock(bool value)
		{
			foreach (TaskView view in this.taskViewList)
			{
				view.setLock(value);
			}
		}

		public void indicateShowMenu(TaskView src)
		{
			for(int i = 0; i < this.taskViewList.Count; i++)
			{
				TaskView view = this.taskViewList[i];
				if(view != src)
				{
					view.hideMenuContent();
				}
			}
		}
    }
}
