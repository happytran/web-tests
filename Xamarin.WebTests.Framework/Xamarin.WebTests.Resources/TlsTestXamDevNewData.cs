﻿//
// TlsTestXamDevNewData.cs
//
// Author:
//       Martin Baulig <martin.baulig@xamarin.com>
//
// Copyright (c) 2016 Xamarin Inc. (http://www.xamarin.com)

//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;

namespace Xamarin.WebTests.Resources
{
	public class TlsTestXamDevNewData : CertificateInfo
	{
		public TlsTestXamDevNewData (byte[] rawData)
			: base (CertificateResourceType.TlsTestXamDevNew, rawData)
		{
		}

		const string subject = "/C=US/ST=California/L=San Francisco/O=Xamarin Inc./CN=*.xamdev.com";
		const string issuer = "/C=US/O=DigiCert Inc/CN=DigiCert SHA2 Secure Server CA";
		const string managedSubject = "CN=*.xamdev.com, O=Xamarin Inc., L=San Francisco, S=California, C=US";
		const string managedIssuer = "CN=DigiCert SHA2 Secure Server CA, O=DigiCert Inc, C=US";

		internal static readonly byte[] hash = new byte[] {
			0x0d, 0x78, 0x98, 0xad, 0x0f, 0x8d, 0x68, 0x9d, 0xce, 0x9c, 0x86, 0x2f, 0x8b, 0x8a, 0xe4, 0x62,
			0x6b, 0x0d, 0xc6, 0x33
		};

		internal static readonly byte[] subject_rawData = new byte[] {
			0x30, 0x68, 0x31, 0x0b, 0x30, 0x09, 0x06, 0x03, 0x55, 0x04, 0x06, 0x13, 0x02, 0x55, 0x53, 0x31,
			0x13, 0x30, 0x11, 0x06, 0x03, 0x55, 0x04, 0x08, 0x13, 0x0a, 0x43, 0x61, 0x6c, 0x69, 0x66, 0x6f,
			0x72, 0x6e, 0x69, 0x61, 0x31, 0x16, 0x30, 0x14, 0x06, 0x03, 0x55, 0x04, 0x07, 0x13, 0x0d, 0x53,
			0x61, 0x6e, 0x20, 0x46, 0x72, 0x61, 0x6e, 0x63, 0x69, 0x73, 0x63, 0x6f, 0x31, 0x15, 0x30, 0x13,
			0x06, 0x03, 0x55, 0x04, 0x0a, 0x13, 0x0c, 0x58, 0x61, 0x6d, 0x61, 0x72, 0x69, 0x6e, 0x20, 0x49,
			0x6e, 0x63, 0x2e, 0x31, 0x15, 0x30, 0x13, 0x06, 0x03, 0x55, 0x04, 0x03, 0x0c, 0x0c, 0x2a, 0x2e,
			0x78, 0x61, 0x6d, 0x64, 0x65, 0x76, 0x2e, 0x63, 0x6f, 0x6d
		};
		internal static readonly byte[] subject_rawDataCanon = new byte[] {
			0x31, 0x0b, 0x30, 0x09, 0x06, 0x03, 0x55, 0x04, 0x06, 0x0c, 0x02, 0x75, 0x73, 0x31, 0x13, 0x30,
			0x11, 0x06, 0x03, 0x55, 0x04, 0x08, 0x0c, 0x0a, 0x63, 0x61, 0x6c, 0x69, 0x66, 0x6f, 0x72, 0x6e,
			0x69, 0x61, 0x31, 0x16, 0x30, 0x14, 0x06, 0x03, 0x55, 0x04, 0x07, 0x0c, 0x0d, 0x73, 0x61, 0x6e,
			0x20, 0x66, 0x72, 0x61, 0x6e, 0x63, 0x69, 0x73, 0x63, 0x6f, 0x31, 0x15, 0x30, 0x13, 0x06, 0x03,
			0x55, 0x04, 0x0a, 0x0c, 0x0c, 0x78, 0x61, 0x6d, 0x61, 0x72, 0x69, 0x6e, 0x20, 0x69, 0x6e, 0x63,
			0x2e, 0x31, 0x15, 0x30, 0x13, 0x06, 0x03, 0x55, 0x04, 0x03, 0x0c, 0x0c, 0x2a, 0x2e, 0x78, 0x61,
			0x6d, 0x64, 0x65, 0x76, 0x2e, 0x63, 0x6f, 0x6d
		};
		internal static readonly byte[] issuer_rawData = new byte[] {
			0x30, 0x4d, 0x31, 0x0b, 0x30, 0x09, 0x06, 0x03, 0x55, 0x04, 0x06, 0x13, 0x02, 0x55, 0x53, 0x31,
			0x15, 0x30, 0x13, 0x06, 0x03, 0x55, 0x04, 0x0a, 0x13, 0x0c, 0x44, 0x69, 0x67, 0x69, 0x43, 0x65,
			0x72, 0x74, 0x20, 0x49, 0x6e, 0x63, 0x31, 0x27, 0x30, 0x25, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13,
			0x1e, 0x44, 0x69, 0x67, 0x69, 0x43, 0x65, 0x72, 0x74, 0x20, 0x53, 0x48, 0x41, 0x32, 0x20, 0x53,
			0x65, 0x63, 0x75, 0x72, 0x65, 0x20, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x20, 0x43, 0x41
		};
		internal static readonly byte[] issuer_rawDataCanon = new byte[] {
			0x31, 0x0b, 0x30, 0x09, 0x06, 0x03, 0x55, 0x04, 0x06, 0x0c, 0x02, 0x75, 0x73, 0x31, 0x15, 0x30,
			0x13, 0x06, 0x03, 0x55, 0x04, 0x0a, 0x0c, 0x0c, 0x64, 0x69, 0x67, 0x69, 0x63, 0x65, 0x72, 0x74,
			0x20, 0x69, 0x6e, 0x63, 0x31, 0x27, 0x30, 0x25, 0x06, 0x03, 0x55, 0x04, 0x03, 0x0c, 0x1e, 0x64,
			0x69, 0x67, 0x69, 0x63, 0x65, 0x72, 0x74, 0x20, 0x73, 0x68, 0x61, 0x32, 0x20, 0x73, 0x65, 0x63,
			0x75, 0x72, 0x65, 0x20, 0x73, 0x65, 0x72, 0x76, 0x65, 0x72, 0x20, 0x63, 0x61
		};

		static readonly CertificateNameInfo subjectName = new CertificateNameInfo (
			0x4a10a94fL, 0xf36e2b0fL, subject_rawData, subject_rawDataCanon, subject);
		static readonly CertificateNameInfo issuerName = new CertificateNameInfo (
			0x85cf5865L, 0x4bcd7fc4L, issuer_rawData, issuer_rawDataCanon, issuer);

		static readonly DateTime notBefore = new DateTime (2016, 3, 15, 0, 0, 0, DateTimeKind.Utc);
		static readonly DateTime notAfter = new DateTime (2017, 4, 12, 12, 0, 0, DateTimeKind.Utc);

		internal static readonly byte[] serial = new byte[] {
			0x0c, 0x6f, 0xfd, 0x03, 0x64, 0xf1, 0xa7, 0x21, 0xd5, 0x7a, 0x4a, 0x67, 0x1c, 0xd3, 0xc9, 0x96
		};
		internal static readonly byte[] serialMono = new byte[] {
			0x96, 0xc9, 0xd3, 0x1c, 0x67, 0x4a, 0x7a, 0xd5, 0x21, 0xa7, 0xf1, 0x64, 0x03, 0xfd, 0x6f, 0x0c
		};

		internal static readonly byte[] publicKeyData = new byte[] {
			0x30, 0x82, 0x01, 0x0a, 0x02, 0x82, 0x01, 0x01, 0x00, 0xa1, 0x34, 0xe4, 0x98, 0xa9, 0x62, 0xd3,
			0x2e, 0x6e, 0x4b, 0xa7, 0xa4, 0xff, 0x21, 0x56, 0x27, 0x80, 0x40, 0xe0, 0x31, 0x59, 0x89, 0xcc,
			0x0a, 0x6d, 0xed, 0x78, 0xa5, 0x5d, 0xe8, 0xcf, 0x23, 0x9a, 0xe1, 0xce, 0x10, 0x80, 0xa2, 0xac,
			0x89, 0x04, 0xbc, 0x37, 0x44, 0x7d, 0xee, 0xca, 0x71, 0x46, 0x22, 0xd3, 0xc3, 0x84, 0xad, 0x50,
			0xba, 0xd4, 0x8b, 0x74, 0x60, 0x3b, 0x2d, 0x18, 0xff, 0xce, 0x26, 0x35, 0x08, 0xf8, 0x62, 0x82,
			0x9f, 0x19, 0x44, 0xb2, 0x7a, 0x74, 0x50, 0x73, 0x76, 0x17, 0xcc, 0x11, 0x40, 0xb9, 0x10, 0x2e,
			0xe2, 0x27, 0x81, 0x27, 0x99, 0xf4, 0x07, 0x7e, 0xef, 0xbc, 0x8d, 0x2e, 0xa0, 0x8d, 0xe4, 0x2a,
			0x15, 0xf2, 0x70, 0x7f, 0xaf, 0x4e, 0x3c, 0x58, 0x36, 0x16, 0xb6, 0xa1, 0x13, 0x2e, 0x2a, 0x1f,
			0xfa, 0x8f, 0x9b, 0x4f, 0xac, 0xab, 0xc1, 0x19, 0x60, 0xf7, 0xd2, 0xec, 0x48, 0xb0, 0xb1, 0x61,
			0xf3, 0x47, 0x72, 0x72, 0x6c, 0x58, 0xf7, 0xef, 0x59, 0x5d, 0x02, 0x44, 0xf7, 0x35, 0xfe, 0xc4,
			0xfd, 0x9d, 0xd4, 0xa1, 0x03, 0xdd, 0x08, 0x0d, 0x95, 0x7e, 0xe9, 0x31, 0x9d, 0x90, 0x5c, 0x95,
			0x0b, 0x98, 0x0e, 0xe9, 0x49, 0xca, 0x5f, 0xf1, 0x8f, 0xfd, 0x57, 0x78, 0x65, 0xf1, 0x30, 0xe5,
			0x9d, 0xda, 0xa0, 0xd6, 0x04, 0xfa, 0x7b, 0xcb, 0xa8, 0x6c, 0x3c, 0x45, 0xba, 0x75, 0x47, 0x64,
			0xb5, 0x55, 0x7d, 0x8b, 0x03, 0xdd, 0x11, 0xca, 0x83, 0x4f, 0x6d, 0x12, 0xe6, 0x95, 0x20, 0x63,
			0x68, 0xfd, 0x9c, 0x39, 0xc0, 0x5d, 0xd0, 0x9a, 0x16, 0x72, 0xa4, 0xb2, 0x1b, 0x37, 0xbf, 0x8f,
			0x85, 0xa3, 0x7f, 0x91, 0xed, 0xa7, 0x69, 0xbc, 0xb5, 0xd8, 0x60, 0xfa, 0x84, 0x13, 0x91, 0x5f,
			0x2c, 0x22, 0x5b, 0xb4, 0x91, 0xff, 0xbd, 0xae, 0xeb, 0x02, 0x03, 0x01, 0x00, 0x01
		};

		public override string ManagedSubjectName {
			get {
				return managedSubject;
			}
		}

		public override string ManagedIssuerName {
			get {
				return managedIssuer;
			}
		}

		public override byte[] Hash {
			get {
				return hash;
			}
		}

		public override CertificateNameInfo IssuerName {
			get {
				return issuerName;
			}
		}

		public override string IssuerNameString {
			get {
				return issuer;
			}
		}

		public override DateTime NotAfter {
			get {
				return notAfter;
			}
		}

		public override DateTime NotBefore {
			get {
				return notBefore;
			}
		}

		public override string PublicKeyAlgorithmOid {
			get {
				return Oid_Rsa;
			}
		}

		public override byte[] PublicKeyData {
			get {
				return publicKeyData;
			}
		}

		public override byte[] PublicKeyParameters {
			get {
				return EmptyPublicKeyParameters;
			}
		}

		public override byte[] SerialNumber {
			get {
				return serial;
			}
		}

		public override byte[] SerialNumberMono {
			get {
				return serialMono;
			}
		}

		public override string SignatureAlgorithmOid {
			get {
				return Oid_RsaWithSha256;
			}
		}

		public override CertificateNameInfo SubjectName {
			get {
				return subjectName;
			}
		}

		public override string SubjectNameString {
			get {
				return subject;
			}
		}

		public override int Version {
			get {
				return 3;
			}
		}
	}
}

