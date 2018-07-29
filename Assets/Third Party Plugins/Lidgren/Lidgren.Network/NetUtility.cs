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

#if !__NOIPENDPOINT__
using NetEndPoint = System.Net.IPEndPoint;
using NetAddress = System.Net.IPAddress;
#endif

using System;
using System.Net;

using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Lidgren.Network
{
	/// Utility methods
	public static partial class NetUtility
	{
		///  Create a hex string from an Int64 value
		public static string ToHexString(long data) =>  
			ToHexString(BitConverter.GetBytes(data));


		/// Create a hex string from an array of bytes
		public static string ToHexString(byte[] data) =>
			ToHexString(data, 0, data.Length);


		/// Create a hex string from an array of bytes
		public static string ToHexString(byte[] data, int offset, int length)
		{
			char[] c = new char[length * 2];
			byte b;
			for (int i = 0; i < length; ++i)
			{
				b = ((byte)(data[offset + i] >> 4));
				c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
				b = ((byte)(data[offset + i] & 0xF));
				c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
			}
			return new string(c);
		}

        public static int BitsToHoldBytes(byte[] inBytes) =>
            inBytes.Length * 8;

		/// Returns how many bits are necessary to hold a certain number
		public static int BitsToHoldUInt(uint value)
		{
			int bits = 1;
			while ((value >>= 1) != 0)
				bits++;
			return bits;
		}

		/// Returns how many bits are necessary to hold a certain number
		public static int BitsToHoldUInt64(ulong value)
		{
			int bits = 1;
			while ((value >>= 1) != 0)
				bits++;
			return bits;
		}

		/// Returns how many bytes are required to hold a certain number of bits
		public static int BytesToHoldBits(int numBits) => 
			(numBits + 7) / 8;

		internal static UInt32 SwapByteOrder(UInt32 value) =>
				((value & 0xff000000) >> 24) |
				((value & 0x00ff0000) >> 8)  |
				((value & 0x0000ff00) << 8)  |
				((value & 0x000000ff) << 24);

		internal static UInt64 SwapByteOrder(UInt64 value) =>
				((value & 0xff00000000000000L) >> 56) |
				((value & 0x00ff000000000000L) >> 40) |
				((value & 0x0000ff0000000000L) >> 24) |
				((value & 0x000000ff00000000L) >> 8)  |
				((value & 0x00000000ff000000L) << 8)  |
				((value & 0x0000000000ff0000L) << 24) |
				((value & 0x000000000000ff00L) << 40) |
				((value & 0x00000000000000ffL) << 56);


		internal static bool CompareElements(byte[] one, byte[] two)
		{
			if (one.Length != two.Length)
				return false;
			for (int i = 0; i < one.Length; i++)
				if (one[i] != two[i])
					return false;
			return true;
		}

		/// Convert a hexadecimal string to a byte array
		public static byte[] ToByteArray(String hexString)
		{
			byte[] retval = new byte[hexString.Length / 2];
			for (int i = 0; i < hexString.Length; i += 2)
				retval[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
			return retval;
		}

		/// Converts a number of bytes to a shorter, more readable string representation
		public static string ToHumanReadable(long bytes)
		{
			if (bytes < 4000) // 1-4 kb is printed in bytes
				return bytes + " bytes";
			if (bytes < 1000 * 1000) // 4-999 kb is printed in kb
				return Math.Round(((double)bytes / 1000.0), 2) + " kilobytes";
			return Math.Round(((double)bytes / (1000.0 * 1000.0)), 2) + " megabytes"; // else megabytes
		}

		// shell sort
		internal static void SortMembersList(System.Reflection.MemberInfo[] list)
		{
			int h;
			int j;
			System.Reflection.MemberInfo tmp;

			h = 1;
			while (h * 3 + 1 <= list.Length)
				h = 3 * h + 1;

			while (h > 0)
			{
				for (int i = h - 1; i < list.Length; i++)
				{
					tmp = list[i];
					j = i;
					while (true)
					{
						if (j >= h)
						{
							if (string.Compare(list[j - h].Name, tmp.Name, StringComparison.InvariantCulture) > 0)
							{
								list[j] = list[j - h];
								j -= h;
							}
							else
								break;
						}
						else
							break;
					}

					list[j] = tmp;
				}
				h /= 3;
			}
		}
	}
}