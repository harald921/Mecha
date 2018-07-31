//#define UNSAFE
//#define BIGENDIAN
/* Copyright (c) 2010 Michael Lidgren

Permission is hereby granted, free of charge, to any person obtaining a copy of this software
and associated documentation files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom
the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE
USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Runtime.InteropServices;

namespace Lidgren.Network
{
	
	/// Utility struct for writing Singles
	
	[StructLayout(LayoutKind.Explicit)]
	public struct SingleUIntUnion
	{
		
		/// Value as a 32 bit float
		
		[FieldOffset(0)]
		public float SingleValue;

		
		/// Value as an unsigned 32 bit integer
		
		[FieldOffset(0)]

		public uint UIntValue;
	}

	public partial class NetBuffer
	{
        public NetBuffer() { }
        public NetBuffer(byte[] inData)
        {
            Data = inData;
            LengthBytes = inData.Length;
        }
		/// Ensures the buffer can hold this number of bits
		
		public void EnsureBufferSize(int numberOfBits)
		{
			int byteLen = ((numberOfBits + 7) >> 3);
			if (m_data == null)
			{
				m_data = new byte[byteLen + c_overAllocateAmount];
				return;
			}
			if (m_data.Length < byteLen)
				Array.Resize<byte>(ref m_data, byteLen + c_overAllocateAmount);
			return;
		}

		
		/// Ensures the buffer can hold this number of bits
		
		internal void InternalEnsureBufferSize(int numberOfBits)
		{
			int byteLen = ((numberOfBits + 7) >> 3);
			if (m_data == null)
			{
				m_data = new byte[byteLen];
				return;
			}
			if (m_data.Length < byteLen)
				Array.Resize<byte>(ref m_data, byteLen);
			return;
		}

		
		/// Writes a boolean value using 1 bit
		
		public void Write(bool value)
		{
			EnsureBufferSize(m_bitLength + 1);
			NetBitWriter.WriteByte((value ? (byte)1 : (byte)0), 1, m_data, m_bitLength);
			m_bitLength += 1;
		}

		
		/// Write a byte
		
		public void Write(byte source)
		{
			EnsureBufferSize(m_bitLength + 8);
			NetBitWriter.WriteByte(source, 8, m_data, m_bitLength);
			m_bitLength += 8;
		}

		
		/// Writes a byte at a given offset in the buffer
		
		public void WriteAt(Int32 offset, byte source) {
			int newBitLength = Math.Max(m_bitLength, offset + 8);
			EnsureBufferSize(newBitLength);
			NetBitWriter.WriteByte((byte) source, 8, m_data, offset);
			m_bitLength = newBitLength;
		}

		
		/// Writes a signed byte
		

		public void Write(sbyte source)
		{
			EnsureBufferSize(m_bitLength + 8);
			NetBitWriter.WriteByte((byte)source, 8, m_data, m_bitLength);
			m_bitLength += 8;
		}

		
		/// Writes 1 to 8 bits of a byte
		
		public void Write(byte source, int numberOfBits)
		{
			NetException.Assert((numberOfBits > 0 && numberOfBits <= 8), "Write(byte, numberOfBits) can only write between 1 and 8 bits");
			EnsureBufferSize(m_bitLength + numberOfBits);
			NetBitWriter.WriteByte(source, numberOfBits, m_data, m_bitLength);
			m_bitLength += numberOfBits;
		}

		
		/// Writes all bytes in an array
		
		public void Write(byte[] source)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			int bits = source.Length * 8;
			EnsureBufferSize(m_bitLength + bits);
			NetBitWriter.WriteBytes(source, 0, source.Length, m_data, m_bitLength);
			m_bitLength += bits;
		}

		
		/// Writes the specified number of bytes from an array
		
		public void Write(byte[] source, int offsetInBytes, int numberOfBytes)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			int bits = numberOfBytes * 8;
			EnsureBufferSize(m_bitLength + bits);
			NetBitWriter.WriteBytes(source, offsetInBytes, numberOfBytes, m_data, m_bitLength);
			m_bitLength += bits;
		}

		
		/// Writes an unsigned 16 bit integer
		
		/// <param name="source"></param>

		public void Write(UInt16 source)
		{
			EnsureBufferSize(m_bitLength + 16);
			NetBitWriter.WriteUInt16(source, 16, m_data, m_bitLength);
			m_bitLength += 16;
		}

		
		/// Writes a 16 bit unsigned integer at a given offset in the buffer
		

		public void WriteAt(Int32 offset, UInt16 source)
		{
			int newBitLength = Math.Max(m_bitLength, offset + 16);
			EnsureBufferSize(newBitLength);
			NetBitWriter.WriteUInt16(source, 16, m_data, offset);
			m_bitLength = newBitLength;
		}

		
		/// Writes an unsigned integer using 1 to 16 bits
		

		public void Write(UInt16 source, int numberOfBits)
		{
			NetException.Assert((numberOfBits > 0 && numberOfBits <= 16), "Write(ushort, numberOfBits) can only write between 1 and 16 bits");
			EnsureBufferSize(m_bitLength + numberOfBits);
			NetBitWriter.WriteUInt16(source, numberOfBits, m_data, m_bitLength);
			m_bitLength += numberOfBits;
		}

		
		/// Writes a signed 16 bit integer
		
		public void Write(Int16 source)
		{
			EnsureBufferSize(m_bitLength + 16);
			NetBitWriter.WriteUInt16((ushort)source, 16, m_data, m_bitLength);
			m_bitLength += 16;
		}

		
		/// Writes a 16 bit signed integer at a given offset in the buffer
		
		public void WriteAt(Int32 offset, Int16 source)
		{
			int newBitLength = Math.Max(m_bitLength, offset + 16);
			EnsureBufferSize(newBitLength);
			NetBitWriter.WriteUInt16((ushort)source, 16, m_data, offset);
			m_bitLength = newBitLength;
		}

#if UNSAFE
		
		/// Writes a 32 bit signed integer
		
		public unsafe void Write(Int32 source)
		{
			EnsureBufferSize(m_bitLength + 32);

			// can write fast?
			if (m_bitLength % 8 == 0)
			{
				fixed (byte* numRef = &Data[m_bitLength / 8])
				{
					*((int*)numRef) = source;
				}
			}
			else
			{
				NetBitWriter.WriteUInt32((UInt32)source, 32, Data, m_bitLength);
			}
			m_bitLength += 32;
		}
#else
		
		/// Writes a 32 bit signed integer
		public void Write(Int32 source)
		{
			EnsureBufferSize(m_bitLength + 32);
			NetBitWriter.WriteUInt32((UInt32)source, 32, m_data, m_bitLength);
			m_bitLength += 32;
		}
#endif

		
		/// Writes a 32 bit signed integer at a given offset in the buffer
		
		public void WriteAt(Int32 offset, Int32 source)
		{
			int newBitLength = Math.Max(m_bitLength, offset + 32);
			EnsureBufferSize(newBitLength);
			NetBitWriter.WriteUInt32((UInt32)source, 32, m_data, offset);
			m_bitLength = newBitLength;
		}

#if UNSAFE
		
		/// Writes a 32 bit unsigned integer
		
		public unsafe void Write(UInt32 source)
		{
			EnsureBufferSize(m_bitLength + 32);

			// can write fast?
			if (m_bitLength % 8 == 0)
			{
				fixed (byte* numRef = &Data[m_bitLength / 8])
				{
					*((uint*)numRef) = source;
				}
			}
			else
			{
				NetBitWriter.WriteUInt32(source, 32, Data, m_bitLength);
			}

			m_bitLength += 32;
		}
#else
		
		/// Writes a 32 bit unsigned integer
		

		public void Write(UInt32 source)
		{
			EnsureBufferSize(m_bitLength + 32);
			NetBitWriter.WriteUInt32(source, 32, m_data, m_bitLength);
			m_bitLength += 32;
		}
#endif

		
		/// Writes a 32 bit unsigned integer at a given offset in the buffer
		

		public void WriteAt(Int32 offset, UInt32 source)
		{
			int newBitLength = Math.Max(m_bitLength, offset + 32);
			EnsureBufferSize(newBitLength);
			NetBitWriter.WriteUInt32(source, 32, m_data, offset);
			m_bitLength = newBitLength;
		}

		
		/// Writes a 32 bit signed integer
		

		public void Write(UInt32 source, int numberOfBits)
		{
			NetException.Assert((numberOfBits > 0 && numberOfBits <= 32), "Write(uint, numberOfBits) can only write between 1 and 32 bits");
			EnsureBufferSize(m_bitLength + numberOfBits);
			NetBitWriter.WriteUInt32(source, numberOfBits, m_data, m_bitLength);
			m_bitLength += numberOfBits;
		}

		
		/// Writes a signed integer using 1 to 32 bits
		
		public void Write(Int32 source, int numberOfBits)
		{
			NetException.Assert((numberOfBits > 0 && numberOfBits <= 32), "Write(int, numberOfBits) can only write between 1 and 32 bits");
			EnsureBufferSize(m_bitLength + numberOfBits);

			if (numberOfBits != 32)
			{
				// make first bit sign
				int signBit = 1 << (numberOfBits - 1);
				if (source < 0)
					source = (-source - 1) | signBit;
				else
					source &= (~signBit);
			}

			NetBitWriter.WriteUInt32((uint)source, numberOfBits, m_data, m_bitLength);

			m_bitLength += numberOfBits;
		}

		/// Writes a 64 bit unsigned integer

		public void Write(UInt64 source)
		{
			EnsureBufferSize(m_bitLength + 64);
			NetBitWriter.WriteUInt64(source, 64, m_data, m_bitLength);
			m_bitLength += 64;
		}

		/// Writes a 64 bit unsigned integer at a given offset in the buffer

		public void WriteAt(Int32 offset, UInt64 source)
		{
			int newBitLength = Math.Max(m_bitLength, offset + 64);
			EnsureBufferSize(newBitLength);
			NetBitWriter.WriteUInt64(source, 64, m_data, offset);
			m_bitLength = newBitLength;
		}

		/// Writes an unsigned integer using 1 to 64 bits

		public void Write(UInt64 source, int numberOfBits)
		{
			EnsureBufferSize(m_bitLength + numberOfBits);
			NetBitWriter.WriteUInt64(source, numberOfBits, m_data, m_bitLength);
			m_bitLength += numberOfBits;
		}

		/// Writes a 64 bit signed integer
		public void Write(Int64 source)
		{
			EnsureBufferSize(m_bitLength + 64);
			ulong usource = (ulong)source;
			NetBitWriter.WriteUInt64(usource, 64, m_data, m_bitLength);
			m_bitLength += 64;
		}

		/// Writes a signed integer using 1 to 64 bits
		public void Write(Int64 source, int numberOfBits)
		{
			EnsureBufferSize(m_bitLength + numberOfBits);
			ulong usource = (ulong)source;
			NetBitWriter.WriteUInt64(usource, numberOfBits, m_data, m_bitLength);
			m_bitLength += numberOfBits;
		}

		//
		// Floating point
		//
#if UNSAFE
		
		/// Writes a 32 bit floating point value
		
		public unsafe void Write(float source)
		{
			uint val = *((uint*)&source);
#if BIGENDIAN
				val = NetUtility.SwapByteOrder(val);
#endif
			Write(val);
		}
#else
		/// Writes a 32 bit floating point value
		public void Write(float source)
		{
			// Use union to avoid BitConverter.GetBytes() which allocates memory on the heap
			SingleUIntUnion su;
			su.UIntValue = 0; // must initialize every member of the union to avoid warning
			su.SingleValue = source;

#if BIGENDIAN
			// swap byte order
			su.UIntValue = NetUtility.SwapByteOrder(su.UIntValue);
#endif
			Write(su.UIntValue);
		}
#endif

#if UNSAFE
		
		/// Writes a 64 bit floating point value
		
		public unsafe void Write(double source)
		{
			ulong val = *((ulong*)&source);
#if BIGENDIAN
			val = NetUtility.SwapByteOrder(val);
#endif
			Write(val);
		}
#else
		/// Writes a 64 bit floating point value
		public void Write(double source)
		{
			byte[] val = BitConverter.GetBytes(source);
#if BIGENDIAN
			// 0 1 2 3   4 5 6 7

			// swap byte order
			byte tmp = val[7];
			val[7] = val[0];
			val[0] = tmp;

			tmp = val[6];
			val[6] = val[1];
			val[1] = tmp;

			tmp = val[5];
			val[5] = val[2];
			val[2] = tmp;

			tmp = val[4];
			val[4] = val[3];
			val[3] = tmp;
#endif
			Write(val);
		}
#endif

		// Variable bits

		/// Write Base128 encoded variable sized unsigned integer of up to 32 bits
		/// <returns>number of bytes written</returns>
		public int WriteVariableUInt32(uint value)
		{
			int retval = 1;
			uint num1 = (uint)value;
			while (num1 >= 0x80)
			{
				this.Write((byte)(num1 | 0x80));
				num1 = num1 >> 7;
				retval++;
			}
			this.Write((byte)num1);
			return retval;
		}

		
		/// Write Base128 encoded variable sized signed integer of up to 32 bits
		/// <returns>number of bytes written</returns>
		public int WriteVariableInt32(int value)
		{
			uint zigzag = (uint)(value << 1) ^ (uint)(value >> 31);
			return WriteVariableUInt32(zigzag);
		}

		
		/// Write Base128 encoded variable sized signed integer of up to 64 bits
		/// <returns>number of bytes written</returns>
		public int WriteVariableInt64(Int64 value)
		{
			ulong zigzag = (ulong)(value << 1) ^ (ulong)(value >> 63);
			return WriteVariableUInt64(zigzag);
		}

		
		/// Write Base128 encoded variable sized unsigned integer of up to 64 bits
		/// <returns>number of bytes written</returns>
		public int WriteVariableUInt64(UInt64 value)
		{
			int retval = 1;
			UInt64 num1 = (UInt64)value;
			while (num1 >= 0x80)
			{
				this.Write((byte)(num1 | 0x80));
				num1 = num1 >> 7;
				retval++;
			}
			this.Write((byte)num1);
			return retval;
		}

		
		/// Compress (lossy) a float in the range -1..1 using numberOfBits bits
		public void WriteSignedSingle(float value, int numberOfBits)
		{
			NetException.Assert(((value >= -1.0) && (value <= 1.0)), " WriteSignedSingle() must be passed a float in the range -1 to 1; val is " + value);

			float unit = (value + 1.0f) * 0.5f;
			int maxVal = (1 << numberOfBits) - 1;
			uint writeVal = (uint)(unit * (float)maxVal);

			Write(writeVal, numberOfBits);
		}

		
		/// Compress (lossy) a float in the range 0..1 using numberOfBits bits
		public void WriteUnitSingle(float value, int numberOfBits)
		{
			NetException.Assert(((value >= 0.0) && (value <= 1.0)), " WriteUnitSingle() must be passed a float in the range 0 to 1; val is " + value);

			int maxValue = (1 << numberOfBits) - 1;
			uint writeVal = (uint)(value * (float)maxValue);

			Write(writeVal, numberOfBits);
		}

		
		/// Compress a float within a specified range using a certain number of bits
		
		public void WriteRangedSingle(float value, float min, float max, int numberOfBits)
		{
			NetException.Assert(((value >= min) && (value <= max)), " WriteRangedSingle() must be passed a float in the range MIN to MAX; val is " + value);

			float range = max - min;
			float unit = ((value - min) / range);
			int maxVal = (1 << numberOfBits) - 1;
			Write((UInt32)((float)maxVal * unit), numberOfBits);
		}

		
		/// Writes an integer with the least amount of bits need for the specified range
		/// Returns number of bits written
		public int WriteRangedInteger(int min, int max, int value)
		{
			NetException.Assert(value >= min && value <= max, "Value not within min/max range!");

			uint range = (uint)(max - min);
			int numBits = NetUtility.BitsToHoldUInt(range);

			uint rvalue = (uint)(value - min);
			Write(rvalue, numBits);

			return numBits;
		}
		
	        
	        /// Writes an integer with the least amount of bits need for the specified range
	        /// Returns number of bits written
	        
	        public int WriteRangedInteger(long min, long max, long value)
	        {
	            NetException.Assert(value >= min && value <= max, "Value not within min/max range!");
	
	            ulong range = (ulong)(max - min);
	            int numBits = NetUtility.BitsToHoldUInt64(range);
	
	            ulong rvalue = (ulong)(value - min);
	            Write(rvalue, numBits);
	
	            return numBits;
	        }

		
		/// Write a string
		public void Write(string source)
		{
			if (string.IsNullOrEmpty(source))
			{
				WriteVariableUInt32(0);
				return;
			}

			byte[] bytes = Encoding.UTF8.GetBytes(source);
			EnsureBufferSize(m_bitLength + 8 + (bytes.Length * 8));
			WriteVariableUInt32((uint)bytes.Length);
			Write(bytes);
		}

		
		/// Writes an endpoint description
		
		public void Write(IPEndPoint endPoint)
		{
			byte[] bytes = endPoint.Address.GetAddressBytes();
			Write((byte)bytes.Length);
			Write(bytes);
			Write((ushort)endPoint.Port);
		}

		
		/// Writes a local timestamp to a message; readable (and convertable to local time) by the remote host using ReadTime()
		
		public void WriteTime(double localTime, bool highPrecision)
		{
			if (highPrecision)
				Write(localTime);
			else
				Write((float)localTime);
		}

		
		/// Pads data with enough bits to reach a full byte. Decreases cpu usage for subsequent byte writes.
		
		public void WritePadBits()
		{
			m_bitLength = ((m_bitLength + 7) >> 3) * 8;
			EnsureBufferSize(m_bitLength);
		}

		
		/// Pads data with the specified number of bits.
		
		public void WritePadBits(int numberOfBits)
		{
			m_bitLength += numberOfBits;
			EnsureBufferSize(m_bitLength);
		}

		
		/// Append all the bits of message to this message
		
		public void Write(NetBuffer buffer)
		{
			EnsureBufferSize(m_bitLength + (buffer.LengthBytes * 8));

			Write(buffer.m_data, 0, buffer.LengthBytes);

			// did we write excessive bits?
			int bitsInLastByte = (buffer.m_bitLength % 8);
			if (bitsInLastByte != 0)
			{
				int excessBits = 8 - bitsInLastByte;
				m_bitLength -= excessBits;
			}
		}
	}
}
