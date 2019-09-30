using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class CircularBuffer<T>
    {
        /// <summary>
        /// The underlying buffer.
        /// </summary>
        private readonly T[] _buffer;

        /// <summary>
        /// The next index to read from in <see cref="_buffer"/>
        /// </summary>
        private int _readPosition = 0;

        /// <summary>
        /// The next index to write to in <see cref="_buffer"/>
        /// </summary>
        private int _writePosition = 0;

        /// <summary>
        /// The maximum number of elements the buffer can hold at one time.
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// The number of elements currently available in the buffer.
        /// </summary>
        public int Count { get; private set; } = 0;

        /// <summary>
        /// Determines if the buffer is full.
        /// </summary>
        public bool IsFull => Capacity == Count;

        /// <summary>
        /// Determines if the buffer is empty.
        /// </summary>
        public bool IsEmpty => Capacity == 0;

        /// <summary>
        /// Creates a new <see cref="CircularBuffer{T}"/> with the specified capacity.
        /// </summary>
        /// <param name="capacity">The maximum number of elements the buffer can hold at one time.</param>
        public CircularBuffer(int capacity)
        {
            Capacity = capacity;
            _buffer = new T[capacity];
        }

        /// <summary>
        /// Writes a maximum of <paramref name="length"/> elements to the buffer.
        /// </summary>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="startIndex">The index at which to start reading from <paramref name="buffer"/>.</param>
        /// <param name="length">The maximum number of elements to write.</param>
        /// <returns>The number of elements read from <paramref name="buffer"/>.</returns>
        public int Write(T[] buffer, int startIndex, int length)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (startIndex + length > buffer.Length)
            {
                throw new ArgumentException("The sum of '" + nameof(startIndex) + "' and '" + nameof(length) + "' is larger than the length of '" + nameof(buffer) + "'.");
            }

            if (startIndex < 0 || length < 0)
            {
                throw new ArgumentOutOfRangeException("'" + nameof(startIndex) + "' or '" + nameof(length) + "' is negative.'");
            }

            if (length == 0)
            {
                return 0;
            }

            int totalElementsToWrite = Math.Min(length, Capacity);
            int writeOffset = length - totalElementsToWrite;

            _writePosition = (_writePosition + writeOffset) % Capacity;

            int elementsToWrite = Math.Min(totalElementsToWrite, Capacity - _writePosition);
            int readStartIndex = startIndex + writeOffset;
            Array.Copy(buffer, readStartIndex, _buffer, _writePosition, elementsToWrite);
            _writePosition = (_writePosition + elementsToWrite) % Capacity;
            readStartIndex += elementsToWrite;

            elementsToWrite = totalElementsToWrite - elementsToWrite;
            if (elementsToWrite > 0)
            {
                Array.Copy(buffer, readStartIndex, _buffer, _writePosition, elementsToWrite);
                _writePosition = (_writePosition + elementsToWrite) % Capacity;
            }

            // Check for write overflow. If we did overflow, we update the _readPosition
            if (Count + totalElementsToWrite > Capacity)
            {
                _readPosition = _writePosition;
                Count = Capacity;
            }
            else
            {
                Count += totalElementsToWrite;
            }

            return totalElementsToWrite;
        }

        public void WriteOne(T val)
        {
            _buffer[_writePosition] = val;
            _writePosition = (_writePosition + 1) % Capacity;

            // Check for write overflow. If we did overflow, we update the _readPosition
            if (Count + 1 > Capacity)
            {
                _readPosition = _writePosition;
                Count = Capacity;
            }
            else
            {
                Count += 1;
            }
        }

        /// <summary>
        /// Reads a maximum of <paramref name="length"/> elements from the buffer.
        /// </summary>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="startIndex">The index at which to start writing to <paramref name="buffer"/>.</param>
        /// <param name="length">The maximum number of elements to read.</param>
        /// <param name="removeFromBuffer">
        /// If true, the data read will be removed from the buffer.
        /// If false, the next read will continue from the same position.
        /// </param>
        /// <returns>The number of elements written into <paramref name="buffer"/>.</returns>
        public int Read(T[] buffer, int startIndex, int length, bool removeFromBuffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (startIndex + length > buffer.Length)
            {
                throw new ArgumentException("The sum of '" + nameof(startIndex) + "' and '" + nameof(length) + "' is larger than the length of '" + nameof(buffer) + "'.");
            }

            if (startIndex < 0 || length < 0)
            {
                throw new ArgumentOutOfRangeException("'" + nameof(startIndex) + "' or '" + nameof(length) + "' is negative.'");
            }

            if (length == 0)
            {
                return 0;
            }

            int totalElementsToRead = Math.Min(length, Count);

            if (totalElementsToRead == 0)
            {
                return 0;
            }

            int readIndex = _readPosition;
            int elementsToRead = Math.Min(totalElementsToRead, Capacity - readIndex);
            int writeStartIndex = startIndex;
            Array.Copy(_buffer, readIndex, buffer, writeStartIndex, elementsToRead);
            readIndex = (readIndex + elementsToRead) % Capacity;
            writeStartIndex += elementsToRead;

            elementsToRead = totalElementsToRead - elementsToRead;
            if (elementsToRead > 0)
            {
                Array.Copy(_buffer, readIndex, buffer, writeStartIndex, elementsToRead);
                readIndex = (readIndex + elementsToRead) % Capacity;
            }

            if (removeFromBuffer)
            {
                _readPosition = readIndex;
                Count -= totalElementsToRead;
            }

            return totalElementsToRead;
        }

        public T ReadOne(bool removeFromBuffer)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The buffer is empty.");
            }

            T res = _buffer[_readPosition];

            if (removeFromBuffer)
            {
                _readPosition = (_readPosition + 1) % Capacity;
                Count -= 1;
            }

            return res;
        }

        public void SkipElements(int elementsCount)
        {
            int totalElementsToSkip = Math.Min(elementsCount, Count);

            _readPosition = (_readPosition + totalElementsToSkip) % Capacity;
            Count -= totalElementsToSkip;
        }
    }
}
