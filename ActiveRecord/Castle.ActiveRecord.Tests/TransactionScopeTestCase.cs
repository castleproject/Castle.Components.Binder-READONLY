// Copyright 2004-2005 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.ActiveRecord.Tests
{
	using System;

	using NUnit.Framework;

	using NHibernate;


	[TestFixture]
	public class TransactionScopeTestCase
	{
		[Test]
		[Ignore("Scope Not implemented")]
		public void ScopeWorks()
		{
			ISession session1, session2, session3, session4;

			ActiveRecordStarter.Initialize( typeof(Post), typeof(Blog) );

			using(new TransactionScope())
			{
				session1 = Blog.Holder.CreateSession( typeof(Blog) );
				session2 = Blog.Holder.CreateSession( typeof(Post) );
				session3 = Blog.Holder.CreateSession( typeof(Blog) );
				session4 = Blog.Holder.CreateSession( typeof(Post) );

				Assert.IsNotNull( session1 );
				Assert.IsNotNull( session2 );
				Assert.IsNotNull( session3 );
				Assert.IsNotNull( session3 );

				Assert.IsTrue( session2 == session1 );
				Assert.IsTrue( session3 == session1 );
				Assert.IsTrue( session4 == session1 );
			}

			// Old behavior

			session1 = Blog.Holder.CreateSession( typeof(Blog) );
			session2 = Blog.Holder.CreateSession( typeof(Blog) );

			Assert.IsNotNull( session1 );
			Assert.IsNotNull( session2 );
			Assert.IsTrue( session1 != session2 );

			Blog.Holder.ReleaseSession(session1);
			Blog.Holder.ReleaseSession(session2);
		}
	}
}
