using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDVS.Common.Tests
{
	[TestClass]
	public class BitArrayExtTests
	{
		[TestMethod]
		public void test_equals()
		{
			var a1 = new BitArrayExt( 100 );
			var a2 = new BitArrayExt( 100 );
			a1.Set( 23, true );
			a2.Set( 23, true );

			Assert.AreEqual( a1, a2 );
		}

		[TestMethod]
		public void test_not_equals()
		{
			var a1 = new BitArrayExt( 100 );
			var a2 = new BitArrayExt( 100 );
			a1.Set( 21, true );
			a2.Set( 23, true );

			Assert.AreNotEqual( a1, a2 );
		}

		[TestMethod]
		public void test_and()
		{
			var a1 = new BitArrayExt( 100 );
			a1.Set( 25, true );
			a1.Set( 53, true );
			var a2 = new BitArrayExt( 100 );
			a2.Set( 53, true );

			a1.And( a2 );

			Assert.AreEqual( a1, a2 );
		}

		[TestMethod]
		public void test_or()
		{
			var a1 = new BitArrayExt( 100 );
			a1.Set( 25, true );
			a1.Set( 53, true );
			var a2 = new BitArrayExt( 100 );
			a2.Set( 53, true );

			a2.Or( a1 );

			Assert.AreEqual( a1, a2 );
		}

		[TestMethod]
		public void test_xor()
		{
			var a1 = new BitArrayExt( 100 );
			a1.Set( 25, true );
			a1.Set( 53, true );
			var a2 = new BitArrayExt( 100 );
			a2.Set( 53, true );
			var a3 = new BitArrayExt( 100 );
			a3.Set( 25, true );

			a1.Xor( a2 );

			Assert.AreEqual( a1, a3 );
		}

		[TestMethod]
		public void test_not()
		{
			var a1 = new BitArrayExt( 100 );
			a1.Set( 25, true );
			a1.Set( 53, true );
			var a2 = new BitArrayExt( 100, true );
			a2.Set( 25, false );
			a2.Set( 53, false );

			a1.Not();

			Assert.AreEqual( a1, a2 );
		}

		[TestMethod]
		public void test_operator_or()
		{
			var a1 = new BitArrayExt( 100 );
			a1.Set( 25, true );
			a1.Set( 53, true );
			var a2 = new BitArrayExt( 100 );
			a2.Set( 54, true );
			var a3 = new BitArrayExt( 100 );
			a3.Set( 25, true );
			a3.Set( 53, true );
			a3.Set( 54, true );

			var res = a1 | a2;

			Assert.AreEqual( res, a3 );
		}

		[TestMethod]
		public void test_operator_and()
		{
			var a1 = new BitArrayExt( 100 );
			a1.Set( 25, true );
			a1.Set( 53, true );
			var a2 = new BitArrayExt( 100 );
			a2.Set( 53, true );
			var a3 = new BitArrayExt( 100 );
			a3.Set( 53, true );

			var res = a1 & a2;

			Assert.AreEqual( res, a3 );
		}
	}
}
