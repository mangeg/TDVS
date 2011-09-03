using System;

namespace TDVS.EntitySystem.Tests
{
	public class TestSystem1 : EntitySystem
	{
		public TestSystem1( params Type[] types )
			: base( types )
		{
		}
	}
	
	public class TestSystem2 : EntitySystem
	{
		public TestSystem2( params Type[] types )
			: base( types )
		{
		}
	}
}