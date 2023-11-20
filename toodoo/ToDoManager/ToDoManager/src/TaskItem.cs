using System;

namespace ToDoManager.src
{
    public enum REPEAT_TYPE
    {
        none = 0,
        day,
        week
    }

    public class TaskItem
    {
        private String task;
        private DateTime deadline;
        private REPEAT_TYPE repeatType;
        private long id;

        public TaskItem()
        {
        }

        // 入力内容を指定
        public TaskItem(String task, DateTime deadline, REPEAT_TYPE type)
        {
            this.task = task;
            this.deadline = deadline;
            this.repeatType = type;
            this.id = DateTime.Now.ToFileTimeUtc();
        }
        //コンストラクタ　内容
        public String Task
        {
            get { return this.task; }
            set { this.task = value; }
        }
        //コンストラクタ　〆
        public DateTime Deadline
        {
            get { return this.deadline; }
            set { this.deadline = value; }
        }
        //コンストラクタ　タイプ
        public REPEAT_TYPE RepeatType
        {
            get { return this.repeatType; }
            set { this.repeatType = value; }
        }
        //コンストラクタ　ID
        public long ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}
