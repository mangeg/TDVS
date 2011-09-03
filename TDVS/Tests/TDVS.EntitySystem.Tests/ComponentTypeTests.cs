using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

			var b1 = new BitArray( EntitySystem.MAX_NR_COMPONENT_TYPES );
			b1.Set( t1.ID, true );
			var b2 = new BitArray( EntitySystem.MAX_NR_COMPONENT_TYPES );
			b2.Set( t2.ID, true );

			Assert.AreEqual( b1.And( t1.Bit ), b1 );
			Assert.AreEqual( b2.And( t2.Bit ), b2 );
		}
	}
}
