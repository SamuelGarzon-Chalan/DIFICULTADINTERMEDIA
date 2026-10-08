using System;

public class Node
{
    public int value;

    public Node next;
    public Node(int value)
    {
        this.value = value;
    }
}


public class Program
{


    public static void Main()
    {
        LinkedList list1 = new LinkedList(1);
        list1.Append(2);
        list1.Append(3);
        list1.Append(4);
        list1.Append(5);
        list1.Append(6);
        list1.Append(7);
        list1.Append(8);
        list1.Append(9);

        Console.WriteLine("Problema 1 Before:");
        list1.PrintList();

        list1.ReverseAlternateK(3);

        Console.WriteLine("Problema 1 After:");
        list1.PrintList();

        DoublyLinkedList list2 = new DoublyLinkedList(1);
        list2.Append(2);
        list2.Append(3);
        list2.Append(4);
        list2.Append(5);

        Console.WriteLine("Problema 2 Before:");
        list2.PrintList();

        list2.RotateRight(2);

        Console.WriteLine("Problema 2 After:");
        list2.PrintList();


        Console.WriteLine("Problema 3:");

        int[] prices = { 8, 4, 6, 2, 3 };
        int[] result = DaysUntilDrop(prices);

        foreach (int day in result)
        {
            Console.Write(day + " ");
        }
        Console.WriteLine();

        Console.WriteLine("Problema 4:");

        int[] priorities = { 1, 1, 9,1,1,9 };
        int location = 2;

        int answer = PrinterQueueExercise4(priorities, location);

        Console.WriteLine(answer);

    }



    public class LinkedList
    {
        private Node head;
        private Node tail;
        private int length;

        public LinkedList(int value)
        {
            Node newNode = new Node(value);
            head = newNode;
            tail = newNode;
            length = 1;
        }
        public Node GetHead()
        {
            return head;
        }
        public Node GetTail()
        {
            return tail;
        }
        public int GetLength()
        {
            return length;
        }
        public void PrintList()
        {
            Node temp = head;
            while (temp != null)
            {
                Console.WriteLine(temp.value);
                temp = temp.next;
            }
        }


        public void Append(int value)
        {
            Node newNode = new Node(value);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.next = newNode;
                tail = newNode;
            }

            length++;
        }


        public Node ReverseAlternateK(int k)
        {
            if (head == null || k <= 1)
                return head;

            Node current = head;
            Node previousTail = null;
            bool reverse = true;

            while (current != null)
            {
                Node groupStart = current;
                Node prev = null;
                int count = 0;

                if (reverse)
                {
                    while (current != null && count < k)
                    {
                        Node next = current.next;
                        current.next = prev;
                        prev = current;
                        current = next;
                        count++;
                    }

                    if (previousTail == null)
                        head = prev;
                    else
                        previousTail.next = prev;

                    groupStart.next = current;
                    previousTail = groupStart;
                }
                else
                {
                    while (current != null && count < k)
                    {
                        previousTail = current;
                        current = current.next;
                        count++;
                    }
                }

                reverse = !reverse;
            }
            tail = previousTail;
            return head;
        }
    }
    public class Node2
    {
        public int value;
        public Node2 next;
        public Node2 prev;

        public Node2(int value)
        {
            this.value = value;
        }
    }
    public class DoublyLinkedList
    {
        private Node2 head;
        private Node2 tail;
        private int length;
        public DoublyLinkedList(int value)
        {
            Node2 newNode = new Node2(value);
            head = newNode;
            tail = newNode;
            length = 1;
        }
        public void PrintList()
        {
            Node2 temp = head;
            while (temp != null)
            {
                Console.Write(temp.value);
                if (temp.next != null)
                    Console.Write(" <-> ");
                temp = temp.next;
            }
            Console.WriteLine();
        }
        public void GetHead()
        {
            Console.WriteLine(head == null ? "Head: null" : "Head: " + head.value);
        }
        public void GetTail()
        {
            Console.WriteLine(tail == null ? "Tail: null" : "Tail: " + tail.value);
        }
        public void GetLength()
        {
            Console.WriteLine("Length: " + length);
        }

        public void Append(int value)
        {
            Node2 newNode = new Node2(value);
            if (length == 0)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.next = newNode;
                newNode.prev = tail;
                tail = newNode;
            }
            length++;
        }
        public Node2 RotateRight(int k)
        {
            if (head == null || k == 0)
                return head;

            Node2 current = head;
            int count = 1;

            while (current.next != null)
            {
                current = current.next;
                count++;
            }

            k = k % count;

            if (k == 0)
                return head;

            Node2 oldTail = current;

            for (int i = 0; i < k; i++)
                current = current.prev;

            Node2 newHead = current.next;
            current.next = null;
            newHead.prev = null;

            oldTail.next = head;
            head.prev = oldTail;
            head = newHead;
            tail = current;

            return head;
        }
    }

    public static int[] DaysUntilDrop(int[] prices)
    {
        int[] result = new int[prices.Length];
        Stack<int> stacko = new Stack<int>();

        for (int i = 0; i < prices.Length; i++)
        {
            result[i] = -1;

            while (stacko.Count > 0 && prices[i] < prices[stacko.Peek()])
            {
                int day = stacko.Pop();
                result[day] = i - day;
            }

            stacko.Push(i);
        }

        return result;
    }

    public static int PrinterQueueExercise4(int[] priorities, int location)
    {
        Queue<int> queue = new Queue<int>();
        int[] count = new int[10];

        for (int i = 0; i < priorities.Length; i++)
        {
            queue.Enqueue(i);
            count[priorities[i]]++;
        }

        int printed = 0;

        while (queue.Count > 0)
        {
            int job = queue.Dequeue();
            int priority = priorities[job];
            bool higher = false;

            for (int i = priority + 1; i <= 9; i++)
            {
                if (count[i] > 0)
                    higher = true;
            }

            if (higher)
            {
                queue.Enqueue(job);
            }
            else
            {
                printed++;
                count[priority]--;

                if (job == location)
                    return printed;
            }
        }

        return -1;
    }

}



