﻿//
// IntermediateCAData.cs
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
	public class IntermediateCAData : CertificateInfo
	{
		public IntermediateCAData (byte[] rawData)
			: base (CertificateResourceType.IntermediateCA, rawData)
		{
		}

		internal const string subject = "/C=US/ST=Massachusetts/O=Xamarin/OU=Engineering/CN=Intermediate Test CA";
		internal const string issuer = HamillerTubeCAData.subject;
		internal const string managedSubject = "CN=Intermediate Test CA, OU=Engineering, O=Xamarin, S=Massachusetts, C=US";
		internal const string managedIssuer = HamillerTubeCAData.managedSubject;

		internal static readonly byte[] hash = new byte[] {
			0x8f, 0xfa, 0xd2, 0x0f, 0xeb, 0xbb, 0x98, 0x85, 0x5f, 0xa5, 0x3d, 0x9c, 0x79, 0xb8, 0x17, 0x5b,
			0x8e, 0x03, 0x04, 0x4b
		};

		internal static readonly byte[] subject_rawData = new byte[] {
			0x30, 0x6c, 0x31, 0x0b, 0x30, 0x09, 0x06, 0x03, 0x55, 0x04, 0x06, 0x13, 0x02, 0x55, 0x53, 0x31,
			0x16, 0x30, 0x14, 0x06, 0x03, 0x55, 0x04, 0x08, 0x13, 0x0d, 0x4d, 0x61, 0x73, 0x73, 0x61, 0x63,
			0x68, 0x75, 0x73, 0x65, 0x74, 0x74, 0x73, 0x31, 0x10, 0x30, 0x0e, 0x06, 0x03, 0x55, 0x04, 0x0a,
			0x13, 0x07, 0x58, 0x61, 0x6d, 0x61, 0x72, 0x69, 0x6e, 0x31, 0x14, 0x30, 0x12, 0x06, 0x03, 0x55,
			0x04, 0x0b, 0x13, 0x0b, 0x45, 0x6e, 0x67, 0x69, 0x6e, 0x65, 0x65, 0x72, 0x69, 0x6e, 0x67, 0x31,
			0x1d, 0x30, 0x1b, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x14, 0x49, 0x6e, 0x74, 0x65, 0x72, 0x6d,
			0x65, 0x64, 0x69, 0x61, 0x74, 0x65, 0x20, 0x54, 0x65, 0x73, 0x74, 0x20, 0x43, 0x41
		};
		internal static readonly byte[] subject_rawDataCanon = new byte[] {
			0x31, 0x0b, 0x30, 0x09, 0x06, 0x03, 0x55, 0x04, 0x06, 0x0c, 0x02, 0x75, 0x73, 0x31, 0x16, 0x30,
			0x14, 0x06, 0x03, 0x55, 0x04, 0x08, 0x0c, 0x0d, 0x6d, 0x61, 0x73, 0x73, 0x61, 0x63, 0x68, 0x75,
			0x73, 0x65, 0x74, 0x74, 0x73, 0x31, 0x10, 0x30, 0x0e, 0x06, 0x03, 0x55, 0x04, 0x0a, 0x0c, 0x07,
			0x78, 0x61, 0x6d, 0x61, 0x72, 0x69, 0x6e, 0x31, 0x14, 0x30, 0x12, 0x06, 0x03, 0x55, 0x04, 0x0b,
			0x0c, 0x0b, 0x65, 0x6e, 0x67, 0x69, 0x6e, 0x65, 0x65, 0x72, 0x69, 0x6e, 0x67, 0x31, 0x1d, 0x30,
			0x1b, 0x06, 0x03, 0x55, 0x04, 0x03, 0x0c, 0x14, 0x69, 0x6e, 0x74, 0x65, 0x72, 0x6d, 0x65, 0x64,
			0x69, 0x61, 0x74, 0x65, 0x20, 0x74, 0x65, 0x73, 0x74, 0x20, 0x63, 0x61
		};
		internal static readonly byte[] issuer_rawData = HamillerTubeCAData.subjectName_rawData;
		internal static readonly byte[] issuer_rawDataCanon = HamillerTubeCAData.subjectName_rawDataCanon;

		internal static readonly CertificateNameInfo subjectName = new CertificateNameInfo (
			0xfe75c920L, 0xf4db406eL, subject_rawData, subject_rawDataCanon, subject);
		internal static readonly CertificateNameInfo issuerName = HamillerTubeCAData.subjectName;

		static readonly DateTime notBefore = new DateTime (2016, 3, 24, 22, 36, 28, DateTimeKind.Utc);
		static readonly DateTime notAfter = new DateTime (2026, 3, 22, 22, 36, 28, DateTimeKind.Utc);

		internal static readonly byte[] serial = new byte[] {
			0x10, 0x00, 0x03
		};
		internal static readonly byte[] serialMono = new byte[] {
			0x03, 0x00, 0x10
		};

		internal static readonly byte[] publicKeyData = new byte[] {
			0x30, 0x82, 0x02, 0x0a, 0x02, 0x82, 0x02, 0x01, 0x00, 0xca, 0xbe, 0x37, 0x8b, 0x6b, 0x27, 0xde,
			0x2f, 0x0a, 0x60, 0xcc, 0x57, 0x82, 0xeb, 0xdb, 0x81, 0x9c, 0x45, 0x63, 0xea, 0xfd, 0x55, 0x32,
			0xc0, 0x7e, 0xb4, 0x4e, 0x53, 0xe4, 0xc4, 0x6d, 0x8f, 0x27, 0xa6, 0xeb, 0xf6, 0x77, 0x34, 0x33,
			0x06, 0x82, 0xa4, 0xe0, 0x0a, 0x35, 0x78, 0xe9, 0xf7, 0x50, 0x1f, 0xd5, 0x64, 0xab, 0xac, 0x3b,
			0xb1, 0x39, 0x1b, 0x3c, 0xe4, 0xd6, 0xf0, 0xf3, 0x92, 0xc0, 0xd5, 0x62, 0x09, 0xb4, 0xb2, 0x73,
			0xef, 0xb1, 0x60, 0x47, 0x01, 0xe8, 0x6c, 0x81, 0x04, 0xdf, 0xd0, 0x9b, 0xcd, 0x56, 0x47, 0x3e,
			0x27, 0x6d, 0x5f, 0xed, 0x9a, 0x14, 0x84, 0xdb, 0x97, 0xeb, 0x8b, 0xab, 0x70, 0x18, 0xfd, 0x29,
			0x96, 0x44, 0xb6, 0x56, 0x52, 0xc7, 0x7f, 0x4e, 0xb5, 0x5b, 0xa1, 0x96, 0x77, 0x33, 0x93, 0xae,
			0xa5, 0xf6, 0x9e, 0xc4, 0x6e, 0x67, 0xba, 0xa4, 0x64, 0xf8, 0x11, 0x60, 0x5c, 0x9f, 0x5e, 0x4a,
			0x5c, 0x45, 0x9c, 0x6f, 0xa8, 0x38, 0xde, 0xa5, 0xb6, 0x81, 0xf3, 0xd5, 0x09, 0xbb, 0x7d, 0x0e,
			0x37, 0x0f, 0x34, 0x17, 0x45, 0x02, 0xc1, 0x8b, 0x55, 0x81, 0x83, 0x0c, 0xe1, 0xc9, 0xd7, 0x2f,
			0xb1, 0x39, 0xd3, 0x28, 0x24, 0xa8, 0x9b, 0xfb, 0xf3, 0xf3, 0xf1, 0x67, 0xd3, 0x40, 0xaf, 0xff,
			0x5d, 0xb8, 0x13, 0x8d, 0x70, 0x63, 0x0f, 0x25, 0xaa, 0x72, 0xad, 0xe3, 0xb7, 0x32, 0x0d, 0xf6,
			0x48, 0x17, 0xab, 0x4d, 0xbe, 0x89, 0x2b, 0x55, 0xf7, 0x55, 0xce, 0x9e, 0x76, 0x2f, 0x7a, 0xd9,
			0x49, 0xfc, 0x19, 0x20, 0x43, 0x3b, 0xcd, 0xea, 0x2b, 0x32, 0x33, 0xb3, 0x3d, 0xde, 0x43, 0xf4,
			0x40, 0xad, 0x2c, 0x3f, 0x18, 0xf4, 0x3b, 0x51, 0x64, 0x64, 0x7a, 0x61, 0x5b, 0x61, 0x62, 0x7d,
			0xfb, 0x93, 0x25, 0x89, 0xa5, 0xfd, 0xc5, 0x8b, 0x62, 0x9a, 0x18, 0x37, 0xce, 0x6c, 0xf7, 0x37,
			0xcf, 0xfa, 0x18, 0x34, 0xaa, 0x29, 0x03, 0x86, 0x60, 0x82, 0xe6, 0xc6, 0xac, 0x35, 0xdc, 0x1c,
			0x85, 0xd3, 0xd7, 0xd1, 0xcf, 0x52, 0x55, 0x6c, 0xf1, 0xe0, 0x84, 0xee, 0xb4, 0x8e, 0x58, 0x7a,
			0x96, 0x07, 0x05, 0x60, 0xf5, 0xac, 0x92, 0x9e, 0x1d, 0xbf, 0x27, 0x94, 0x74, 0x7a, 0xde, 0xbe,
			0x88, 0x9c, 0x90, 0x8a, 0xb1, 0x49, 0x74, 0xf2, 0x4b, 0xc7, 0x21, 0xff, 0x09, 0xfe, 0x3f, 0x29,
			0x42, 0x94, 0x8c, 0x6c, 0x4d, 0x1e, 0x9f, 0xc9, 0xf8, 0xc4, 0x70, 0x2d, 0x52, 0xf3, 0x31, 0x52,
			0x5c, 0x5e, 0xc3, 0xce, 0xd7, 0x47, 0x22, 0x2f, 0x9b, 0x23, 0x0a, 0xe3, 0xc4, 0x81, 0xcc, 0xfa,
			0xad, 0x65, 0x02, 0x0a, 0xbe, 0xd0, 0xd3, 0xc0, 0x61, 0x52, 0x47, 0x50, 0x9f, 0xb3, 0x8a, 0xc4,
			0x43, 0x2c, 0x50, 0xe6, 0x81, 0x68, 0x69, 0xce, 0x0c, 0xd9, 0xfc, 0x59, 0x74, 0xe0, 0x3e, 0xf0,
			0xda, 0x5c, 0x8b, 0xbb, 0xf4, 0x64, 0xf3, 0xde, 0xb2, 0xe5, 0xee, 0xd6, 0x7e, 0x1d, 0x2c, 0x53,
			0x4d, 0xce, 0xdf, 0x2a, 0xdb, 0xa2, 0x87, 0xc3, 0xc7, 0xeb, 0x23, 0xc6, 0xf7, 0x23, 0x49, 0x2a,
			0xd4, 0xb1, 0x12, 0x0b, 0x16, 0x25, 0xe0, 0x14, 0xa6, 0x4e, 0xca, 0x27, 0xa9, 0x01, 0x36, 0x71,
			0x88, 0x20, 0x3a, 0x1c, 0x39, 0x24, 0x86, 0x3a, 0xd6, 0x56, 0x99, 0xc0, 0x40, 0x67, 0xa7, 0xd5,
			0x3b, 0xe2, 0x2a, 0xa4, 0xc4, 0x8e, 0xa8, 0x79, 0xdd, 0x30, 0x23, 0x0f, 0x27, 0x4e, 0x19, 0x3a,
			0xab, 0xef, 0x74, 0x79, 0x3d, 0x7f, 0xfb, 0x31, 0x9c, 0x2f, 0x1a, 0x2a, 0x22, 0x30, 0x19, 0x9f,
			0xf3, 0x5c, 0xf1, 0x18, 0x64, 0x3a, 0x14, 0x9b, 0x3f, 0x22, 0xcf, 0xc7, 0xaa, 0x0b, 0xbd, 0x39,
			0xa2, 0xa7, 0x64, 0xb8, 0x94, 0x14, 0x2a, 0x37, 0xc5, 0x02, 0x03, 0x01, 0x00, 0x01
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

