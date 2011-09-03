using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDVS.Common;

namespace TDVS.EntitySystem.Tests
{
	[TestClass]
	public class ComponentTypeTests
	{
		[TestMethod]
		public void ensure_id_counter_is_increasing_for_each_type()
		{
			var t1 = new ComponentType();
			var t2 = new ComponentType();

			Assert.AreNotEqual( t1.ID, t2.ID );
			Assert.AreEqual( Math.Abs( t1.ID - t2.ID ), 1 );
		}

		[TestMethod]
		public void ensure_bit_field_is_increasing_and_correct()
		{
			var t1 = new ComponentType();
			var t2 = new ComponentType();

			var b1 = new BitArrayExt( EntitySystem.MAX_NR_COMPONENT_TYPES );
			b1.Set( t1.ID, true );
			var b2 = new BitArrayExt( EntitySystem.MAX_NR_COMPONENT_TYPES );
			b2.Set( t2.ID, true );

			Assert.AreEqual( b1, t1.Bit );
			Assert.AreEqual( b2, t2.Bit );
		}
	}
}
