
# Priority Queue

A simple and super efficient priority Queue for .NET Core that uses a min heap for the underlying data structure. It does not allocate for each enqueued item. It supports both classes and structs. The priority type can be specified.

| Method                | Time Complexity |
| ------                | --------------- |
| Peek()                | O(1)            |
| PeekPriority()        | O(1)            |
| PeekWithPriority()    | O(1)            |
| Enqueue()             | O(log(n))       |
| Dequeue()             | O(log(n))       |
| DequeueWithPriority() | O(log(n))       |

You can read more about heaps here : https://en.wikipedia.org/wiki/Heap_(data_structure)

## Usage

```csharp
var queue = new PriorityQueue<string, int>();

queue.Enqueue("A", 2);
queue.Enqueue("B", 1);
queue.Enqueue("C", 3);

while (queue.Count > 0)
{
    Console.WriteLine(queue.Dequeue());
}
//"B"
//"A"
//"C"
```
