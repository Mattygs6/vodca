// <copyright file="Crc32.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    ///     Calculate CRC32 
    /// </summary>
    /// <remarks>
    ///  See http://damieng.com/blog/2006/08/08/calculating_crc32_in_c_and_net   
    /// </remarks>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Web\Misc\Crc32.cs" title="Crc32.cs" lang="C#" />
    /// </example>
    internal sealed partial class Crc32 : HashAlgorithm
    {
        /// <summary>
        /// The polynomial seed
        /// </summary>
        private readonly uint seed;

        /// <summary>
        /// The polynomial vale table
        /// </summary>
        private readonly uint[] table;

        /// <summary>
        /// The Default value table
        /// </summary>
        private static uint[] defaultTable;

        /// <summary>
        /// The polynomial hash
        /// </summary>
        private uint hash;

        /// <summary>
        /// Initializes a new instance of the <see cref="Crc32"/> class.
        /// </summary>
        public Crc32()
        {
            this.table = InitializeTable(0xedb88320);
            this.seed = uint.MaxValue;
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Crc32"/> class.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <param name="seed">The polynomial seed.</param>
        public Crc32(uint polynomial, uint seed)
        {
            this.table = InitializeTable(polynomial);
            this.seed = seed;
            this.Initialize();
        }

        /// <summary>
        /// Gets the size, in bits, of the computed hash code.
        /// </summary>
        /// <returns>The size, in bits, of the computed hash code.</returns>
        public override int HashSize
        {
            get
            {
                return 0x20;
            }
        }

        /// <summary>
        /// Computes the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>The calculated polynomial UInt32 value</returns>
        public static uint Compute(byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(0xedb88320), uint.MaxValue, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Computes the specified seed.
        /// </summary>
        /// <param name="seed">The polynomial seed.</param>
        /// <param name="buffer">The polynomial buffer.</param>
        /// <returns>The calculated polynomial UInt32 value</returns>
        public static uint Compute(uint seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(0xedb88320), seed, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Computes the specified polynomial.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <param name="seed">The polynomial seed.</param>
        /// <param name="buffer">The polynomial buffer.</param>
        /// <returns>The calculated polynomial UInt32 value</returns>
        public static uint Compute(uint polynomial, uint seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm"/> class.
        /// </summary>
        public override void Initialize()
        {
            this.hash = this.seed;
        }

        /// <summary>
        /// Hashes the core.
        /// </summary>
        /// <param name="buffer">The polynomial buffer.</param>
        /// <param name="start">The polynomial start.</param>
        /// <param name="length">The polynomial length.</param>
        protected override void HashCore(byte[] buffer, int start, int length)
        {
            this.hash = CalculateHash(this.table, this.hash, buffer, start, length);
        }

        /// <summary>
        /// When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.
        /// </summary>
        /// <returns>
        /// The computed hash code.
        /// </returns>
        protected override byte[] HashFinal()
        {
            byte[] buffer = this.UInt32ToBigEndianBytes(~this.hash);
            this.HashValue = buffer;

            return buffer;
        }

        /// <summary>
        /// Initializes the table.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <returns>The default polynomial table values</returns>
        private static uint[] InitializeTable(uint polynomial)
        {
            if ((polynomial == 0xedb88320) && (defaultTable != null))
            {
                return defaultTable;
            }

            var numArray = new uint[0x100];
            for (int i = 0; i < 0x100; i++)
            {
                var num2 = (uint)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((num2 & 1) == 1)
                    {
                        num2 = (num2 >> 1) ^ polynomial;
                    }
                    else
                    {
                        num2 = num2 >> 1;
                    }
                }

                numArray[i] = num2;
            }

            if (polynomial == 0xedb88320)
            {
                defaultTable = numArray;
            }

            return numArray;
        }

        /// <summary>
        /// Calculates the hash.
        /// </summary>
        /// <param name="table">The polynomial table.</param>
        /// <param name="seed">The polynomial seed.</param>
        /// <param name="buffer">The polynomial buffer.</param>
        /// <param name="start">The polynomial start index.</param>
        /// <param name="size">The polynomial size.</param>
        /// <returns>The calculated hash value</returns>
        private static uint CalculateHash(uint[] table, uint seed, byte[] buffer, int start, int size)
        {
            uint num = seed;

            for (int i = start; i < size; i++)
            {
                num = (num >> 8) ^ table[(int)((IntPtr)(buffer[i] ^ (num & 0xff)))];
            }

            return num;
        }

        /// <summary>
        /// The uint32 to big Endian bytes.
        /// </summary>
        /// <param name="x">The polynomial byte value.</param>
        /// <returns>Fixed polynomial byte value</returns>
        private byte[] UInt32ToBigEndianBytes(uint x)
        {
            return new[] { ((byte)((x >> 0x18) & 0xff)), ((byte)((x >> 0x10) & 0xff)), ((byte)((x >> 8) & 0xff)), ((byte)(x & 0xff)) };
        }
    }
}
