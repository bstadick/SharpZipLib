// ZipConstants.cs
//
// Copyright (C) 2001 Mike Krueger
// Copyright (C) 2004 John Reilly
//
// This file was translated from java, it was part of the GNU Classpath
// Copyright (C) 2001 Free Software Foundation, Inc.
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// Linking this library statically or dynamically with other modules is
// making a combined work based on this library.  Thus, the terms and
// conditions of the GNU General Public License cover the whole
// combination.
// 
// As a special exception, the copyright holders of this library give you
// permission to link this library with independent modules to produce an
// executable, regardless of the license terms of these independent
// modules, and to copy and distribute the resulting executable under
// terms of your choice, provided that you also meet, for each linked
// independent module, the terms and conditions of the license of that
// module.  An independent module is a module which is not derived from
// or based on this library.  If you modify this library, you may extend
// this exception to your version of the library, but you are not
// obligated to do so.  If you do not wish to do so, delete this
// exception statement from your version.

using System;
using System.Text;
using System.Threading;

namespace ICSharpCode.SharpZipLib.Zip 
{
	
	/// <summary>
	/// The kind of compression used for an entry in an archive
	/// </summary>
	public enum CompressionMethod
	{
		/// <summary>
		/// A direct copy of the file contents is held in the archive
		/// </summary>
		Stored     = 0,
		
		/// <summary>
		/// Common Zip compression method using a sliding dictionary 
		/// of up to 32KB and secondary compression from Huffman/Shannon-Fano trees
		/// </summary>
		Deflated   = 8,
		
		/// <summary>
		/// An extension to deflate with a 64KB window. Not supported by #Zip currently
		/// </summary>
		Deflate64  = 9,
		
		/// <summary>
		/// Not supported by #Zip currently
		/// </summary>
		BZip2      = 11,
		
		/// <summary>
		/// WinZip special for AES encryption, Not supported by #Zip
		/// </summary>
		WinZipAES  = 99,
		
	}
	
	/// <summary>
	/// Defines the contents of the general bit flags field for an archive entry.
	/// </summary>
	[Flags]
	enum GeneralBitFlags : int
	{
		/// <summary>
		/// If set indicates that the file is encrypted
		/// </summary>
		Encrypted         = 0x0001,
		/// <summary>
		/// Two bits defining the compression method (only for Method 6 Imploding and 8,9 Deflating)
		/// </summary>
		Method            = 0x0006,
		/// <summary>
		/// If set a trailing data desciptor is appended to the entry data
		/// </summary>
		Descriptor        = 0x0008,
		/// <summary>
		/// Reserved
		/// </summary>
		Reserved          = 0x0010,
		/// <summary>
		/// If set indicates the file contains Pkzip compressed patched data.
		/// </summary>
		Patched           = 0x0020,
		/// <summary>
		/// If set strong encryption has been used for this entry.
		/// </summary>
		StrongEncryption  = 0x0040,
		/// <summary>
		/// Reserved by PKWare for enhanced compression.
		/// </summary>
		EnhancedCompress  = 0x1000,
		/// <summary>
		/// If set indicates that values in the local header are masked to hide
		/// their actual values, and the central directory is encrypted.
		/// </summary>
		/// <remarks>
		/// Used when encrypting the central directory contents.
		/// </remarks>
		HeaderMasked      = 0x2000
	}
	
	/// <summary>
	/// This class contains constants used for Zip format files
	/// </summary>
	public sealed class ZipConstants
	{
		#region Versions
		/// <summary>
		/// The version made by field for entries in the central header when created by this library
		/// </summary>
		/// <remarks>
		/// This is also the Zip version for the library when comparing against the version required to extract
		/// for an entry.  See <see cref="ZipInputStream.CanDecompressEntry">ZipInputStream.CanDecompressEntry</see>.
		/// </remarks>
		public const int VersionMadeBy = 45;
		
		/// <summary>
		/// The version made by field for entries in the central header when created by this library
		/// </summary>
		/// <remarks>
		/// This is also the Zip version for the library when comparing against the version required to extract
		/// for an entry.  See <see cref="ZipInputStream.CanDecompressEntry">ZipInputStream.CanDecompressEntry</see>.
		/// </remarks>
		[Obsolete("Use VersionMadeBy instead")]
		public const int VERSION_MADE_BY = 45;
		
		/// <summary>
		/// The minimum version required to support strong encryption
		/// </summary>
		public const int VersionStrongEncryption = 50;

		/// <summary>
		/// The minimum version required to support strong encryption
		/// </summary>
		[Obsolete("Use VersionStrongEncryption instead")]
		public const int VERSION_STRONG_ENCRYPTION = 50;
		
		/// <summary>
		/// The version required for Zip64 extensions
		/// </summary>
		public const int VersionZip64 = 45;
		
		#endregion
		
		#region Header Sizes
		/// <summary>
		/// Size of local entry header (excluding variable length fields at end)
		/// </summary>
		public const int LocalHeaderBaseSize = 30;
		
		/// <summary>
		/// Size of local entry header (excluding variable length fields at end)
		/// </summary>
		[Obsolete("Use LocalHeaderBaseSize instead")]
		public const int LOCHDR = 30;
		
		/// <summary>
		/// Size of Zip64 data descriptor
		/// </summary>
		public const int Zip64DataDescriptorSize = 20;
		
		/// <summary>
		/// Size of data descriptor
		/// </summary>
		public const int DataDescriptorSize = 16;
		
		/// <summary>
		/// Size of data descriptor
		/// </summary>
		[Obsolete("Use DataDescriptorSize instead")]
		public const int EXTHDR = 16;
		
		/// <summary>
		/// Size of central header entry (excluding variable fields)
		/// </summary>
		public const int CentralHeaderBaseSize = 46;
		
		/// <summary>
		/// Size of central header entry
		/// </summary>
		[Obsolete("Use CentralHeaderBaseSize instead")]
		public const int CENHDR = 46;
		
		/// <summary>
		/// Size of end of central record (excluding variable fields)
		/// </summary>
		public const int EndOfCentralRecordBaseSize = 22;
		
		/// <summary>
		/// Size of end of central record (excluding variable fields)
		/// </summary>
		[Obsolete("Use EndOfCentralRecordBaseSize instead")]
		public const int ENDHDR = 22;
		
		/// <summary>
		/// Size of 'classic' cryptographic header stored before any entry data
		/// </summary>
		public const int CryptoHeaderSize = 12;
		
		/// <summary>
		/// Size of cryptographic header stored before entry data
		/// </summary>
		[Obsolete("Use CryptoHeaderSize instead")]
		public const int CRYPTO_HEADER_SIZE = 12;
		#endregion
		
		#region Header Signatures
		
		/// <summary>
		/// Signature for local entry header
		/// </summary>
		public const int LocalHeaderSignature = 'P' | ('K' << 8) | (3 << 16) | (4 << 24);

		/// <summary>
		/// Signature for local entry header
		/// </summary>
		[Obsolete("Use LocalHeaderSignature instead")]
		public const int LOCSIG = 'P' | ('K' << 8) | (3 << 16) | (4 << 24);

		/// <summary>
		/// Signature for spanning entry
		/// </summary>
		public const int SpanningSignature = 'P' | ('K' << 8) | (7 << 16) | (8 << 24);
		
		/// <summary>
		/// Signature for spanning entry
		/// </summary>
		[Obsolete("Use SpanningSignature instead")]
		public const int SPANNINGSIG = 'P' | ('K' << 8) | (7 << 16) | (8 << 24);
		
		/// <summary>
		/// Signature for temporary spanning entry
		/// </summary>
		public const int SpanningTempSignature = 'P' | ('K' << 8) | ('0' << 16) | ('0' << 24);
		
		/// <summary>
		/// Signature for temporary spanning entry
		/// </summary>
		[Obsolete("Use SpanningTempSignature instead")]
		public const int SPANTEMPSIG = 'P' | ('K' << 8) | ('0' << 16) | ('0' << 24);
		
		/// <summary>
		/// Signature for data descriptor
		/// </summary>
		/// <remarks>
		/// This is only used where the length, Crc, or compressed size isnt known when the
		/// entry is created and the output stream doesnt support seeking.
		/// The local entry cannot be 'patched' with the correct values in this case
		/// so the values are recorded after the data prefixed by this header, as well as in the central directory.
		/// </remarks>
		public const int DataDescriptorSignature = 'P' | ('K' << 8) | (7 << 16) | (8 << 24);
		
		/// <summary>
		/// Signature for data descriptor
		/// </summary>
		/// <remarks>
		/// This is only used where the length, Crc, or compressed size isnt known when the
		/// entry is created and the output stream doesnt support seeking.
		/// The local entry cannot be 'patched' with the correct values in this case
		/// so the values are recorded after the data prefixed by this header, as well as in the central directory.
		/// </remarks>
		[Obsolete("Use DataDescriptorSignature instead")]
		public const int EXTSIG = 'P' | ('K' << 8) | (7 << 16) | (8 << 24);
		
		/// <summary>
		/// Signature for central header
		/// </summary>
		[Obsolete("Use CentralHeaderSignature instead")]
		public const int CENSIG = 'P' | ('K' << 8) | (1 << 16) | (2 << 24);

		/// <summary>
		/// Signature for central header
		/// </summary>
		public const int CentralHeaderSignature = 'P' | ('K' << 8) | (1 << 16) | (2 << 24);

		/// <summary>
		/// Signature for Zip64 central file header
		/// </summary>
		public const int Zip64CentralFileHeaderSignature = 'P' | ('K' << 8) | (6 << 16) | (6 << 24);
		
		/// <summary>
		/// Signature for Zip64 central file header
		/// </summary>
		[Obsolete("Use Zip64CentralFileHeaderSignature instead")]
		public const int CENSIG64 = 'P' | ('K' << 8) | (6 << 16) | (6 << 24);
		
		/// <summary>
		/// Signature for Zip64 central directory locator
		/// </summary>
		public const int Zip64CentralDirLocatorSignature = 'P' | ('K' << 8) | (6 << 16) | (7 << 24);
		
		/// <summary>
		/// Signature for archive extra data signature (were headers are encrypted).
		/// </summary>
		public const int ArchiveExtraDataSignature = 'P' | ('K' << 8) | (6 << 16) | (7 << 24);
		
		/// <summary>
		/// Central header digitial signature
		/// </summary>
		public const int CentralHeaderDigitalSignature = 'P' | ('K' << 8) | (5 << 16) | (5 << 24);
		
		/// <summary>
		/// Central header digitial signature
		/// </summary>
		[Obsolete("Use CentralHeaderDigitalSignaure instead")]
		public const int CENDIGITALSIG = 'P' | ('K' << 8) | (5 << 16) | (5 << 24);

		/// <summary>
		/// End of central directory record signature
		/// </summary>
		public const int EndOfCentralDirectorySignature = 'P' | ('K' << 8) | (5 << 16) | (6 << 24);
		
		/// <summary>
		/// End of central directory record signature
		/// </summary>
		[Obsolete("Use EndOfCentralDirectorySignature instead")]
		public const int ENDSIG = 'P' | ('K' << 8) | (5 << 16) | (6 << 24);
		#endregion
		

#if !COMPACT_FRAMEWORK

        static int defaultCodePage = Thread.CurrentThread.CurrentCulture.TextInfo.OEMCodePage;
		
		/// <summary>
		/// Default encoding used for string conversion.  0 gives the default system Ansi code page.
		/// Dont use unicode encodings if you want to be Zip compatible!
		/// Using the default code page isnt the full solution neccessarily
		/// there are many variable factors, codepage 850 is often a good choice for
		/// European users, however be careful about compatability.
		/// </summary>
		public static int DefaultCodePage {
			get {
				return defaultCodePage; 
			}
			set {
				defaultCodePage = value; 
			}
		}
#endif

		/// <summary>
		/// Convert a portion of a byte array to a string.
		/// </summary>		
		/// <param name="data">
		/// Data to convert to string
		/// </param>
		/// <param name="length">
		/// Number of bytes to convert starting from index 0
		/// </param>
		/// <returns>
		/// data[0]..data[length - 1] converted to a string
		/// </returns>
		public static string ConvertToString(byte[] data, int length)
		{
			if ( data == null ) {
				return string.Empty;	
			}
			
#if COMPACT_FRAMEWORK
			return Encoding.ASCII.GetString(data, 0, length);
#else
			return Encoding.GetEncoding(DefaultCodePage).GetString(data, 0, length);
#endif
		}
	
		/// <summary>
		/// Convert byte array to string
		/// </summary>
		/// <param name="data">
		/// Byte array to convert
		/// </param>
		/// <returns>
		/// <paramref name="data">data</paramref>converted to a string
		/// </returns>
		public static string ConvertToString(byte[] data)
		{
			if ( data == null ) {
				return string.Empty;	
			}
			return ConvertToString(data, data.Length);
		}

		/// <summary>
		/// Convert a string to a byte array
		/// </summary>
		/// <param name="str">
		/// String to convert to an array
		/// </param>
		/// <returns>Converted array</returns>
		public static byte[] ConvertToArray(string str)
		{
			if ( str == null ) {
				return new byte[0];
			}
			
#if COMPACT_FRAMEWORK
			return Encoding.ASCII.GetBytes(str);
#else
			return Encoding.GetEncoding(DefaultCodePage).GetBytes(str);
#endif
		}
		
		/// <summary>
		/// Initialise default instance of <see cref="ZipConstants">ZipConstants</see>
		/// </summary>
		/// <remarks>
		/// Private to prevent instances being created.
		/// </remarks>
		ZipConstants()
		{
			// Do nothing
		}
	}
}
