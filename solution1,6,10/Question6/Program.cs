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
            int choice;
            do
            {
                Console.WriteLine("\n--- Queue Menu ---");
                Console.WriteLine("1. Insert");
                Console.WriteLine("2. Delete");
                Console.WriteLine("3. Display");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter value to insert: ");
                        int val = int.Parse(Console.ReadLine());
                        q.Insert(val);
                        break;
                    case 2:
                        q.Delete();
                        break;
                    case 3:
                        q.Display();
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            } while (choice != 4);
        }
    }
}
