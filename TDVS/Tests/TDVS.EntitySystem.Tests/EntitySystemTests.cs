using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDVS.Common.Extensions;

namespace TDVS.EntitySystem.Tests
{
	[TestClass]
	public class EntitySystemTests
	{
		private World _world;
		private EntityManager _mgr;

		public EntitySystemTests()
		{
			_world = new World();
			_mgr = new EntityManager( _world );
		}

		[TestMethod]
		public void ensure_type_bits_are_set()
		{
			var s = new TestSystem1( typeof( TestComponent1 ), typeof( TestComponent2 ) );
			_world.SystemManager.SetSystem( s );
			var t1 = ComponentTypeManager.GetTypeFor<TestComponent1>();
			var t2 = ComponentTypeManager.GetTypeFor<TestComponent2>();
			var combined = t1.Bit | t2.Bit;

			Assert.AreEqual( combined, s.TypeBit );
		}

		[TestMethod]
		public void ensure_system_bit_is_set()
		{
			var b1 = SystemBitManager.GetBit<TestSystem1>();
			var b2 = SystemBitManager.GetBit<TestSystem2>();

			var s1 = new TestSystem1( typeof( TestComponent1 ), typeof( TestComponent2 ) );
			var s2 = new TestSystem2( typeof( TestComponent1 ) );
			_world.SystemManager.SetSystem( s1 );
			_world.SystemManager.SetSystem( s2 );

			Assert.AreEqual( s1.SystemBit & b1, b1 );
			Assert.AreEqual( s2.SystemBit & b2, b2 );
			Assert.AreNotEqual( s2.SystemBit & b1, b1 );
		}

		[TestMethod]
		public void ensure_enteties_that_add_components_of_interest_are_added_to_system()
		{
			var s1 = new TestSystem1( typeof( TestComponent1 ), typeof( TestComponent2 ) );
			_world.SystemManager.SetSystem<TestSystem1>( s1 );
			var e = _mgr.Create();
			_mgr.AddComponent( e, new TestComponent1() );
			e.Refresh();
		}
	}
}