using System.Collections;
using UnityEngine;

public class ActionRingBuffer : IEnumerable
{
	private readonly IAction[] buffer;
	private readonly int size;

	public int Head { get; private set; }
	public int Tail { get; private set; }

	public int Length => Tail >= Head ? Tail - Head : size - Head + Tail;

	public ActionRingBuffer(int size)
	{
		buffer = new IAction[size + 1];
		this.size = size + 1;
		Head = 0;
		Tail = 0;
	}

	private int Increase(int value) => ++value % size;
	private int Decrease(int value) => value == 0 ? size - 1 : --value;

	public void Add(IAction item)
	{
		if(item.Status == ActionState.SLEEPING)
		{
			item.Status = ActionState.ACTIVE;
			return;
		}

		int insertPoint = Tail;

		Tail = Increase(Tail);

		int deadLockCounter = 0;

		while (Tail == Head)
        {
			Head = Increase(Head);
			Remove();
			Tail = Increase(Tail);

			//Prevents dead lock if all elements in the ring buffer are active and a new active element is tried to add
			if(++deadLockCounter == size)
            {
				Tail = Decrease(Tail);
				throw new System.Exception("RingBuffer dead lock because buffer is to small. Increase the size of the Ringbuffer");
			}
		}

		buffer[insertPoint] = item;
		buffer[insertPoint].Status = ActionState.ACTIVE;
	}

	public void Remove()
    {
		while(Head != Tail)
		{
			if (buffer[Head].IsInactive())
			{
				buffer[Head].Status = ActionState.INACTIVE;
				Head = Increase(Head);
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
				return buffer[(Head + index) % size];
			}
		}

		set
		{
			if (index >= Length)
				throw new System.Exception("Invalid index: " + index + " in ring buffer");
			else
			{
				buffer[(Head + index) % size] = value;
			}
		}

	}

	public bool Empty => Head == Tail;

	public void Clear()
	{
		Head = Tail;
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
		for(int i = Head; i != Tail; i = (i + 1) % size)
		{
			if(buffer[i].IsActive())
				yield return buffer[i];
		}
	}

    #region Debug
    public void DEBUG_OUTPUT_BUFFER()
	{
		for(int i = 0; i < buffer.Length; ++i)
		{
			if (buffer[i] == null) continue;
			Debug.Log($"Pos {i}: {buffer[i].Name} / {buffer[i].Status}");
		}
	}
	#endregion
}
