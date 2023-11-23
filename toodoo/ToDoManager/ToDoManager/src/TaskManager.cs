using System;
using System.Collections.Generic;
using System.Xml;

// 同じ名前空間で仕様（名前の衝突を防ぐ）
namespace ToDoManager.src
{
    // クラス
    class TaskManager
    {
        // 変数定義
        private const String confName = "taskList.xml";
        private List<TaskItem> taskList;
        private static TaskManager instance = null;

        // プライベートコンストラクタ
        private TaskManager()
        {
            // 処理の開始
            try
            {
                using (System.IO.TextReader reader = new System.IO.StreamReader(confName))
                {
                    // xmlドキュメントを作成
                    XmlDocument xmlData = new XmlDocument();
                    xmlData.PreserveWhitespace = true;
                    xmlData.LoadXml(reader.ReadToEnd());
                    XmlNodeReader xmlReader = new XmlNodeReader(xmlData.DocumentElement);
                    this.taskList = (List<TaskItem>)new System.Xml.Serialization.XmlSerializer(typeof(List<TaskItem>)).Deserialize(xmlReader);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                // ファイルがない場合空リストを作る
                this.taskList = new List<TaskItem>();
            }
        }

        // formが1つのため一つだけ作成
        public static TaskManager getInstance()
        {
            if (null == instance)
            {
                instance = new TaskManager();
            }

            return instance;
        }

        // タスクを追加する
        public void addTask(TaskItem item)
        {
            // リストに追加していく
            this.taskList.Add(item);
            // ソートで締め切り日
            this.taskList.Sort(delegate (TaskItem a, TaskItem b) { return a.Deadline.CompareTo(b.Deadline); });
        }

        // 指定したIDのタスクを削除
        public void deleteTaskById(long id)
        {
            for (int i = 0; i < this.taskList.Count; i++)
            {
                TaskItem task = this.taskList[i];
                // IDを探索して一致したら削除
                if (task.ID == id)
                {
                    this.taskList.Remove(task);
                    // これ以上探す余地なし
                    break;
                }
            }
        }
    
        // 指定したIDのタスクを完了させる
        public void completeTaskById(long id)
        {
            for (int i = 0; i < this.taskList.Count; i++)
            {
                TaskItem task = this.taskList[i];
                // 一致してたら
                if (task.ID == id)
                {
                    if (task.RepeatType == REPEAT_TYPE.none)
                    {
                        // 削除
                        this.taskList.Remove(task);
                    }
                    else if(task.RepeatType == REPEAT_TYPE.day)
                    {
                        //次の日を追加　締め切り+1
                        DateTime date = task.Deadline.AddDays(1);
                        // 締め切り日が今日になるまで一日足す
                        while (DateTime.Now.Date > date.Date) date = date.AddDays(1);
                        task.Deadline = date;
                        // ソートで締め切り日
                        this.taskList.Sort(delegate (TaskItem a, TaskItem b) { return a.Deadline.CompareTo(b.Deadline); });
                    }
                    else if (task.RepeatType == REPEAT_TYPE.week)
                    {
                        //次の日を追加　締め切り+7
                        DateTime date = task.Deadline.AddDays(7);
                        // 締め切り日が今日になるまで7日足す
                        while (DateTime.Now.Date > date.Date) date = date.AddDays(7);
                        task.Deadline = date;
                        // ソートで締め切り日
                        this.taskList.Sort(delegate (TaskItem a, TaskItem b) { return a.Deadline.CompareTo(b.Deadline); });
                    }

                    break;
                }
            }
        }

        // 指定したIDのタスクを取得する
        public TaskItem getTaskItemByID(long id)
        {
            TaskItem item = null;

            for(int i=0; i<this.taskList.Count; i++)
            {
                if(id == this.taskList[i].ID)
                {
                    item = this.taskList[i];
                    break;
                }
            }

            return item;
        }

        // IDを指定してタスク内容を変更する
        public void editTaskItemByID(long id, String task, DateTime deadline, REPEAT_TYPE type)
        {
            TaskItem taskItem = getTaskItemByID(id);

            // 編集可能なら、、
            if (null != taskItem)
            {
                taskItem.Task = task;
                taskItem.Deadline = deadline;
                taskItem.RepeatType = type;
                // ソートで締め切り日
                this.taskList.Sort(delegate (TaskItem a, TaskItem b) { return a.Deadline.CompareTo(b.Deadline); });
            }
        }

        // タスクのリストを取得
        public List<TaskItem> getTaskList()
        {
            return this.taskList;
        }

        // タスクをファイルに保存
        public void saveTaskList()
        {
            using (System.IO.TextWriter writer = new System.IO.StreamWriter(confName))
            {
                //xmlに書き込む
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<TaskItem>));
                serializer.Serialize(writer, this.taskList);
            }
        }
    }
}
