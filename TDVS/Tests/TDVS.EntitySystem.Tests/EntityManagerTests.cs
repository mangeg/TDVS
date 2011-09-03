using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDVS.EntitySystem.Tests
{
	[TestClass]
	public class EntityManagerTests
	{
		private World _world;
		private EntityManager _mgr;

		public EntityManagerTests()
		{
			_world = new World();
			_mgr = new EntityManager( _world );
		}

		[TestMethod]
		public void add_entity_and_count_should_be_1()
		{
			var entity = _mgr.Create();

			Assert.AreEqual( _mgr.TotalCount, 1 );
		}

		[TestMethod]
		public void add_entity_and_remove_total_should_be_1_and_active_should_be_0()
		{
			var entity = _mgr.Create();
			_mgr.Delete( entity );

			Assert.AreEqual( _mgr.TotalCount, 1 );
			Assert.AreEqual( _mgr.ActiveCount, 0 );
		}

		[TestMethod]
		public void add_100_entities_and_last_should_have_id_99()
		{
			Entity e = null;
			for ( var i = 0; i < 100; i++ )
			{
				e = _mgr.Create();
			}

			Assert.AreEqual( e.ID, 99 );
		}

		[TestMethod]
		public void get_entity_by_id_should_return_correct_entity()
		{
			var entity = _mgr.Create();
			var entity2 = _mgr.Create();
			var retEntity = _mgr.GetEntity( entity.ID );

			Assert.AreEqual( entity.ID, retEntity.ID );
		}

		[TestMethod]
		public void ensure_entities_are_reused_after_deleted()
		{
			var e = _mgr.Create();
			var id1 = e.ID;
			_mgr.Delete( e );
			e = _mgr.Create();
			var id2 = e.ID;

			Assert.AreEqual( id1, id2 );
		}

		[TestMethod]
		public void ensure_deleted_entities_are_not_returned_from_get_entity()
		{
			var e = _mgr.Create();
			var id = e.ID;
			_mgr.Delete( e );
			e = _mgr.GetEntity( id );

			Assert.IsNull( e );
		}

		[TestMethod]
		public void ensure_isactive_returns_false_for_deleted_entities_else_true()
		{
			var e1 = _mgr.Create();
			var e2 = _mgr.Create();

			_mgr.Delete( e1 );

			Assert.IsFalse( _mgr.IsActive( e1.ID ) );
			Assert.IsTrue( _mgr.IsActive( e2.ID ) );
		}

		[TestMethod]
		public void ensure_counters_are_coorect()
		{
			var e1 = _mgr.Create();
			var e2 = _mgr.Create();
			var e3 = _mgr.Create();
			_mgr.Delete( e1 );


			Assert.AreEqual( _mgr.ActiveCount, 2 );
			Assert.AreEqual( _mgr.InactiveCount, 1 );
			Assert.AreEqual( _mgr.TotalCount, 3 );
		}

		[TestMethod]
		public void ensure_components_are_added_to_entity()
		{
			var entity = _mgr.Create();
			var component = new TestComponent();

			_mgr.AddComponent<TestComponent>( entity, component );

			Assert.AreEqual( _mgr.GetComponent<TestComponent>( entity ), component );
			Assert.AreEqual( _mgr.GetComponent( entity, ComponentTypeManager.GetTypeFor<TestComponent>() ), component );
		}

		[TestMethod]
		public void ensure_components_are_removed_when_entity_is_deleted()
		{
			var entity = _mgr.Create();
			var component = new TestComponent();

			_mgr.AddComponent<TestComponent>( entity, component );
			_mgr.Delete( entity );

			Assert.AreNotEqual( _mgr.GetComponent<TestComponent>( entity ), component );
		}

		[TestMethod]
		public void ensure_added_event_fires()
		{
			bool fired = false;
			_mgr.EntityCreated += ( e ) =>
			{
				fired = true;
			};
			var entity = _mgr.Create();

			Assert.IsTrue( fired );
		}

		[TestMethod]
		public void ensure_removed_event_fires()
		{
			bool fired = false;
			_mgr.EntityRemoved += ( e ) =>
			{
				fired = true;
			};
			var entity = _mgr.Create();
			_mgr.Delete( entity );

			Assert.IsTrue( fired );
		}

		[TestMethod]
		public void ensure_component_added_event_fires()
		{
			bool fired = false;
			_mgr.ComponentAdded += ( e, c ) =>
			{
				fired = true;
			};
			var entity = _mgr.Create();
			_mgr.AddComponent( entity, new TestComponent() );

			Assert.IsTrue( fired );
		}

		[TestMethod]
		public void ensure_component_removed_event_fires()
		{
			bool fired = false;
			_mgr.ComponentRemoved += ( e, c ) =>
			{
				fired = true;
			};
			var entity = _mgr.Create();
			_mgr.AddComponent( entity, new TestComponent() );
			_mgr.RemoveComponent( entity, ComponentTypeManager.GetTypeFor<TestComponent>() );

			Assert.IsTrue( fired );
		}
	}


}
