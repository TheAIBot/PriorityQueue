using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriorityQueue;
using System;
using System.Collections.Generic;

namespace Testing
{
    [TestClass]
    public class PriorityQueueTests
    {

        [TestMethod]
        public void EnqueueDequeueIntInt()
        {
            var rando = new Random(1);
            var max = 10000;
            var queue = new PriorityQueue<int, int>();
            var numbers = new List<int>();

            for (int i = 0; i < max; i++)
            {
                int value = rando.Next(max);
                numbers.Add(value);
                queue.Enqueue(value, value);
            }

            Assert.IsTrue(queue.Count == max);

            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                Assert.IsTrue(numbers.Contains(next));
                numbers.Remove(next);

                if (queue.Count > 0)
                {
                    Assert.IsTrue(queue.Peek() >= next, $"Expected {queue.Peek().ToString("N0")} to be more than {next.ToString("N0")}.");
                }
            }

            Assert.AreEqual(0, numbers.Count);
        }

        [TestMethod]
        public void EnqueueDequeueUnorderedIntInt()
        {
            var rando = new Random(2);
            var queue = new PriorityQueue<int, int>();
            var numbers = new List<int>();

            for (int z = 0; z < 100; z++)
            {
                for (int i = 0; i < 1000; i++)
                {
                    int value = rando.Next(int.MinValue, int.MaxValue);
                    numbers.Add(value);
                    queue.Enqueue(value, value);
                }
                for (int x = 0; x < 300; x++)
                {
                    var next = queue.Dequeue();
                    Assert.IsTrue(numbers.Contains(next));
                    numbers.Remove(next);

                    if (queue.Count > 0)
                    {
                        Assert.IsTrue(queue.Peek() >= next, $"Expected {queue.Peek().ToString("N0")} to be more than {next.ToString("N0")}.");
                    }
                }
            }
        }

        [TestMethod]
        public void EnqueueDequeueStringInt()
        {
            var queue = new PriorityQueue<string, int>();

            queue.Enqueue("A", 2);
            queue.Enqueue("B", 1);
            queue.Enqueue("C", 3);

            Assert.AreEqual("B", queue.Dequeue());
            Assert.AreEqual("A", queue.Dequeue());
            Assert.AreEqual("C", queue.Dequeue());
        }

        [TestMethod]
        public void EnqueueDequeueUnorderedStringInt()
        {
            var queue = new PriorityQueue<string, int>();

            queue.Enqueue("A", 2);
            Assert.AreEqual("A", queue.Peek());
            Assert.AreEqual("A", queue.Dequeue());

            queue.Enqueue("A", 2);
            queue.Enqueue("B", 1);
            Assert.AreEqual("B", queue.Dequeue());
            Assert.AreEqual("A", queue.Dequeue());

            queue.Enqueue("A", 2);
            queue.Enqueue("B", 1);
            queue.Enqueue("C", 3);
            Assert.AreEqual("B", queue.Dequeue());
            Assert.AreEqual("A", queue.Dequeue());
            Assert.AreEqual("C", queue.Dequeue());
        }
    }
}
