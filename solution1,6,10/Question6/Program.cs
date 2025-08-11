namespace Question6
{

    class Node
    {
        public int Data;
        public Node Next;

        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }

    class Queue
    {
        private Node front, rear;

        public Queue()
        {
            front = rear = null;
        }

        public void Insert(int data)
        {
            Node newNode = new Node(data);
            if (rear == null)
            {
                front = rear = newNode;
                return;
            }
            rear.Next = newNode;
            rear = newNode;
        }

        public void Delete()
        {
            if (front == null)
            {
                Console.WriteLine("Queue is empty");
                return;
            }
            front = front.Next;
            if (front == null) rear = null;
        }

        public void Display()
        {
            Node temp = front;
            while (temp != null)
            {
                Console.Write(temp.Data + " ");
                temp = temp.Next;
            }
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Queue q = new Queue();
            q.Insert(10);
            q.Insert(20);
            q.Insert(30);
            Console.WriteLine("Queue:");
            q.Display();
            q.Delete();
            Console.WriteLine("After Deletion:");
            q.Display();
        }
    }
}
