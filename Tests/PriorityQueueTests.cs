using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        [TestMethod]
        public void EnqueueDequeueDecreasingPriorityIntInt()
        {
            Random rand = new Random(3);
            for (int i = 0; i < 100_000; i++)
            {
                PriorityQueue<int, int> queue = new PriorityQueue<int, int>();
                for (int z = 0; z < rand.Next(0, 500); z++)
                {
                    int randNum = rand.Next(0, 101);
                    int priority = rand.Next();
                    queue.Enqueue(randNum, priority);
                }

                int oldPriority = int.MinValue;
                for (int z = 0; z < queue.Count; z++)
                {
                    int newPriority = queue.DequeueWithPriority().Priority;
                    Assert.IsTrue(newPriority >= oldPriority);
                    oldPriority = newPriority;
                }
            }
        }

        [TestMethod]
        public void TryRemoveFirstIntInt()
        {
            Func<int, bool>[] tests = new Func<int, bool>[]
            {
                x => x > 30,
                x => x < 77,
                x => x % 9 == 0
            };

            Random rand = new Random(3);
            foreach (var test in tests)
            {
                for (int i = 0; i < 1000; i++)
                {
                    int expectedExtracted = 0;
                    PriorityQueue<int, int> queue = new PriorityQueue<int, int>();
                    for (int z = 0; z < rand.Next(0, 50); z++)
                    {
                        int randNum = rand.Next(0, 101);
                        int priority = rand.Next();
                        queue.Enqueue(randNum, priority);
                        if (test(randNum))
                        {
                            expectedExtracted++;
                        }
                    }

                    int oldCount = queue.Count;

                    for (int z = 0; z < expectedExtracted; z++)
                    {
                        if (queue.TryRemoveFirstOrDefault(test, out int value))
                        {
                            Assert.IsTrue(test(value));
                        }
                        else
                        {
                            Assert.Fail();
                        }
                    }

                    if (!queue.TryRemoveFirstOrDefault(test, out int shouldBeDefault))
                    {
                        Assert.AreEqual(default(int), shouldBeDefault);
                    }
                    else
                    {
                        Assert.Fail();
                    }

                    Assert.AreEqual(oldCount - expectedExtracted, queue.Count);

                    int oldPriority = int.MinValue;
                    for (int z = 0; z < queue.Count; z++)
                    {
                        int newPriority = queue.DequeueWithPriority().Priority;
                        Assert.IsTrue(newPriority >= oldPriority);
                        oldPriority = newPriority;
                    }
                }
            }
        }
    }
}
