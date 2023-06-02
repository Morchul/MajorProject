using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ActionRingBuffer : IEnumerable
{
	private readonly IAction[] buffer;
	private readonly int size;

	private int head;
	private int tail;

	public int Length => tail >= head ? tail - head : size - head + tail;

	public ActionRingBuffer(int size)
	{
		buffer = new IAction[size];
		this.size = size;
		head = 0;
		tail = 0;
	}

	public void Add(IAction item)
	{
		buffer[tail] = item;

		tail = ++tail % size;

		int deadLockCounter = 0;
		while (tail == head)
        {
			++head;
			Remove();
			++tail;

			//Prevents dead lock if all elements in the ring buffer are active and a new active element is tried to add
			if(++deadLockCounter == size)
            {
				Debug.LogWarning("RingBuffer dead lock because buffer is to small. Increase the size of the Ringbuffer");
				return;
			}
		}
	}

	public void Remove()
    {
		for(int i = head; i != tail; i = (i + 1) % size)
        {
			if (buffer[i].Status != ActionState.FINISHED)
            {
				++head;
				buffer[i].Status = ActionState.INACTIVE;
			}
				
			else
				break;
        }
    }

	public IAction this[int index]
	{
		get
		{
			if (index >= Length)
				throw new System.Exception("Invalid index: " + index + " in ring buffer");
			else
			{
				return buffer[(head + index) % size];
			}
		}

		set
		{
			if (index >= Length)
				throw new System.Exception("Invalid index: " + index + " in ring buffer");
			else
			{
				buffer[(head + index) % size] = value;
			}
		}

	}

	public bool Empty => head == tail;

	public void Clear()
	{
		head = tail;
	}

	public void ClearReferences()
	{
		for (int i = 0; i < size; ++i)
		{
			buffer[i] = default;
		}
		Clear();
	}

	public IEnumerator GetEnumerator()
	{
		for(int i = head; i != tail; i = (i + 1) % size)
		{
			if(buffer[i].Status == ActionState.ACTIVE)
				yield return buffer[i];
		}
	}
}
