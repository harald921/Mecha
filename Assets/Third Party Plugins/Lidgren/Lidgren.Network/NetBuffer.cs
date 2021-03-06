﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lidgren.Network
{
	public partial class NetBuffer
	{
		/// Number of bytes to overallocate for each message to avoid resizing
		protected const int c_overAllocateAmount = 4;

		private static readonly Dictionary<Type, MethodInfo> s_readMethods;
		private static readonly Dictionary<Type, MethodInfo> s_writeMethods;

		internal byte[] m_data;
		internal int m_bitLength;
		internal int m_readPosition;

		/// Gets or sets the internal data buffer
		public byte[] Data
		{
			get { return m_data; }
			set
            {
                m_data = value;
                LengthBytes = m_data.Length;
            }
		}

		/// Gets or sets the length of the used portion of the buffer in bytes
		public int LengthBytes
		{
			get { return ((m_bitLength + 7) >> 3); }
			set
			{
				m_bitLength = value * 8;
				InternalEnsureBufferSize(m_bitLength);
			}
		}

		/// Gets or sets the length of the used portion of the buffer in bits
		public int LengthBits
		{
			get { return m_bitLength; }
			set
			{
				m_bitLength = value;
				InternalEnsureBufferSize(m_bitLength);
			}
		}

		/// Gets or sets the read position in the buffer, in bits (not bytes)
		public long Position
		{
			get { return (long)m_readPosition; }
			set { m_readPosition = (int)value; }
		}

		/// Gets the position in the buffer in bytes; note that the bits of the first returned byte may already have been read - check the Position property to make sure.
		public int PositionInBytes => (int)(m_readPosition / 8); 
	}
}
